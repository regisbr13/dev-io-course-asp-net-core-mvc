using System;
using System.Threading.Tasks;
using MyStock.Business.Interfaces;
using MyStock.Business.Interfaces.Repository;
using MyStock.Business.Interfaces.Services;
using MyStock.Business.Models;
using MyStock.Business.Models.Validations;

namespace MyStock.Business.Services
{   public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, INotifier notifier) : base(notifier)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Insert(Category category)
        {
            if (!ExecuteValidation(new CategoryValidation(), category)) return;

            await _categoryRepository.Insert(category);
        }

        public async Task Remove(Guid id)
        {
            await _categoryRepository.Remove(id);
        }

        public async Task Update(Category category)
        {
            if (!ExecuteValidation(new CategoryValidation(), category)) return;

            await _categoryRepository.Update(category);
        }
    }
}