using Fora.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fora.UI.Pages.Forum
{
    public class InterestsModel : PageModel
    {
        public List<InterestModel> Interests { get; set; } = new();
       [BindProperty] public InterestModel Interest { get; set; } = new InterestModel();
        public ApiManager apiManager { get; set; } = new ApiManager();
        public async Task OnGet()
        {
          
            // Get all interests
            Interests = await apiManager.GetInterests();
        }

        public async Task<IActionResult> OnPost()
        {
            await apiManager.AddInterest(Interest);
            Interests = await apiManager.GetInterests();
            return Page();
        }
         
    }
}
