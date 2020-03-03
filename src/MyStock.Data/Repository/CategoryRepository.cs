using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStock.Business.Interfaces.Repository;
using MyStock.Business.Models;
using MyStock.Data.Context;

namespace MyStock.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyContext context) : base(context)
        {
        }

        public async Task<Category> FindById(Guid id, bool products)
        {
            return await _context.Categories.AsNoTracking().Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}