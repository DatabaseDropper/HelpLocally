using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelpLocally.Domain;
using HelpLocally.Common;
using Microsoft.AspNetCore.Authorization;

namespace HelpLocally.Web.Pages.OfferTypes
{
    [Authorize(Roles = Roles.Admin)]
    public class CreateModel : PageModel
    {
        private readonly Infrastructure.HelpLocallyContext _context;

        public CreateModel(Infrastructure.HelpLocallyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public OfferType OfferType { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OfferTypes.Add(OfferType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
