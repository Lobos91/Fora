using Fora.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fora.UI.Pages.Forum
{
    public class UserSettingsModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;

        public UserSettingsModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        ApiManager apiManager = new();
        public void OnGet()
        {


        }
    }
}
