using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.Session
{
    public interface ISessionUsecase
    {
        Task<SessionResult> Get(int sessionId);
        Task<SessionResult> CreateNew(string sessionName);
    }
}
