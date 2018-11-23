
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.User
{
    public class UserClient : IUserClient
    {
        private readonly IRestClientWrapper _restClientWrapper;


        public UserClient(IRestClientWrapper restClientWrapper)
        {
            _restClientWrapper = restClientWrapper;
        }

        public async Task<ICollection<UserResult>> GetUsers(Guid sessionId)
        {
            var result = await _restClientWrapper.GetAsync<ICollection<UserResult>>(sessionId, "User/Session/");
            return result;
        }

        public async Task<UserResult> GetUser(Guid userId)
        {
            var result = await _restClientWrapper.GetAsync<UserResult>(userId, "User/");
            return result;
        }

        public async Task<UserResult> CreateUser(string nickname, Guid sessionId)
        {
            var result = await _restClientWrapper.Post<UserResult>("User/",
                new CreateUserRequest { Nickname = nickname, SessionId = sessionId });
            return result;
        }
    }
}
