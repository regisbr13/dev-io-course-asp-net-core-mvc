using System;

namespace MyStock.Business.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Value { get; set; }
        public DateTime Register { get; set; }
        public bool Active { get; set; }
        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
