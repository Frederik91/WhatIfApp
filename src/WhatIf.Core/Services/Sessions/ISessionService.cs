using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Core.Services.Sessions
{
    public interface ISessionService
    {
        Task Get(Guid sessionId);
        Task Create(Guid playerId);
        Task Join(Guid playerId, int joinId);
    }
}
