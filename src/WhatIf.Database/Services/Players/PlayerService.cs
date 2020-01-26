using System;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using Microsoft.EntityFrameworkCore.Update;
using WhatIf.Core.Models;
using WhatIf.Core.Services;

namespace WhatIf.Database.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly IQueryExecutor _queryExecutor;

        public PlayerService(IMapper mapper, IQueryExecutor queryExecutor)
        {
            _mapper = mapper;
            _queryExecutor = queryExecutor;
        }

        public async Task<PlayerDto> Create(string name, Guid sessionId)
        {
            var playerTbl = await _queryExecutor.ExecuteAsync(new CreatePlayerQuery { Name = name, SessionId = sessionId });
            return _mapper.Map<PlayerDto>(playerTbl);
        }

        public async Task<PlayerDto> Get(Guid playerId)
        {
            var playerTbl = await _queryExecutor.ExecuteAsync(new PlayerByIdQuery { Id = playerId });
            return playerTbl is null ? null : _mapper.Map<PlayerDto>(playerTbl);
        }
    }
}
