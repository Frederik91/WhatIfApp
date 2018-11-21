using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatIf.Server.Services.User
{
    public interface IUserService
    {
        Task<string> GetUser(int id);
    }
}
