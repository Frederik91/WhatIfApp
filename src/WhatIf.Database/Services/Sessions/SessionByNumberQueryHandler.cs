using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using Microsoft.EntityFrameworkCore;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Sessions
{
    public class SessionByNumberQueryHandler : IQueryHandler<SessionByNumberQuery, SessionTbl>
    {
        private readonly WhatIfDbContext _dbContext;

        public SessionByNumberQueryHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<SessionTbl> HandleAsync(SessionByNumberQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            return _dbContext.Sessions.AsNoTracking().FirstOrDefaultAsync(x => !x.IsFinished && x.Number == query.Number, cancellationToken);
        }
    }
}