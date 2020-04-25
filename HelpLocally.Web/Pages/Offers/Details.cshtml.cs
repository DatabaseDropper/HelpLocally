using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using HelpLocally.Application;
using Microsoft.AspNetCore.Authorization;
using HelpLocally.Common;

namespace HelpLocally.Web.Pages.Offers
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly HelpLocallyContext _context;
        private readonly OfferService _offerService;
        private readonly UserService _userService;
        public string Message = "";

        public DetailsModel(HelpLocallyContext context, OfferService offerService, UserService userService)
        {
            _context = context;
            _offerService = offerService;
            _userService = userService;
        }

        public Offer Offer { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Offer = await _context.Offers.Include(o => o.Type).Include(x => x.Company).FirstOrDefaultAsync(m => m.Id == id && m.IsSold == false && m.IsDeleted == false);

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
                Message = "Offer not found";
                return Page();
            }

            var user = await _userService.TryFindUserByIdAsync(this.GetUserId());
            
            if (user == null)
            {
                Message = "Unauthorized";
                return Page();
            }

            var result = await _offerService.PurchaseOfferAsync(user, id.Value);

            Message = result.Success ?  "Success" : result.Error;
            return Page();
        }
    }
}
