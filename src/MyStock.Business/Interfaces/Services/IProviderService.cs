using System;
using System.Threading.Tasks;
using MyStock.Business.Models;

namespace MyStock.Business.Interfaces.Services
{
    public interface IProviderService : IDisposable
    {
        Task Insert(Provider provider);
        Task Update(Provider provider);
        Task UpdateAddress(Address address);
        Task Remove(Guid id);
    }
}