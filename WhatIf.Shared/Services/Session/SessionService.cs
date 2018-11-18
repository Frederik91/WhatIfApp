using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Shared.Services.Session
{
    public class SessionService : ISessionService
    {
        public Task<SessionResult> Get(int joinId)
        {
            return Task.FromResult(new SessionResult
            {
                Finished = false,
                Started = false,
                Id = Guid.NewGuid(),
                JoinId = joinId
            });
        }
    }
}
