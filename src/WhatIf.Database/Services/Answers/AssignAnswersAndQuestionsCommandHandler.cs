using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using Microsoft.EntityFrameworkCore;
using WhatIf.Database.Extensions;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Answers
{
    public class AssignAnswersAndQuestionsCommandHandler : ICommandHandler<AssignAnswersAndQuestionsCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public AssignAnswersAndQuestionsCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleAsync(AssignAnswersAndQuestionsCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var questions = await _dbContext.Questions.Where(x => x.SessionId == command.SessionId).ToListAsync(cancellationToken);
            questions.Shuffle();
            var answers = await _dbContext.Answers.Where(x => x.SessionId == command.SessionId).ToListAsync(cancellationToken);
            answers.Shuffle();
            var playerIds = await _dbContext.Players.Where(x => x.SessionId == command.SessionId).Select(x => x.Id).ToListAsync(cancellationToken);


            for (var i = 0; i < playerIds.Count; i++)
            {
                var questionPlayerId = playerIds[i];
                var answerPlayerId = playerIds[i + 1 < playerIds.Count ? i + 1 : 0];
                var questionsToAnswer = questions.Where(x => x.CreatedByPlayerId == answerPlayerId);
                foreach (var question in questionsToAnswer)
                {
                    question.PlayerToReadQuestionId = questionPlayerId;
                    var answer = answers.First();
                    answer.PlayerToReadAnswerId = answerPlayerId;
                    question.AssignedAnswerId = answer.Id;
                    answers.Remove(answer);
                }
            }



            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
