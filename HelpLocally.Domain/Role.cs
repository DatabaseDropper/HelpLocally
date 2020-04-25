using System;
using System.Collections.Generic;

namespace HelpLocally.Domain
{
    public class Role
    {
        private Role()
        {

        }

        public Role(string name)
        {
            Name = name;
        }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}