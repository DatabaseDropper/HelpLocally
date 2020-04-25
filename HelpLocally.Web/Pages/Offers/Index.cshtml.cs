using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using HelpLocally.Common;
using Microsoft.AspNetCore.Authorization;

namespace HelpLocally.Web.Pages.Offers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly HelpLocallyContext _context;

        public IndexModel(HelpLocallyContext context)
        {
            _context = context;
        }

        public IList<Offer> Offer { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIsCompany = this.User.IsInRole(Roles.Company);

            var id = this.GetUserId();

            if (id == null)
            {
                return Page();
            }

            Offer = await _context
                            .Offers
                            .Include(o => o.Type)
                            .Include(x => x.Company)
                            .Where(x => 
                                x.IsSold == false &&
                                x.IsDeleted == false && 
                                (userIsCompany ? x.Company.CompanyOwnerId == id : true))
                            .ToListAsync();

            return Page();
        }
    }
}
