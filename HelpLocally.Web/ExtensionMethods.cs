using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelpLocally.Web
{
    public static class ExtensionMethods
    {
        public static Guid? GetUserId(this PageModel p)
        {
            var str = p.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(str))
            {
                return new Guid(str);
            }

            return null;
        }
    }
}
