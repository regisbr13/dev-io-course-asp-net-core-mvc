using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStock.Business.Interfaces.Repository;
using MyStock.Business.Models;
using MyStock.Data.Context;

namespace MyStock.Data.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(MyContext context) : base(context)
        {
        }

        public async Task<List<Provider>> FindAll(bool address)
        {
            if (address)
            {

                return await _context.Providers.AsNoTracking().Include(x => x.Address).ToListAsync();
            }
            return await FindAll();
        }

        public async Task<Provider> FindById(Guid id, bool address, bool product)
        {
            if (address)
            {
                if (product)
                    return await _context.Providers.AsNoTracking().Include(x => x.Address).Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
                return await _context.Providers.AsNoTracking().Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
            }
            return await FindById(id);
        }
    }
}