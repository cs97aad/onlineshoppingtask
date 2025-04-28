using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;

namespace AutomationExerciseTests.Utilities
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://automationexercise.com/");
        }

        public async Task<HttpResponseMessage> CreateAccountAsync(Dictionary<string, string> userData)
        {
            var content = new FormUrlEncodedContent(userData);
            var response = await _client.PostAsync("api/createAccount", content);
            return response;
        }
    }
}
