using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipeSharingProject.Client.Pages
{
    public class LoginModel : PageModel
    {
        public IActionResult OnPost()
        {
            var redirectUrl = Url.Page("/Index");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, MicrosoftAccountDefaults.AuthenticationScheme);
        }
    }
}
