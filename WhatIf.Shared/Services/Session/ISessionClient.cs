using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.Session
{
    public interface ISessionClient
    {
        Task<SessionResult> Get(int joinId);
        Task<SessionResult> CreateNew(string sessionName);
        Task SetLeader(Guid sessionId, Guid userId);
        Task StartSession(Guid sessionResultId);
    }
}
