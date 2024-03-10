using Client.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class LoginModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string redirectUri)
        {
            if (redirectUri.IsMissing())
            {
                redirectUri = Url.Content("~/");
            }

            if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
            {
                Response.Redirect(redirectUri);
            }

            return Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = redirectUri
                }, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
