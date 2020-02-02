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
    public class SessionByIdQueryHandler : IQueryHandler<SessionByIdQuery, SessionTbl>
    {
        private readonly WhatIfDbContext _dbContext;

        public SessionByIdQueryHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<SessionTbl> HandleAsync(SessionByIdQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            return _dbContext.Sessions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
        }
    }
}