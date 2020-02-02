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
    public class GetQuestionAnswersByPlayerQueryHandler : IQueryHandler<GetQuestionAnswersByPlayerQuery, List<QuestionAnswerCpt>>
    {
        private readonly WhatIfDbContext _whatIfDbContext;

        public GetQuestionAnswersByPlayerQueryHandler(WhatIfDbContext whatIfDbContext)
        {
            _whatIfDbContext = whatIfDbContext;
        }

        public async Task<List<QuestionAnswerCpt>> HandleAsync(GetQuestionAnswersByPlayerQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            var questions = await _whatIfDbContext.Questions.AsNoTracking().Where(x => x.PlayerToReadQuestionId == query.PlayerId).ToListAsync(cancellationToken);
            var answers = await _whatIfDbContext.Answers.AsNoTracking().Where(x => x.PlayerToReadAnswerId == query.PlayerId).ToListAsync(cancellationToken);

            var result = answers.Select((t, i) => new QuestionAnswerCpt { Question = questions.First(x => x.Id == t.QuestionId), Answer = t }).ToList();
            return result;
        }
    }
}
