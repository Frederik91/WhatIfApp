using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Server.Services.Session
{
    public class SessionService : ISessionService
    {
        private readonly IJoinIdGenerator _joinIdGenerator;

        public SessionService(IJoinIdGenerator joinIdGenerator)
        {
            _joinIdGenerator = joinIdGenerator;
        }

        public async Task<SessionResult> Get(int joinId)
        {
            var session = await CreateNew();
            session.JoinId = joinId;
            return session;
        }

        public Task<SessionResult> CreateNew()
        {
            return Task.FromResult(new SessionResult
            {
                Finished = false,
                Started = false,
                Name = "New Session",
                Id = Guid.NewGuid(),
                JoinId = _joinIdGenerator.Generate()
            });
        }
    }
}
