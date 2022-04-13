using Fora.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fora.UI.Pages.Forum
{
    [BindProperties]
    public class ThreadsModel : PageModel
    {
        public ThreadModel Thread { get; set; }
        public async void OnGet(int id)
        {
            ApiManager apiManager = new();
            // Get the specific thread with the id

            Thread = await apiManager.GetOneThread(id);
           
        }

        public async Task<IActionResult> OnPost()
        {
            ApiManager apiManager = new();

            await apiManager.PostThread(Thread);

            if (ModelState.IsValid)
            {

            }

            return Page();
        }

    }
}
