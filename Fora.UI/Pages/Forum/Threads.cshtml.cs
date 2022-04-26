using Fora.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [BindProperty(SupportsGet = true)] public List<InterestModel> Interests { get; set; } = new();
        [BindProperty(SupportsGet = true)] public string? SearchKey { get; set; } = string.Empty;
        [BindProperty] public ThreadModel ThreadToPost { get; set; }
        public IEnumerable<SelectListItem> InterestToChose { get; set; }
        [BindProperty]  public int UserID { get; set; }
       
        ApiManager apiManager = new();

        public async Task<IActionResult>  OnGet(int id)
        {

            var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            var users = await apiManager.GetUsers();
            var user = users.FirstOrDefault(u => u.Username == currentUser.UserName);
            UserID = user.Id;

            if (id != 0)   
            {
                Interest = await apiManager.GetOneInterest(id);

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

            Interests = await apiManager.GetInterests();
            InterestToChose = Interests.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });

            return Page();

        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
                var users = await apiManager.GetUsers();
                var user = users.FirstOrDefault(u => u.Username == currentUser.UserName);
                UserID = user.Id;

                ThreadToPost.UserId = UserID;
                ThreadToPost.Messages[0].UserId = UserID;
                ThreadToPost.InterestId = Interest.Id;

                await apiManager.PostThread(ThreadToPost);

            }
            // return Page();
            TempData["success"] = "Your thread was successfully posted";
            return RedirectToPage("/Forum/Threads");
        }

        public async Task<IActionResult> OnPostDeleteTopic(int id)
        {
            await apiManager.RemoveThread(id);
            TempData["success"] = "Your thread was successfully deleted";
            return RedirectToPage("/Forum/Threads");
        }
    }

    

}

