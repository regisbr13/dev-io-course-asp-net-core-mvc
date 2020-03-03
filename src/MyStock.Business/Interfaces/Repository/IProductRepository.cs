using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyStock.Business.Models;

namespace MyStock.Business.Interfaces.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
         Task<List<Product>> FindByProvider(Guid providerId);
         Task<List<Product>> FindAll(bool provider, bool category);
         Task<Product> FindById(Guid id, bool provider, bool category);
    }
}