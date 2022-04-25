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

        [BindProperty(SupportsGet = true)] public InterestModel Interest { get; set; } = new();
        [BindProperty(SupportsGet = true)] public List<ThreadModel> Threads { get; set; }
        [BindProperty] public ThreadModel ThreadToPost { get; set; }
        [BindProperty(SupportsGet = true)] public string SearchKey { get; set; } = string.Empty;
        public int UserID { get; set; }
       
        ApiManager apiManager = new();

        public async Task<IActionResult>  OnGet(int id)
        {
            if (id != 0)   
            {
                var interests = await apiManager.GetInterests();
                Interest = interests.FirstOrDefault(x => x.Id == id);

                var threads = await apiManager.ReturnAllThreads();
                Threads = threads.Where(t => t.InterestId == Interest.Id).ToList();
            }
            else 
            {
                if (string.IsNullOrEmpty(SearchKey))
                {
                    Threads = await apiManager.ReturnAllThreads();
                }
                else
                {
                    var threads = await apiManager.ReturnAllThreads();
                    Threads = threads.Where(x => x.Name.Contains(SearchKey, StringComparison.CurrentCultureIgnoreCase)).ToList();
                }

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

        //public async Task<IActionResult> OnPostSearch(int id)
        //{
        //    var interests = await apiManager.GetInterests();
        //    Interest = interests.FirstOrDefault(x => x.Id == id);


        //    return Page();
        //}

    }
}
