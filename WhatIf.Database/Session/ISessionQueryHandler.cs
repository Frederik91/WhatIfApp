using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Database.Session
{
    public interface ISessionQueryHandler
    {
        Task Create(SessionResult session);
        Task<SessionResult> Get(int joinId);
        Task SetLeader(SetLeaderRequest request);
    }
}
