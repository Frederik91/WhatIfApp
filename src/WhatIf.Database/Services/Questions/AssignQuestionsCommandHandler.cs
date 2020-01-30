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

namespace WhatIf.Database.Services.Questions
{
    public class AssignQuestionsCommandHandler : ICommandHandler<AssignQuestionsCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public AssignQuestionsCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task HandleAsync(AssignQuestionsCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var playerQuestions = await _dbContext.Players.Where(x => x.SessionId == command.SessionId).Join(_dbContext.Questions,
                player => player.Id, question => question.CreatedByPlayerId,
                (player, question) => new {PlayerId = player.Id, Question = question}).ToListAsync(cancellationToken);

            playerQuestions.Shuffle();

            foreach (var playerQuestion in playerQuestions)
            {
                var otherQuestion = playerQuestions.First(x =>
                    x.PlayerId != playerQuestion.PlayerId && !x.Question.AssignedToPlayerId.HasValue);
                otherQuestion.Question.AssignedToPlayerId = playerQuestion.PlayerId;
                //try
                //{
                //    _dbContext.Entry(otherQuestion.Question).Property(x => x.AssignedToPlayerId).IsModified = true;
                //}
                //catch (Exception e)
                //{
                    
                //}

                //_dbContext.Update(otherQuestion.Question);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
