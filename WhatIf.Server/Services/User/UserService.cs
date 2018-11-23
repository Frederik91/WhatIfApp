using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatIf.Database;
using WhatIf.Database.User;
using WhatIf.Shared.Services.Session;
using WhatIf.Shared.Services.User;

namespace WhatIf.Server.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserQueryHandler _userQueryHandler;

        public UserService(IUserQueryHandler userQueryHandler)
        {
            _userQueryHandler = userQueryHandler;
        }
        public async Task<UserResult> GetUser(Guid id)
        {
            return await _userQueryHandler.GetUser(id);
        }

        public async Task<UserResult> CreateUser(CreateUserRequest request)
        {
            var user = new UserResult
            {
                Id = Guid.NewGuid(),
                SessionId = request.SessionId,
                Nickname = request.Nickname
            };
            await _userQueryHandler.CreateUser(user);
            return user;
        }

        public async Task<ICollection<UserResult>> GetUsersInSession(Guid sessionId)
        {
            var users = await _userQueryHandler.GetUsersInSession(sessionId);
            return users;
        }
    }
}
