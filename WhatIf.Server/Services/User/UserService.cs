using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatIf.Database;

namespace WhatIf.Server.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserQueryHandler _userQueryHandler;

        public UserService(IUserQueryHandler userQueryHandler)
        {
            _userQueryHandler = userQueryHandler;
        }
        public async Task<string> GetUser(int id)
        {
            return await _userQueryHandler.GetUser(id);
        }
    }
}
