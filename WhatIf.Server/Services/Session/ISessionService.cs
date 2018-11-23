using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhatIf.Shared.Services.Session;
using WhatIf.Shared.Services.User;

namespace WhatIf.Server.Services.Session
{
    public interface ISessionService
    {
        Task<SessionResult> Get(int joinId);
        Task<SessionResult> CreateNew(CreateSessionRequest request);
        Task SetLeader(SetLeaderRequest request);
    }
}
