using Fora.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fora.UI.Pages.Forum.Account
{
    public class UserSettings : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        public string UserName { get; set; }

        public ApiManager apimanager { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public List<InterestModel> Interests { get; set; } = new();
        public List<UserInterestModel> UserInterests { get; set; } = new();
        public List<ThreadModel> Threads { get; set; } = new();
        public UserInterestModel UserInterest { get; set; }

        public IEnumerable<SelectListItem> InterestToChose { get; set; }

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

            // dropdown list for adding a interest
            InterestToChose = Interests.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });

        }
        // User Interest
        public async Task<IActionResult> OnPostDeleteUserInterest(int id)
        {
            var userInterests = await apimanager.GetUserInterests();
            var userInterest = userInterests.ToList().FirstOrDefault(x => x.InterestId == id);

            if (userInterest == null)
            {
                return NotFound();
            }

            await apimanager.RemoveUserInterest(id);

            return RedirectToPage("/Forum/Account/UserSettings");
        }

        //App interest
        public async Task<IActionResult> OnPostDeleteInterest(int id)
        {
            var interests = await apimanager.GetInterests();
            var interest = interests.ToList().FirstOrDefault(x => x.Id == id);

            if (interest == null)
            {
                return NotFound();
            }

            await apimanager.RemoveInterest(id);

            return RedirectToPage("/Forum/Account/UserSettings");
        }

        public async Task<IActionResult> OnPostAddUserInterest()
        {
            if(ModelState.IsValid)
            {

                await apimanager.AddUserInterest(UserInterest);
            }

            return RedirectToPage("/Forum/Account/UserSettings");
        }
    }
}
