﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatIf.Server
{
    public interface IUserService
    {
        Task<string> GetUser(int id);
    }
}