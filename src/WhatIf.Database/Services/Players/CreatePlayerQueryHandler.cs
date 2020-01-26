using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using WhatIf.Core.Models;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Players
{
    public class CreatePlayerQueryHandler : IQueryHandler<CreatePlayerQuery, PlayerTbl>
    {
        private readonly WhatIfDbContext _dbContext;

        public CreatePlayerQueryHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PlayerTbl> HandleAsync(CreatePlayerQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            var player = _dbContext.Players.Add(new PlayerTbl { Name = query.Name, SessionId = query.SessionId });
            await _dbContext.SaveChangesAsync(cancellationToken);
            return player.Entity;
        }
    }
}
