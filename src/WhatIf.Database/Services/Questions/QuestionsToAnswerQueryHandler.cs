using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using Microsoft.EntityFrameworkCore;
using WhatIf.Core.Models;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Questions
{
    public class QuestionsToAnswerQueryHandler : IQueryHandler<QuestionsToAnswerQuery, List<QuestionTbl>>
    {
        private readonly WhatIfDbContext _dbContext;

        public QuestionsToAnswerQueryHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<QuestionTbl>> HandleAsync(QuestionsToAnswerQuery query, CancellationToken cancellationToken = new CancellationToken())
        {

            return await _dbContext.Questions.AsNoTracking().Where(x => x.AssignedToPlayerId == query.PlayerId).ToListAsync(cancellationToken);
        }
    }
}
    