﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using WhatIf.Core.Helpers;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Sessions
{
    public class CreateSessionQueryHandler : IQueryHandler<CreateSessionQuery, SessionTbl>
    {
        private readonly WhatIfDbContext _dbContext;
        private readonly ISessionIdGenerator _sessionIdGenerator;

        public CreateSessionQueryHandler(WhatIfDbContext dbContext, ISessionIdGenerator sessionIdGenerator)
        {
            _dbContext = dbContext;
            _sessionIdGenerator = sessionIdGenerator;
        }

        public async Task<SessionTbl> HandleAsync(CreateSessionQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            var session = _dbContext.Add(new SessionTbl { Number = _sessionIdGenerator.Generate(), CardAmount = 3 });
            await _dbContext.SaveChangesAsync(cancellationToken);
            return session.Entity;
        }
    }
}
