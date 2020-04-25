using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using HelpLocally.Common;

namespace HelpLocally.Web.Pages.OfferTypes
{
    [Authorize(Roles = Roles.Admin)]
    public class IndexModel : PageModel
    {
        private readonly HelpLocally.Infrastructure.HelpLocallyContext _context;

        public IndexModel(HelpLocally.Infrastructure.HelpLocallyContext context)
        {
            _context = context;
        }

        public IList<OfferType> OfferType { get;set; }

        public async Task OnGetAsync()
        {
            OfferType = await _context.OfferTypes.ToListAsync();
        }
    }
}
