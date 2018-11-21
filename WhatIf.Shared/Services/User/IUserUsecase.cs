using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.User
{
    public interface IUserUsecase
    {
        Task<UserResult> GetUser(Guid sessionId);
        Task<object> CreateUser(string nickname);
    }
}
