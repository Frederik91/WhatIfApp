using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.User
{
    public interface IUserClient
    {
        Task<ICollection<UserResult>> GetUsers(Guid sessionId);
        Task<UserResult> GetUser(Guid userId);
        Task<UserResult> CreateUser(string nickname, Guid sessionId);
    }
}
