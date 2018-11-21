
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.Session
{
    public class UserUsecase : IUserUsecase
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverApiBaseUrl = "http://localhost:53562/api/";

        public UserUsecase()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverApiBaseUrl) };
        }

        public async Task<string> GetUsers()
        {
            //var request = new RestRequest("/User/{id}", Method.GET);
            //var cancellationTokenSource = new CancellationTokenSource();
            //request.AddUrlSegment("id", "1337");

            //var response =  await httpClient.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            //var content = response.Content;
            //return content;
            string result = "Default";
            var response = await _httpClient.GetAsync("User/1337");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
