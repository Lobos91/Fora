using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Fora.UI.Pages.Forum
{
    [BindProperties]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(bool logMeOut)
        {
           if (logMeOut)
           {
                await _signInManager.SignOutAsync();
           }

           return RedirectToPage("/Index");
        }
    }
}
