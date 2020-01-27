using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using Microsoft.EntityFrameworkCore;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Players
{
    public class PlayersInSessionQueryHandler : IQueryHandler<PlayersInSessionQuery, List<PlayerTbl>>
    {
        private readonly WhatIfDbContext _dbContext;

        public PlayersInSessionQueryHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<PlayerTbl>> HandleAsync(PlayersInSessionQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            return _dbContext.Players.Where(x => x.SessionId == query.SessionId).ToListAsync(cancellationToken);
        }
    }
}
