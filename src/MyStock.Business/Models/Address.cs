using System;

namespace MyStock.Business.Models
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string Cep { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
