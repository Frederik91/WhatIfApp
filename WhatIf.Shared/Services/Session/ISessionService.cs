using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.Session
{
    public interface ISessionService
    {
        Task<SessionResult> Get(int joinId);
        Task<SessionResult> CreateNew();
    }
}
