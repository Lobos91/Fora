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
        [BindProperty(SupportsGet = true)]
        public InterestModel Interest { get; set; }
      
        public int UserID { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<ThreadModel> Threads { get; set; } 
        [BindProperty]
        public ThreadModel ThreadToPost { get; set; }

        ApiManager apiManager = new();
        public async Task<IActionResult>  OnGet(int id)
        {
            if(id != null)  //Just in order to load all data, otherwise it jumps to view too fast 
            {
                var interests = await apiManager.GetInterests();
                Interest = interests.First(x => x.Id == id);

                  var threads = await apiManager.ReturnAllThreads();
                 Threads = threads.Where(t => t.InterestId == id).ToList();
            }
            return Page();

        }
        public async Task<IActionResult> OnPost()
        {
            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            var users = await apiManager.GetUsers();
            var user =  users.FirstOrDefault(u => u.Username == currentUser.UserName);
            UserID = user.Id;

            ThreadToPost.UserId = UserID;
            ThreadToPost.Messages[0].UserId = UserID;
            ThreadToPost.InterestId = Interest.Id;

            await apiManager.PostThread(ThreadToPost);

            return Page();
          // return RedirectToPage("/Forum/Threads" + "?id=17");
        }

    }
}
