using System;
using System.Collections.Generic;
using System.Text;

namespace HelpLocally.Common.InputModels
{
    public class CreateCompanyInput
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Nip { get; set; }

        public string BankAccountNumber { get; set; }
    }
}
