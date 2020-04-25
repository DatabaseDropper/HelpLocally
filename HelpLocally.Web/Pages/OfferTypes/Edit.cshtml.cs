using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpLocally.Domain;
using HelpLocally.Common;
using Microsoft.AspNetCore.Authorization;

namespace HelpLocally.Web.Pages.OfferTypes
{
    [Authorize(Roles = Roles.Admin)]
    public class EditModel : PageModel
    {
        private readonly HelpLocally.Infrastructure.HelpLocallyContext _context;

        public EditModel(HelpLocally.Infrastructure.HelpLocallyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OfferType OfferType { get; set; }

        [BindProperty]
        public int? Id { get; set; }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Id == null)
            {
                return Page();
            }

            var old = await _context.OfferTypes.FirstOrDefaultAsync(m => m.Id == Id);

            if (old == null)
                return Page();

            old.Description = OfferType.Description;
            old.Name = OfferType.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferTypeExists(OfferType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OfferTypeExists(int id)
        {
            return _context.OfferTypes.Any(e => e.Id == id);
        }
    }
}
