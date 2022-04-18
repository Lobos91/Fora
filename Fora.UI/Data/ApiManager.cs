﻿using Newtonsoft.Json;

namespace Fora.UI.Data
{
    public class ApiManager 
    {
        private static readonly HttpClient client = new HttpClient();

        
        private string baseURL = "https://localhost:7119/api/";

        private string urlThreads = "https://localhost:7119/api/Thread";
        private string urlMessages = "https://localhost:7119/api/Message";
        private string urlInterests = "https://localhost:7119/api/Interest";
        private string urlUser =        "https://localhost:7119/api/User";
        private string urlUserInterest = "https://localhost:7119/api/UserInterest";

        // Return one thread
        public async Task<ThreadModel> GetOneThread(int id)
        {
            using (HttpClient client = new())
            {
                var response = await client.GetFromJsonAsync<List<ThreadModel>>(urlThreads);
                var getThread = response.FirstOrDefault(x => x.Id == id);

                return getThread;
            }

            return null;
        }

        public async Task<List<ThreadModel>> ReturnAllThreads()
        {
            using (HttpClient client = new())
            {
                var response = await client.GetFromJsonAsync<List<ThreadModel>>(urlThreads);
                return response;
            }

            return null;
        }
      
        public async Task<ThreadModel> PostThread(ThreadModel thread)
        {
            using (HttpClient client = new())
            {
                using (var response = await client.PostAsJsonAsync<ThreadModel>(baseURL + "Thread", thread))
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ThreadModel>(strResponse);
                }
            }

            return null;
        }
        // get users
        public async Task<List<UserModel>> GetUsers()
        {
            using (HttpClient client = new())
            {
                var response = await client.GetFromJsonAsync<List<UserModel>>(urlUser);
                return response;
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

        public async Task RemoveInterest(int id)
        {
            using (HttpClient client = new())
            {
                var respond = await client.DeleteAsync(urlInterests + "/Delete/" + id);
                respond.EnsureSuccessStatusCode();
            }
        }

        public async Task<List<UserInterestModel>> GetUserInterests()
        {
            using (HttpClient client = new())
            {
                var response = await client.GetFromJsonAsync<List<UserInterestModel>>(urlUserInterest);
                return response;
            }

            return null;
        }


        public async Task RemoveUserInterest(int id)
        {
            using (HttpClient client = new())
            {
               var respond = await client.DeleteAsync(urlUserInterest + "/Delete/" + id);
               respond.EnsureSuccessStatusCode();
            }
        }

        public async Task<UserInterestModel> AddUserInterest(UserInterestModel interest)
        {
            using (HttpClient client = new())
            {
                using (var response = await client.PostAsJsonAsync<UserInterestModel>(urlUserInterest, interest))
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserInterestModel>(strResponse);
                }
            }

            return null;
        }
    }
}
