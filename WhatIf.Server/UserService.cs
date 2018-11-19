using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatIf.Database;

namespace WhatIf.Server
{
    public class UserService : IUserService
    {
        private readonly IUserQueryHandler userQueryHandler;

        public UserService(IUserQueryHandler userQueryHandler)
        {
            this.userQueryHandler = userQueryHandler;
        }
        public async Task<string> GetUser(int id)
        {
            return await userQueryHandler.GetUser(id);
        }
    }
}
