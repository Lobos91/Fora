 using Fora.UI.Model;
using Newtonsoft.Json;

namespace Fora.UI.Data
{
    public class ApiManager
    {
        private string urlThreads = "https://localhost:7119/api/Thread";
        private string urlMessages = "https://localhost:7119/api/Message";
        private string urlInterests = "https://localhost:7119/api/Interest";

        public async Task<ThreadModel> ReturnAllThreads()
        {
            using (HttpClient client = new())
            {
                using (var response = await client.GetAsync(urlThreads))
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ThreadModel>(strResponse);
                }
            }

            return null;
        }

    }
}
