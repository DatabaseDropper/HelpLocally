using System;

namespace HelpLocally.Domain
{
    public class Company
    {
        private Company()
        {

        }

        public Company(string name, string nip, string bankAccountNumber, User companyOwner)
        {
            Name = name;
            Nip = nip;
            BankAccountNumber = bankAccountNumber;
            CompanyOwner = companyOwner;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Nip { get; set; }

        public string BankAccountNumber { get; set; }

        public bool IsDeleted { get; set; } = false;

        public User CompanyOwner { get; set; }

        public Guid? CompanyOwnerId { get; set; }
    }
}