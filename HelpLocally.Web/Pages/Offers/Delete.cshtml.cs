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

namespace HelpLocally.Web.Pages.Offers
{
    [Authorize(Roles = Roles.Company)]
    public class DeleteModel : PageModel
    {
        private readonly HelpLocally.Infrastructure.HelpLocallyContext _context;

        public DeleteModel(HelpLocally.Infrastructure.HelpLocallyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guid? id { get; set; }

        public Offer Offer { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Offer = await _context.Offers.Include(o => o.Type).FirstOrDefaultAsync(m => m.Id == id && m.IsSold == false);

            if (Offer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Offer = await _context.Offers.FirstOrDefaultAsync(x => x.Id == id && x.IsSold == false);

            if (Offer != null)
            {
                Offer.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
