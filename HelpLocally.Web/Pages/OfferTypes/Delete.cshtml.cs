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

namespace HelpLocally.Web.Pages.OfferTypes
{
    [Authorize(Roles = Roles.Admin)]
    public class DeleteModel : PageModel
    {
        private readonly HelpLocally.Infrastructure.HelpLocallyContext _context;

        public DeleteModel(HelpLocally.Infrastructure.HelpLocallyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OfferType OfferType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OfferType = await _context.OfferTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (OfferType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OfferType = await _context.OfferTypes.FindAsync(id);

            if (OfferType != null)
            {
                _context.OfferTypes.Remove(OfferType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
