using HelpLocally.Application;
using HelpLocally.Common.InputModels;
using HelpLocally.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelpLocally.Web.Pages.Account
{
    public class SignInModel : PageModel
    {
        private readonly UserService _userService;
        private readonly IPasswordHasher<User> _hasher;

        public SignInModel(UserService userService, IPasswordHasher<User> _hasher)
        {
            _userService = userService;
            this._hasher = _hasher;
        }

        public void OnGet()
        {

        }


        public async Task<IActionResult> OnGetLogout()
        {
            await this.HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public async Task<IActionResult> OnPost(LoginInput input)
        {
            var user = await _userService.TryFindUserByLoginAsync(input.Login);

            if (user == null)
                return RedirectToPage("SignIn");

            var check_pw = _hasher.VerifyHashedPassword(user, user.PasswordHash, input.Password);

            if (check_pw != PasswordVerificationResult.Success)
            {
                return RedirectToPage("SignIn");
            }

            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login)
            };

            foreach (var role in user.UserRoles.Select(x => x.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await this.HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);

            if (!string.IsNullOrWhiteSpace(input.ReturnUrl) && Url.IsLocalUrl(input.ReturnUrl))
            {
                return LocalRedirect(input.ReturnUrl);
            }

            return LocalRedirect("/");
        }
    }
}