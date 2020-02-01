using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
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
                (player, question) => new { PlayerId = player.Id, Question = question }).ToListAsync(cancellationToken);

            playerQuestions.Shuffle();
            var playerIds = playerQuestions.Select(x => x.PlayerId).Distinct().ToList();
            var questions = playerQuestions.Select(x => x.Question).ToList();

            for (var i = 0; i < playerIds.Count; i++)
            {
                var playerId = playerIds[i];
                var nextPlayerId = playerIds[i + 1 < playerIds.Count ? i + 1 : 0];
                var questionsToAssign = questions.Where(x => x.CreatedByPlayerId == nextPlayerId);
                foreach (var question in questionsToAssign)
                    question.AssignedToPlayerId = playerId;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
