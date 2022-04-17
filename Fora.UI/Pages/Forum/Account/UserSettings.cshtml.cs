using Fora.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fora.UI.Pages.Forum.Account
{
    public class UserSettings : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        public string UserName { get; set; }

        //Hämta alla nödvändiga data från api -> DB
        public ApiManager apimanager { get; set; } = new();
        public List<InterestModel> Interests { get; set; }
        public List<UserInterestModel> UserInterests { get; set; }
        public List<ThreadModel> Threads { get; set; }

        public UserSettings(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager; 
        }

        public async Task OnGet()
        {
            //Hämta usern som är inloggad
            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            UserName = currentUser.UserName;

            // Hämta alla interests
            Interests = await apimanager.GetInterests();

            //Hämta interests som tillhör usern
            UserInterests = await apimanager.GetUserInterests();

            //Get all threads som tillhör current usern
            var allthreads = await apimanager.ReturnAllThreads();
            Threads = allthreads.Where(x => x.User.Username == UserName).ToList();


           


        }
    }
}
