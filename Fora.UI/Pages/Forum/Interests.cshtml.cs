using Fora.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fora.UI.Pages.Forum
{
    public class InterestsModel : PageModel
    {
        public List<InterestModel> Interests { get; set; }
        public async Task OnGet()
        {
            ApiManager apiManager = new();
            // Get all interests
            Interests = await apiManager.GetInterests();
        }
    }
}
