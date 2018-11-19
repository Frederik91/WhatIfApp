
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.Session
{
    public class UserUsecase : IUserUsecase
    {
        private HttpClient httpClient;
        private string serverApiBaseUrl = "http://localhost:53562/api/";

        public UserUsecase()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serverApiBaseUrl);
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
            var response = await httpClient.GetAsync("User/1337");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
          
            return result;
        }
    }
}
