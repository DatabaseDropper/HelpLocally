using System;
using System.Collections.Generic;
using System.Text;

namespace HelpLocally.Domain
{
    public class User
    {
        private User()
        {

        }

        public User(string login)
        {
            Login = login;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
    }
}
