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

        //H�mta alla n�dv�ndiga data fr�n api -> DB
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
            //H�mta usern som �r inloggad
            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            UserName = currentUser.UserName;

            // H�mta alla interests
            Interests = await apimanager.GetInterests();

            //H�mta interests som tillh�r usern
            UserInterests = await apimanager.GetUserInterests();

            //Get all threads som tillh�r current usern
            var allthreads = await apimanager.ReturnAllThreads();
            Threads = allthreads.Where(x => x.User.Username == UserName).ToList();


           


        }
    }
}
