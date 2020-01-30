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
    public class GetQuestionAnswersByPlayerHandler : IQueryHandler<GetQuestionAnswersByPlayer, List<QuestionAnswerCpt>>
    {
        private readonly WhatIfDbContext _whatIfDbContext;

        public GetQuestionAnswersByPlayerHandler(WhatIfDbContext whatIfDbContext)
        {
            _whatIfDbContext = whatIfDbContext;
        }

        public async Task<List<QuestionAnswerCpt>> HandleAsync(GetQuestionAnswersByPlayer query, CancellationToken cancellationToken = new CancellationToken())
        {
            _whatIfDbContext.Questions.Where(x => x.)
        }
    }
}
