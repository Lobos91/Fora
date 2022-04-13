 using Fora.UI.Model;
using Newtonsoft.Json;

namespace Fora.UI.Data
{
    public class ApiManager
    {
        private string urlThreads = "https://localhost:7119/api/Thread";
        private string urlThreadId = "https://localhost:7119/api/Thread/Id";
        private string urlMessages = "https://localhost:7119/api/Message";
        private string urlInterests = "https://localhost:7119/api/Interest";


        public async Task<List<ThreadModel>> ReturnAllThreads()
        {
            using (HttpClient client = new())
            {
                var response = await client.GetFromJsonAsync<List<ThreadModel>>(urlThreads);
                return response;
            }

            return null;
        }

        // Return one thread
        public async Task<ThreadModel> GetOneThread(int id)
        {
            using (HttpClient client = new())
            {
                var response = await client.GetFromJsonAsync<List<ThreadModel>>(urlThreadId);
                var getThread = response.FirstOrDefault(x => x.Id == id);
                
                return getThread;
            }

            return null;
        }

        // Post
        public async Task<ThreadModel> PostThread(ThreadModel thread)
        {
            using (HttpClient client = new())
            {
                using (var response = await client.PostAsJsonAsync<ThreadModel>(urlThreads, thread))
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ThreadModel>(strResponse);
                }
            }

            return null;
        }

        public async Task<List<InterestModel>> GetInterests()
        {
            using (HttpClient client = new())
            {
                var response = await client.GetFromJsonAsync<List<InterestModel>>(urlInterests);
                return response;
            }

            return null;
        }
    }
}
