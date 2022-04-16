using Fora.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fora.UI.Pages.Forum
{
    public class ThreadsModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public ThreadsModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public List<ThreadModel> Threads { get; set; }
        [BindProperty]
        public ThreadModel ThreadToPost { get; set; }

        ApiManager apiManager = new();
        public async void OnGet(int id)
        {
            // Get the specific thread with the id
           var threads = await apiManager.ReturnAllThreads();
           Threads = threads.Where(t => t.InterestId == id).ToList();
            
        }
        public async Task<IActionResult> OnPost()
        {
            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            var users = await apiManager.GetUsers();
            var user =  users.FirstOrDefault(u => u.Username == currentUser.UserName);

            ThreadToPost.User = user;
            await apiManager.PostThread(ThreadToPost);


            return Page();
        }

    }
}
