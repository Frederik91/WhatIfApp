using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Answers
{
    public class SubmitAnswersCommandHandler : ICommandHandler<SubmitAnswersCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public SubmitAnswersCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(SubmitAnswersCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var player = _dbContext.Players.First(x => x.Id == command.PlayerId);
            var answers = command.Requests.Select(x => new AnswerTbl
            {
                Id = Guid.NewGuid(),
                CreatedByPlayerId = command.PlayerId,
                QuestionId = x.QuestionId,
                Content = x.Answer,
                SessionId = player.SessionId
            });
            player.HasSubmittedAnswers = true;
            _dbContext.Answers.AddRange(answers);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
