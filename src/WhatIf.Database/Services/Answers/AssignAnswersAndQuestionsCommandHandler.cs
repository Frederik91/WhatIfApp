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
            var answers = await _dbContext.Answers.Where(x => x.SessionId == command.SessionId).ToListAsync(cancellationToken);
            var playerIds = await _dbContext.Players.Where(x => x.SessionId == command.SessionId).Select(x => x.Id).ToListAsync(cancellationToken);

            var cardPerPlayerCount = questions.Count / playerIds.Count;
            var cards = new Dictionary<Guid, List<(QuestionTbl, AnswerTbl)>>();
            foreach (var question in questions)
            {
                var answer = answers.FirstOrDefault(y => y.QuestionId == question.Id);
                var playerId = playerIds[cards.Count % cardPerPlayerCount];
                var card = (question, answer);
                if (cards.TryGetValue(playerId, out var playerCards))
                    playerCards.Add(card);
                else
                    cards.Add(playerId, new List<(QuestionTbl, AnswerTbl)> { card });
            }

            foreach (var playerCardGroup in cards)
            {
                var index = playerIds.IndexOf(playerCardGroup.Key);
                var nextPlayerId = playerIds[index < playerIds.Count ? index : 0];
                var nextPlayerCards = cards[nextPlayerId];
                foreach (var card in playerCardGroup.Value)
                {
                    card.Item1.PlayerToReadQuestionId = playerCardGroup.Key;
                    card.Item2.PlayerToReadAnswerId = playerCardGroup.Key;
                    var cardIndex = playerCardGroup.Value.IndexOf(card);
                    var nextPlayerCard = nextPlayerCards[cardIndex];
                    card.Item1.AssignedAnswerId = nextPlayerCard.Item2.Id;
                }
            }



            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
