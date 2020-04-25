using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using HelpLocally.Common;
using Microsoft.AspNetCore.Authorization;
using HelpLocally.Common.InputModels;
using Microsoft.EntityFrameworkCore;

namespace HelpLocally.Web.Pages.Offers
{
    [Authorize(Roles = Roles.Company)]
    public class CreateModel : PageModel
    {
        private readonly HelpLocally.Infrastructure.HelpLocallyContext _context;

        public CreateModel(HelpLocally.Infrastructure.HelpLocallyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["TypeId"] = new SelectList(_context.Set<OfferType>(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public OffertInput Offer { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var type = await _context.OfferTypes.FirstOrDefaultAsync(x => x.Id == Offer.TypeId);

            if (type == null)
                return Page();
            
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyOwnerId == this.GetUserId());

            if (company == null)
                return Page();

            _context.Offers.Add(new Offer(Offer.Name, Offer.Description, type, Offer.Price, company));
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
