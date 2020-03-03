using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyStock.Business.Models;

namespace MyStock.Business.Interfaces.Repository
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<List<Provider>> FindAll(bool address);
        Task<Provider> FindById(Guid id, bool address, bool product);
    }
}