using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.Session
{
    public class SessionClient : ISessionClient
    {
        private readonly IRestClientWrapper _restClientWrapper;


        public SessionClient(IRestClientWrapper restClientWrapper)
        {
            _restClientWrapper = restClientWrapper;
        }

        public async Task<SessionResult> Get(int joinId)
        {
            var user = await _restClientWrapper.GetAsync<SessionResult>(joinId.ToString(), "Session/");
            return user;
        }

        public async Task<SessionResult> CreateNew(string sessionName)
        {
            var request = new CreateSessionRequest {Name = sessionName};
            var session = await _restClientWrapper.Post<SessionResult>("Session/", request);
            return session;
        }

        public async Task SetLeader(Guid sessionId, Guid userId)
        {
            var request = new SetLeaderRequest {SessionId = sessionId, UserId = userId};
            await _restClientWrapper.Put("Session/Leader", request);
        }

        public Task StartSession(Guid sessionResultId)
        {
            throw new NotImplementedException();
        }
    }
}
