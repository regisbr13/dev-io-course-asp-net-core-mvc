using System;
using System.Threading.Tasks;
using MyStock.Business.Models;

namespace MyStock.Business.Interfaces.Services
{
    public interface IProviderService : IDisposable
    {
        Task Insert(Provider product);
        Task Update(Provider product);
        Task UpdateAddress(Address address);
        Task Remove(Guid id);
    }
}