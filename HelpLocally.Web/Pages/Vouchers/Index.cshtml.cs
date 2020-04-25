using HelpLocally.Application;
using HelpLocally.Common;
using HelpLocally.Common.ViewModels;
using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpLocally.Web.Pages.Vouchers
{
    [Authorize(Roles = Roles.CustomerAndCompany)]
    public class IndexModel : PageModel
    {
        private readonly HelpLocallyContext _context;

        public IndexModel(HelpLocallyContext context)
        {
            _context = context;
        }

        public List<VoucherViewModel> Vouchers = new List<VoucherViewModel>();

        public async Task<IActionResult> OnGet()
        {
            var id = this.GetUserId();

            if (id == null)
                return RedirectToPage("/Account/Denied");

            var userIsCompany = this.User.IsInRole(Roles.Company);

            if (this.User.IsInRole(Roles.Company))
            {
                Vouchers = await GetCompanyVouchersAsync(id.Value);
            }
            else if (this.User.IsInRole(Roles.Customer))
            {
                Vouchers = await GetCustomerVouchersAsync(id.Value);
            }

            var query = _context.Vouchers.Include(x => x.Company);

            return Page();
        }

        private async Task<List<VoucherViewModel>> GetCustomerVouchersAsync(Guid id)
        {
            return await _context
                         .Vouchers
                         .Where(x => x.OwnerId == id)
                         .Select(x =>
                         new VoucherViewModel
                         {
                             Id = x.Id,
                             CompanyName = x.Company.Name,
                             ExpirationDate = x.ExpirationDate,
                             OriginalAmount = x.OriginalAmount,
                             CurrentAmount = x.CurrentAmount,
                             CompanyId = x.CompanyId,
                         })
                         .ToListAsync();
        }

        private async Task<List<VoucherViewModel>> GetCompanyVouchersAsync(Guid id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyOwnerId == id);

            if (company == null)
                return new List<VoucherViewModel>();

            return await _context
                         .Vouchers
                         .Where(x => x.CompanyId == company.Id)
                         .Select(x =>
                         new VoucherViewModel
                         {
                             Id = x.Id,
                             ExpirationDate = x.ExpirationDate,
                             OriginalAmount = x.OriginalAmount,
                             CurrentAmount = x.CurrentAmount,
                             CompanyId = x.CompanyId,
                             OwnerLogin = x.Owner.Login
                         })
                         .ToListAsync();
        }
    }
}