using System;
using System.Collections.Generic;
using System.Text;

namespace HelpLocally.Common.InputModels
{
    public class OffertInput
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? TypeId { get; set; }

        public decimal Price { get; set; }
    }
}
