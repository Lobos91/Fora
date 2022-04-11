using Fora.UI.Model;
using Newtonsoft.Json;

namespace Fora.UI.Data
{
    public class ApiManager
    {
        private string url = "";

        public async Task<RegistrationModel> ReturnRandomActivityFromApi()
        {
            using (HttpClient client = new())
            {
                using (var response = await client.GetAsync(url))
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RegistrationModel>(strResponse);
                }
            }

            return null;
        }

    }
}
