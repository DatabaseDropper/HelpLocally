using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HelpLocally.Application
{
    public class UserService
    {
        private readonly HelpLocallyContext _context;

        public UserService(HelpLocallyContext context)
        {
            _context = context;
        }

        public async Task<User> TryFindUserByIdAsync(Guid? id)
        {
            if (id == null)
                return null;

            return await _context
                         .Users
                         .Include(x => x.UserRoles)
                         .ThenInclude(x => x.Role)
                         .FirstOrDefaultAsync(x => x.Id == id);
        }  
        
        public async Task<User> TryFindUserByLoginAsync(string login)
        {
            return await _context
                         .Users
                         .Include(x => x.UserRoles)
                         .ThenInclude(x => x.Role)
                         .FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}
