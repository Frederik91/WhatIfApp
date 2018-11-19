using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Database
{
    public interface IUserQueryHandler
    {
        Task<string> GetUser(int id);
    }
}
