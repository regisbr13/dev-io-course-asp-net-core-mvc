using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyStock.Business.Interfaces;
using MyStock.Business.Interfaces.Repository;
using MyStock.Business.Interfaces.Services;
using MyStock.Business.Models;
using MyStock.ViewModels;

namespace MyStock.Controllers
{
    [Route("Categorias")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, ICategoryService categoryService, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var categories = _mapper.Map<List<CategoryViewModel>>(await _categoryRepository.FindAll());
            return View(categories.OrderBy(x => x.Name));
        }

        [Route("Detalhes/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var category = await GetById(id, true);
            
            if (category == null) return NotFound();

            return View(category);
        }

        [Route("Nova")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Nova")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel obj)
        {
            if (!ModelState.IsValid) return View(obj);

            var category = _mapper.Map<Category>(obj);
            await _categoryService.Insert(category);

            return RedirectToAction(nameof(Index));
        }

        [Route("Editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryViewModel = await GetById(id, false);

            if (categoryViewModel == null) return NotFound();

            return View(categoryViewModel);
        }

        [Route("Editar/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CategoryViewModel obj)
        {
            if (id != obj.Id) return NotFound();

            if (!ModelState.IsValid) return View(obj);

            var category = _mapper.Map<Category>(obj);
            await _categoryService.Update(category);
            return RedirectToAction(nameof(Index));
            
        }

        [Route("Excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var categoryViewModel = await GetById(id, false);

            if (categoryViewModel == null) return NotFound();

            return View(categoryViewModel);
        }

        [Route("Excluir/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoryViewModel = await GetById(id, false);

            if (categoryViewModel == null) return NotFound();

            await _categoryService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<CategoryViewModel> GetById(Guid id, bool products)
        {
            if (products)
                return _mapper.Map<CategoryViewModel>(await _categoryRepository.FindById(id, true));
            return _mapper.Map<CategoryViewModel>(await _categoryRepository.FindById(id));
        }
    }
}