using Fora.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fora.UI.Pages.Forum
{
    public class TopicModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;

        public TopicModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public ThreadModel Topic { get; set; } = new();
        [BindProperty] public MessageModel NewMessage { get; set; } = new();
        public ApiManager apiManager{ get; set; } = new ApiManager();
        [BindProperty(SupportsGet = true)] public UserModel User { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            if (id != null)
            {
                Topic = await apiManager.GetOneThread(id);
            }

            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            var users = await apiManager.GetUsers();
            User = users.FirstOrDefault(u => u.Username == currentUser.UserName);

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            var users = await apiManager.GetUsers();
            var user = users.FirstOrDefault(u => u.Username == currentUser.UserName);

            if (ModelState.IsValid)
            {
                NewMessage.ThreadId = id;
                NewMessage.UserId = user.Id;
              
                await apiManager.AddMessage(NewMessage);
            }

            //return RedirectToPage("/Forum/Topic");
            TempData["success"] = "Your post was successfully added.";
            return RedirectToPage("/Forum/Threads");

            
        }

        public async Task<IActionResult> OnPostDeletePost(int id)
        {
            await apiManager.DeleteMessage(id);
            TempData["success"] = "Your post was successfully deleted";
            return RedirectToPage("/Forum/Threads");
        }
    }
}
