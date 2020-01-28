using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using Microsoft.EntityFrameworkCore;
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

            Shuffle(playerQuestions);

            foreach (var playerQuestion in playerQuestions)
            {
                var otherQuestion = playerQuestions.First(x =>
                    x.PlayerId != playerQuestion.PlayerId && !x.Question.AssignedToPlayerId.HasValue);
                otherQuestion.Question.AssignedToPlayerId = playerQuestion.PlayerId;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private static readonly Random Rng = new Random();

        public static void Shuffle<T>(IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
