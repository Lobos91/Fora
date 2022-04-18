using Fora.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fora.UI.Pages.Forum
{
    public class TopicModel : PageModel
    {
        public ThreadModel Topic { get; set; } 
        public ApiManager apiManager{ get; set; } = new ApiManager();
        public async Task<IActionResult> OnGet(int id)
        {
            if(id != null)
            {
                Topic = await apiManager.GetOneThread(id);
            }

            return Page();
        }
    }
}
