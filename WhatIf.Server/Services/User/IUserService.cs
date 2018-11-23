using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatIf.Shared.Services.User;

namespace WhatIf.Server.Services.User
{
    public interface IUserService
    {
        Task<UserResult> GetUser(Guid id);
        Task<UserResult> CreateUser(CreateUserRequest request);
        Task<ICollection<UserResult>> GetUsersInSession(Guid sessionId);
    }
}
