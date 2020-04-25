using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpLocally.Common;
using HelpLocally.Common.InputModels;
using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpLocally.Web.Pages.Companies
{
    [Authorize(Roles = Roles.Admin)]
    public class RegisterModel : PageModel
    {
        private readonly HelpLocallyContext _context;
        private readonly IPasswordHasher<User> _hasher;

        public RegisterModel(HelpLocallyContext context, IPasswordHasher<User> hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public string Message = "";
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(LoginInput input)
        {
            var validation = ValidateFluently(input);

            var user = new User(input.Login);
            user.PasswordHash = _hasher.HashPassword(user, input.Password);

            var role = _context.Roles.First(x => x.Name == Roles.Customer);

            var userRole = new UserRole
            {
                Role = role,
                User = user
            };

            user.UserRoles.Add(userRole);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return Redirect("/");
        }

        private (bool Success, string Error) ValidateFluently(LoginInput input)
        { 
            if (string.IsNullOrWhiteSpace(input.Login))
                return (false, "User login is empty");     
            
            if (string.IsNullOrWhiteSpace(input.Password))
                return (false, "User password is empty");

            return (true, "");
        }
    }
}