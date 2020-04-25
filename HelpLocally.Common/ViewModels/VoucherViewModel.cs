using System;
using System.Collections.Generic;
using System.Text;

namespace HelpLocally.Common.ViewModels
{
    public class VoucherViewModel
    {
        public Guid Id { get; set; }

        public decimal OriginalAmount { get; set; }

        public decimal CurrentAmount { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid CompanyId { get; set; }
		
        public string CompanyName { get; set; }

        public string OwnerLogin { get; set; }
    }
}
