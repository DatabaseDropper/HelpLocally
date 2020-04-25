using System;
using System.Collections.Generic;
using System.Text;

namespace HelpLocally.Domain
{
    public class OfferType
    {
        public OfferType(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public OfferType()
        {

        }

        public int Id { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
