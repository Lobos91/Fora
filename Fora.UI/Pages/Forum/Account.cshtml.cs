using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fora.UI.Pages.Forum
{
    public class AccountModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        public string UserName { get; set; }

        public AccountModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager; 
        }

        public async Task OnGet()
        {
            //Hämta usern som är inloggad
            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            UserName = currentUser.UserName;
        }
    }
}
