using System;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using WhatIf.Core.Models;
using WhatIf.Core.Services;

namespace WhatIf.Database.Services.Sessions
{
    public class SessionService : ISessionService
    {
        private readonly IMapper _mapper;
        private readonly IQueryExecutor _queryExecutor;
        private readonly ICommandExecutor _commandExecutor;

        public SessionService(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            _mapper = mapper;
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
        }

        public async Task<SessionDto> Get(Guid sessionId)
        {
            var sessionTbl = await _queryExecutor.ExecuteAsync(new SessionByIdQuery { Id = sessionId });
            return sessionTbl is null ? null : _mapper.Map<SessionDto>(sessionTbl);
        }

        public async Task Start(Guid sessionId, int cardAmount)
        {
            await _commandExecutor.ExecuteAsync(new StartSessionCommand { SessionId = sessionId, CardAmount = cardAmount });
        }

        public async Task<SessionDto> Get(int number)
        {
            var sessionTbl = await _queryExecutor.ExecuteAsync(new SessionByNumberQuery { Number = number });
            return sessionTbl is null ? null : _mapper.Map<SessionDto>(sessionTbl);
        }

        public async Task<SessionDto> Create(string name)
        {
            var sessionTbl = await _queryExecutor.ExecuteAsync(new CreateSessionQuery { Name = name });
            return _mapper.Map<SessionDto>(sessionTbl);
        }
    }
}
