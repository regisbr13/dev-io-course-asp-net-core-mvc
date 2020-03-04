using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStock.Business.Interfaces;
using MyStock.Business.Interfaces.Repository;
using MyStock.Business.Interfaces.Services;
using MyStock.Business.Models;
using MyStock.Data.Exceptions;
using MyStock.Extensions.Authentication;
using MyStock.ViewModels;

namespace MyStock.Controllers
{
    [Authorize]
    [Route("Fornecedores")]
    public class ProvidersController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository, IProviderService providerService, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _providerService = providerService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var providers = _mapper.Map<List<ProviderViewModel>>(await _providerRepository.FindAll(true));
            return View(providers.OrderBy(x => x.Name));
        }

        [AllowAnonymous]
        [Route("Detalhes/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var provider = await GetById(id, true, false);

            if (provider == null) return NotFound();

            return View(provider);
        }

        [ClaimsAuthorize("Provider", "Add")]
        [Route("Novo")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Provider", "Add")]
        [Route("Novo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProviderViewModel obj)
        {
            obj.DocumentNumber = obj.DocumentNumber.Replace(".", "").Replace("-", "").Replace("/", "");

            var provider = _mapper.Map<Provider>(obj);
            await _providerService.Insert(provider);
            if (!ValidOperation()) return View(obj);

            TempData["Success"] = "Fornecedor cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Provider", "Edit")]
        [Route("Editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var providerViewModel = await GetById(id, true, true);

            if (providerViewModel == null) return NotFound();

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Edit")]
        [Route("Editar/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProviderViewModel obj)
        {
            if (id != obj.Id) return NotFound();

            if (!ModelState.IsValid) return View(obj);

            obj.DocumentNumber = obj.DocumentNumber.Replace(".", "").Replace("-", "").Replace("/", "");

            var provider = _mapper.Map<Provider>(obj);
            await _providerService.Update(provider);
            if (!ValidOperation()) return View(obj);

            TempData["Success"] = "Fornecedor atualizado com sucesso.";
            return RedirectToAction(nameof(Index));

        }

        [ClaimsAuthorize("Provider", "Del")]
        [Route("Excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var providerViewModel = await GetById(id, false, false);

            if (providerViewModel == null) return NotFound();

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Del")]
        [Route("Excluir/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var providerViewModel = await GetById(id, true, false);

                if (providerViewModel == null) return NotFound();

                await _providerService.Remove(id);
                if (!ValidOperation()) return View(providerViewModel);

                TempData["Success"] = "Fornecedor excluído com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException ex)
            {
                var providers = _mapper.Map<List<ProviderViewModel>>(await _providerRepository.FindAll(true));
                ViewData.ModelState.AddModelError(string.Empty, $"{ex.Message} existem produtos pertencentes a este fornecedor.");
                return View("Index", providers.OrderBy(p => p.Name));
            }
        }

        [ClaimsAuthorize("Provider", "EditAddress")]
        [Route("Editar-endereco/{providerId:guid}")]
        public async Task<IActionResult> EditAddress(Guid providerId)
        {
            var provider = await GetById(providerId, true, false);
            if (provider == null)
            {
                return NotFound();
            }

            return PartialView("_UpdateAddress", new ProviderViewModel { Address = provider.Address });
        }

        [ClaimsAuthorize("Provider", "EditAddress")]
        [Route("Editar-endereco/{providerId:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAddress(ProviderViewModel viewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("DocumentNumber");
            if (ModelState.IsValid)
            {
                await _providerService.UpdateAddress(_mapper.Map<Address>(viewModel.Address));
                if (!ValidOperation()) return PartialView("_UpdateAddress", viewModel);

                var url = Url.Action("GetAddress", "Providers", new { providerId = viewModel.Address.ProviderId });
                return Json(new { success = true, url });
            }

            return PartialView("_UpdateAddress", viewModel);
        }

        private async Task<ProviderViewModel> GetById(Guid id, bool address, bool products)
        {
            if (address)
            {
                if (products)
                    return _mapper.Map<ProviderViewModel>(await _providerRepository.FindById(id, true, true));
                return _mapper.Map<ProviderViewModel>(await _providerRepository.FindById(id, true, false));
            }
            return _mapper.Map<ProviderViewModel>(await _providerRepository.FindById(id));
        }
    }
}