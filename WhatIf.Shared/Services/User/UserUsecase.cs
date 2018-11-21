
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.User
{
    public class UserUsecase : IUserUsecase
    {
        private readonly IRestClientWrapper _restClientWrapper;


        public UserUsecase(IRestClientWrapper restClientWrapper)
        {
            _restClientWrapper = restClientWrapper;
        }

        public async Task<UserResult> GetUser(Guid sessionId)
        {
            var result = await _restClientWrapper.GetAsync<UserResult>(sessionId, "User/Session/");
            return result;
        }

        public Task<object> CreateUser(string nickname)
        {
            throw new NotImplementedException();
        }
    }
}
