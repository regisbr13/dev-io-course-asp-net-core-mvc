using System;
using System.Threading.Tasks;
using MyStock.Business.Models;

namespace MyStock.Business.Interfaces.Services
{
    public interface IProductService : IDisposable
    {
        Task Insert(Product product);
        Task Update(Product product);
        Task Remove(Guid id);
    }
}