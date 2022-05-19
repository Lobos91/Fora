using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Fora.UI.Pages.Forum
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet(string returnUrl)
        {

        }

        public async Task<IActionResult> OnPost()
        {

            if(ModelState.IsValid) 
            {
                // Sign in user

                // Try to sign in
                var signInResult = await _signInManager.PasswordSignInAsync(Username, Password, false, false);

                // Evaluate result of sign in
                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Forum/Interests");
                }
            }

            return Page();
        }
    }
}
