using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStock.Business.Interfaces;
using MyStock.Business.Interfaces.Repository;
using MyStock.Business.Interfaces.Services;
using MyStock.Business.Models;
using MyStock.ViewModels;

namespace MyStock.Controllers
{
    [Route("Produtos")]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IProviderRepository _providerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IProductService productService, IProviderRepository providerRepository, ICategoryRepository categoryRepository, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
            _productService = productService;
            _providerRepository = providerRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var products = _mapper.Map<List<ProductViewModel>>(await _productRepository.FindAll(true, true));
            return View(products.OrderBy(x => x.Name));
        }

        [Route("Detalhes/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await GetById(id, true, true);
            
            if (product == null) return NotFound();

            return View(product);
        }

        [Route("Novo")]
        public async Task<IActionResult> Create()
        {
            return View(await GenerateFormViewModel(new ProductViewModel { Image = "product.jpg" }));
        }

        [Route("Novo")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel obj)
        {
            if (!ModelState.IsValid) return View(await GenerateFormViewModel(obj));

            var imgPrefix = Guid.NewGuid() + "_";

            if (!await UploadFile(obj.ImageUpload, imgPrefix)) obj.Image = "product.jpg";
            
            else obj.Image = imgPrefix + obj.ImageUpload.FileName;

            await _productService.Insert(_mapper.Map<Product>(obj));
            if (!ValidOperation()) return View(await GenerateFormViewModel(obj));

            TempData["Success"] = "Produto cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [Route("Editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetById(id, true, true);

            if (productViewModel == null) return NotFound();

            return View(await GenerateFormViewModel(productViewModel));
        }
        
        [Route("Editar/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel obj)
        {
            if (id != obj.Id) return NotFound();

            var productBase = await GetById(id, false, false);
            obj.Provider = productBase.Provider;
            obj.Image = productBase.Image;

            if (!ModelState.IsValid) return View(obj);

            if (obj.ImageUpload != null) 
            {
                DeleteImg(obj.Image);

                var imgPrefix = Guid.NewGuid() + "_";

                if (!await UploadFile(obj.ImageUpload, imgPrefix)) return View(await GenerateFormViewModel(obj));

                productBase.Image = imgPrefix + obj.ImageUpload.FileName;
            }
            productBase.Name = obj.Name;
            productBase.Description = obj.Description;
            productBase.Value = obj.Value;
            productBase.CategoryId = obj.CategoryId;
            productBase.Active = obj.Active;

            var product = _mapper.Map<Product>(productBase);
            await _productService.Update(product);
            if (!ValidOperation()) return View(await GenerateFormViewModel(obj));

            TempData["Success"] = "Produto atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [Route("Excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = await GetById(id, true, true);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        [Route("Excluir/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productViewModel = await GetById(id, false, false);

            if (productViewModel == null) return NotFound();

            DeleteImg(productViewModel.Image);
            await _productService.Remove(id);
            if (!ValidOperation()) return View(productViewModel);

            TempData["Success"] = "Produto excluído com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<ProductViewModel> GetById(Guid id, bool provider, bool category)
        {
            if (provider) 
            {
                if (category)
                    return _mapper.Map<ProductViewModel>(await _productRepository.FindById(id, true, true));
                return _mapper.Map<ProductViewModel>(await _productRepository.FindById(id, true, false));
            }
            return _mapper.Map<ProductViewModel>(await _productRepository.FindById(id));
        }

        private async Task<ProductViewModel> GenerateFormViewModel(ProductViewModel obj)
        {
            var providersViewModel = _mapper.Map<List<ProviderViewModel>>(await _providerRepository.FindAll());
            var categoriesViewModel = _mapper.Map<List<CategoryViewModel>>(await _categoryRepository.FindAll());
            obj.Categories = categoriesViewModel;
            obj.Providers = providersViewModel;

            return obj;
        }

        private async Task<bool> UploadFile(IFormFile img, string imgPrefix)
        {
            if (img == null) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + img.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(String.Empty, "Já existe uma imagem com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await img.CopyToAsync(stream);                       
            }
            return true;
        }

        private void DeleteImg(string path)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", path);

            System.IO.File.Delete(fullPath);
        }
    }
}