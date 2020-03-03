using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStock.Business.Interfaces.Repository;
using MyStock.Business.Models;
using MyStock.Data.Context;

namespace MyStock.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(MyContext context) : base(context)
        {
        }

        public async Task<Address> FindByProvider(Guid providerId)
        {
            return await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.ProviderId == providerId);
        }
    }
}