using System;
using System.Collections.Generic;
using System.Text;

namespace HelpLocally.Common.InputModels
{
    public class LoginInput
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
