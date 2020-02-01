using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using Microsoft.EntityFrameworkCore;
using WhatIf.Database.Concepts;

namespace WhatIf.Database.Services.Answers
{
    public class RemainingAnswersCountQueryHandler : IQueryHandler<RemainingAnswersCountQuery, int>
    {
        private readonly WhatIfDbContext _whatIfDbContext;

        public RemainingAnswersCountQueryHandler(WhatIfDbContext whatIfDbContext)
        {
            _whatIfDbContext = whatIfDbContext;
        }

        public Task<int> HandleAsync(RemainingAnswersCountQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            return _whatIfDbContext.Answers.AsNoTracking().Where(x => x.SessionId == query.SessionId && !x.IsRead).CountAsync(cancellationToken);
        }
    }
}
