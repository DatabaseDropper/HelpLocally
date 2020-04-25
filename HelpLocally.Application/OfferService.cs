using HelpLocally.Common;
using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HelpLocally.Application
{
    public class OfferService
    {
        private readonly HelpLocallyContext _context;

        public OfferService(HelpLocallyContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Error)> PurchaseOfferAsync(User user, Guid OfferId)
        {
            if (!user.UserRoles.Any(x => x.Role.Name == Roles.Customer))
            {
                return (false, "Not sufficent permissions");
            }

            var offer = await _context
                              .Offers
                              .Include(x => x.Company)
                              .FirstOrDefaultAsync(x => x.Id == OfferId && x.IsSold == false);

            if (offer == null)
            {
                return (false, "Offer not Found");
            }

            var user_paid = TryToGetMoneyFromUsersExternalPayingProviderAsync(user, offer.Price);

            if (!user_paid)
            {
                return (false, "You have to pay via your bank or something");
            }

            offer.IsSold = true;

            var newVoucher = new Voucher(user, offer.Price, offer.Company, DateTime.UtcNow.AddYears(1));

            await _context.Vouchers.AddAsync(newVoucher);
            await _context.SaveChangesAsync();

            return (true, "");
        }

        private bool TryToGetMoneyFromUsersExternalPayingProviderAsync(User user, decimal price)
        {
            return true;
        }
    }
}
