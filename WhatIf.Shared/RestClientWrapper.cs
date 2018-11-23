using Microsoft.JSInterop;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
                response = await _httpClient.GetAsync("api/" + urlEnding);

            else
                response = await _httpClient.GetAsync("api/" + urlEnding + id);

            var content = await response.Content.ReadAsStringAsync();
            var result = Json.Deserialize<T>(content);
            return result;
        }

        public async Task<T> GetAsync<T>(string text, string urlEnding)
        {
            var response = await _httpClient.GetAsync("api/" + urlEnding + text);

            var content = await response.Content.ReadAsStringAsync();
            var result = Json.Deserialize<T>(content);
            return result;
        }

        public async Task<T> Post<T>(string urlEnding, object payload)
        {
            var myContent = Json.Serialize(payload);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var httpContent = new ByteArrayContent(buffer);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PostAsync("api/" + urlEnding, httpContent);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                throw new HttpRequestException($"Status code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var result = Json.Deserialize<T>(content);
            return result;
        }

        public async Task Put(string urlEnding, object payload)
        {
            var myContent = Json.Serialize(payload);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var httpContent = new ByteArrayContent(buffer);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PutAsync("api/" + urlEnding, httpContent);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                throw new HttpRequestException($"Status code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
            }
        }
    }
}
