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
        public UserSettings(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public ApiManager apiManager = new();
        public UserModel User { get; set; } = new();
        public List<ThreadModel> Threads { get; set; } = new();
        [BindProperty(SupportsGet = true)] public List<InterestModel> Interests { get; set; } = new();
        [BindProperty] public List<UserInterestModel> UserInterests { get; set; } = new();
        [BindProperty] public UserInterestModel UserInterest { get; set; } = new();
        public IEnumerable<SelectListItem> InterestToChose { get; set; }

      
        public async Task OnGet()
        {
            //Hämta usern som är inloggad
            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            var users = await apiManager.GetUsers();
            User = users.FirstOrDefault(x => x.Username == currentUser.UserName);

            // Hämta alla interests
            Interests = await apiManager.GetInterests();
           
            
            //Hämta interests som tillhör usern
            var allUserInterests = await apiManager.GetUserInterests();
            UserInterests = allUserInterests.Where(x => x.UserId == User.Id).ToList();
         
            //Get all threads som tillhör current usern
            var allthreads = await apiManager.ReturnAllThreads();
            Threads = allthreads.Where(x => x.User.Username == User.Username).ToList();

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
            await apiManager.RemoveUserInterest(id);

            return RedirectToPage("/Forum/Account/UserSettings");
        }

        //App interest
        public async Task<IActionResult> OnPostDeleteInterest(int id)
        {
          //  var interests = await apiManager.GetInterests();
          //  var interest = interests.ToList().FirstOrDefault(x => x.Id == id);

            //if (interest == null)
            //{
            //    return NotFound();
            //}

            await apiManager.RemoveInterest(id);

            return RedirectToPage("/Forum/Account/UserSettings");
        }

        public async Task<IActionResult> OnPostAddUserInterest()
        {
            // Following 3 lines of code are neccessary, without them User.Id is always 0 (idk why)
            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            var users = await apiManager.GetUsers();
            User = users.FirstOrDefault(x => x.Username == currentUser.UserName);

            if (ModelState.IsValid)
            {
                UserInterest.UserId = User.Id; 
                await apiManager.AddUserInterest(UserInterest);
            }

            return RedirectToPage("/Forum/Account/UserSettings");
        }
    }
}
