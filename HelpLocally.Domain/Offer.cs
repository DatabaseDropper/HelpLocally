using System;
using System.Collections.Generic;
using System.Text;

namespace HelpLocally.Domain
{
    public class Offer
    {
        public Offer(string name, string description, OfferType type, decimal price, Company company)
        {
            Name = name;
            Description = description;
            Type = type;
            Price = price;
            Company = company;
        }

        private Offer()
        {

        }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Description { get; set; }

        public int? TypeId { get; set; }

        public OfferType Type { get; set; }

        public decimal Price { get; set; }

        public Guid? CompanyId { get; set; }

        public Company Company { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsSold { get; set; } = false;
    }
}
