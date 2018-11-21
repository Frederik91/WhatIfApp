using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WhatIf.Shared
{
    public class RestClientWrapper : IRestClientWrapper
    {
        private readonly HttpClient _httpClient;

        public RestClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(Guid id, string urlEnding)
        {
            HttpResponseMessage response = null;
            if (id == Guid.Empty)
                response = await _httpClient.GetAsync(urlEnding);
            
            else
                response = await _httpClient.GetAsync(urlEnding + id);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> GetAsync<T>(string text, string urlEnding)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(urlEnding + text);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }
    }
}
