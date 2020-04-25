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
    public class CreateModel : PageModel
    {
        private readonly HelpLocallyContext _context;
        private readonly IPasswordHasher<User> _hasher;

        public CreateModel(HelpLocallyContext context, IPasswordHasher<User> hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public string Message = "";
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(CreateCompanyInput input)
        {
            var validation = ValidateFluently(input);

            if (!validation.Success)
            {
                Message = validation.Error;
                return Page();
            }

            if (_context.Users.Any(x => x.Login.ToLower() == input.Login.ToLower()))
            {
                Message = "User with given login already exists.";
                return Page();
            }

            if (_context.Companies.Any(x => x.Name.ToLower() == input.Name.ToLower()))
            {
                Message = "Company with given name already exists.";
                return Page();
            }

            var user = new User(input.Login);
            user.PasswordHash = _hasher.HashPassword(user, input.Password);
            var role = _context.Roles.First(x => x.Name == Roles.Company);

            var userRole = new UserRole
            {
                Role = role,
                User = user
            };

            user.UserRoles.Add(userRole);
            var company = new Company(input.Name, input.Nip, input.BankAccountNumber, user);

            await _context.AddAsync(company);
            await _context.SaveChangesAsync();

            return Redirect("/");
        }

        private (bool Success, string Error) ValidateFluently(CreateCompanyInput input)
        {
            if (string.IsNullOrWhiteSpace(input.BankAccountNumber))
                return (false, "Bank account number is empty");    
            
            if (string.IsNullOrWhiteSpace(input.Nip))
                return (false, "NIP is empty"); 
            
            if (string.IsNullOrWhiteSpace(input.Name))
                return (false, "Company name is empty");  
            
            if (string.IsNullOrWhiteSpace(input.Login))
                return (false, "User login is empty");     
            
            if (string.IsNullOrWhiteSpace(input.Password))
                return (false, "User password is empty");

            return (true, "");
        }
    }
}