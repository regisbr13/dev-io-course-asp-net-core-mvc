using MyStock.Business.Models.Enums;
using System.Collections.Generic;

namespace MyStock.Business.Models
{
    public class Provider : BaseEntity
    {
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
        public ProviderType ProviderType { get; set; }
        public bool Active { get; set; }
        public Address Address { get; set; }
        public List<Product> Products { get; set; }
    }
}
