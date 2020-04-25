using System;
using System.Collections.Generic;
using System.Text;

namespace HelpLocally.Domain
{
    public class Voucher
    {
        private Voucher()
        {
        
        }

        public Voucher(User owner, decimal originalAmount, Company company, DateTime expirationDate)
        {
            OriginalAmount = originalAmount;
            CurrentAmount = originalAmount;
            Company = company;
            ExpirationDate = expirationDate;
            Owner = owner;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public decimal OriginalAmount { get; set; } = 0M;

        public decimal CurrentAmount { get; set; } = 0M;

        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public Guid OwnerId { get; set; }

        public User Owner { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
