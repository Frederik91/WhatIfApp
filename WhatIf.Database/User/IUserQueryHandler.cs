using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhatIf.Shared.Services.User;

namespace WhatIf.Database.User
{
    public interface IUserQueryHandler
    {
        Task<UserResult> GetUser(Guid id);
        Task CreateUser(UserResult user);
        Task<ICollection<UserResult>> GetUsersInSession(Guid sessionId);
    }
}
