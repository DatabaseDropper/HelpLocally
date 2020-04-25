using System.Collections.Generic;
using System.Threading.Tasks;
using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HelpLocally.Web.Pages.Companies
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly HelpLocallyContext _context;
        public ICollection<Company> Companies { get; set; }

        public IndexModel(HelpLocallyContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Companies = await _context.Companies.ToArrayAsync();
        }
    }
}