using System;
using System.Threading.Tasks;
using MyStock.Business.Models;

namespace MyStock.Business.Interfaces.Repository
{
    public interface IAddressRepository : IRepository<Address>
    {
         Task<Address> FindByProvider(Guid providerId);
    }
}