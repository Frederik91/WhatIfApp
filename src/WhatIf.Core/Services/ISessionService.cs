using System;
using System.Threading.Tasks;
using WhatIf.Core.Models;

namespace WhatIf.Core.Services
{
    public interface ISessionService
    {
        Task<SessionDto> Get(int number);
        Task<SessionDto> Create(string name);
        Task SetGameMaster(Guid sessionId, Guid playerId);
        Task<SessionDto> Get(Guid sessionId);
    }
}
