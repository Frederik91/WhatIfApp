using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatIf.Database.Session;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Server.Services.Session
{
    public class SessionService : ISessionService
    {
        private readonly ISessionQueryHandler _sessionQueryHandler;
        private readonly IJoinIdGenerator _joinIdGenerator;

        public SessionService(ISessionQueryHandler sessionQueryHandler, IJoinIdGenerator joinIdGenerator)
        {
            _sessionQueryHandler = sessionQueryHandler;
            _joinIdGenerator = joinIdGenerator;
        }

        public async Task<SessionResult> Get(int joinId)
        {
            var session = await _sessionQueryHandler.Get(joinId);
            return session;
        }

        public async Task<SessionResult> CreateNew(CreateSessionRequest request)
        {
            var session = new SessionResult
            {
                Ended = false,
                Started = false,
                Name = request.Name,
                Id = Guid.NewGuid(),
                JoinId = _joinIdGenerator.Generate()
            };
            await _sessionQueryHandler.Create(session);
            return session;
        }

        public async Task SetLeader(SetLeaderRequest setLeaderRequest)
        {
            await _sessionQueryHandler.SetLeader(setLeaderRequest);
        }
    }
}
