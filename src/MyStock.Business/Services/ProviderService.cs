using System;
using System.Linq;
using System.Threading.Tasks;
using MyStock.Business.Interfaces;
using MyStock.Business.Interfaces.Repository;
using MyStock.Business.Interfaces.Services;
using MyStock.Business.Models;
using MyStock.Business.Models.Validations;

namespace MyStock.Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository, IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }

        public async Task Insert(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider) || !ExecuteValidation(new AddressValidation(), provider.Address)) return;

            if (_providerRepository.Search(p => p.DocumentNumber == provider.DocumentNumber).Result.Any())
            {
                Notify("CNPJ já cadastrado");
                return;
            }

            await _providerRepository.Insert(provider);
        }

        public async Task Remove(Guid id)
        {
            await _providerRepository.Remove(id);  
        }

        public async Task Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider)) return;

            if (_providerRepository.Search(p => p.DocumentNumber == provider.DocumentNumber && p.Id != provider.Id).Result.Any())
            {
                Notify("CNPJ já cadastrado");
                return;
            }

            await _providerRepository.Update(provider);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;


            await _addressRepository.Update(address);
        }

        public void Dispose()
        {
            _addressRepository?.Dispose();
            _providerRepository?.Dispose();
        }
    }
}