using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.Session
{
    public class SessionUsecase : ISessionUsecase
    {
        private readonly HttpClient _httpClient;

        public SessionUsecase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<SessionResult> Get(int sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<SessionResult> CreateNew(string sessionName)
        {
            throw new NotImplementedException();
        }
    }
}
