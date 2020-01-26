using CQRS.Query.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WhatIf.Core.Models;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Players
{
    public class PlayerByIdQueryHandler : IQueryHandler<PlayerByIdQuery, PlayerTbl>
    {
        private readonly WhatIfDbContext _dbContext;

        public PlayerByIdQueryHandler( WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<PlayerTbl> HandleAsync(PlayerByIdQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            return _dbContext.Players.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
        }
    }
}