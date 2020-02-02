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
            var cardsByPlayer = new Dictionary<Guid, List<(QuestionTbl, AnswerTbl)>>();
            foreach (var question in questions)
            {
                var answer = answers.First(y => y.QuestionId == question.Id);
                var playerIndex = questions.IndexOf(question) / cardPerPlayerCount;
                var playerId = playerIds[playerIndex];
                question.PlayerToReadQuestionId = playerId;
                answer.PlayerToReadAnswerId = playerId;
                var card = (question, answer);
                if (cardsByPlayer.TryGetValue(playerId, out var playerCards))
                    playerCards.Add(card);
                else
                    cardsByPlayer.Add(playerId, new List<(QuestionTbl, AnswerTbl)> { card });
            }

            var firstCard = cardsByPlayer.First().Value.First();
            for (int i = 0; i < cardPerPlayerCount; i++)
            {
                foreach (var playerCards in cardsByPlayer)
                {
                    var card = playerCards.Value.First();
                    playerCards.Value.Remove(card);

                    var playerIndex = playerIds.IndexOf(playerCards.Key);
                    var nextPlayerId = playerIds[playerIndex + 1 < playerIds.Count ? playerIndex + 1 : 0];
                    var nextCard = cardsByPlayer[nextPlayerId].FirstOrDefault();
                    if (nextCard.Item1 is null)
                        nextCard = firstCard;

                    card.Item1.AssignedAnswerId = nextCard.Item2.Id;
                }
            }



            //var cards = cardsByPlayer.SelectMany(x => x.Value).ToList();
            //var unusedCards = cardsByPlayer.SelectMany(x => x.Value).ToList();
            //var firstCard = cards.First();
            //var stayInLoop = true;
            //while (stayInLoop)
            //{
            //    var nextCard = unusedCards.FirstOrDefault(x => x.Item1.PlayerToReadQuestionId != firstCard.Item1.PlayerToReadQuestionId);
            //    if (nextCard.Item1 is null)
            //    {
            //        nextCard = cards.First();
            //        stayInLoop = false;
            //    }


            //    firstCard.Item1.AssignedAnswerId = nextCard.Item2.Id;
            //    unusedCards.Remove(firstCard);
            //    firstCard = nextCard;
            //}
            //foreach (var card in cards)
            //{
            //    var nextCard = unusedCards.FirstOrDefault(x => x.Item1.PlayerToReadQuestionId != card.Item1.PlayerToReadQuestionId);
            //    if (nextCard.Item1 is null)
            //        nextCard = cards.First();

            //    card.Item1.AssignedAnswerId = nextCard.Item2.Id;
            //    unusedCards.Remove(card);
            //}

            //var initialCard = cards.First();
            //var assignedAnswers = 0;
            //while (assignedAnswers != cards.Count)
            //{
            //    var nextCard = cards.First(x => x.Item1.Id != initialCard.Item1.Id && x.Item1.PlayerToReadQuestionId != initialCard.Item1.PlayerToReadQuestionId && x.Item1.AssignedAnswerId != initialCard.Item2.Id);
            //    initialCard.Item1.AssignedAnswerId = nextCard.Item2.Id;
            //    assignedAnswers++;
            //    initialCard = nextCard;
            //}

            //for (var i = 0; i < cardPerPlayerCount; i++)
            //{
            //    foreach (var playerId in playerIds)
            //    {
            //                        var nextCard = unassignedCards.First(x => x.Item1.Id != initialCard.Item1.Id && x.Item1.PlayerToReadQuestionId != initialCard.Item1.PlayerToReadQuestionId);
            //    initialCard.Item1.AssignedAnswerId = nextCard.Item2.Id;
            //    unassignedCards.Remove(initialCard);
            //        //var index = playerIds.IndexOf(playerId);
            //        //var nextPlayerId = playerIds[index + 1 < playerIds.Count ? index + 1 : 0];
            //        //var card = cardsByPlayer[playerId][i];
            //        //var nextCard = cardsByPlayer[nextPlayerId].First(x => x.Item1.AssignedAnswerId != card.Item2.Id);
            //        //card.Item1.AssignedAnswerId = nextCard.Item2.Id;
            //    }
            //}



            //foreach (var playerCardGroup in cards)
            //{
            //    var index = playerIds.IndexOf(playerCardGroup.Key);
            //    var nextPlayerId = playerIds[index + 1 < playerIds.Count ? index + 1 : 0];
            //    var nextPlayerCards = cards[nextPlayerId];
            //    foreach (var card in playerCardGroup.Value)
            //    {
            //        card.Item1.PlayerToReadQuestionId = playerCardGroup.Key;
            //        card.Item2.PlayerToReadAnswerId = playerCardGroup.Key;
            //        var cardIndex = playerCardGroup.Value.IndexOf(card);
            //        var nextPlayerCard = nextPlayerCards[cardIndex];
            //        nextPlayerCard.Item1.AssignedAnswerId = card.Item2.Id;
            //    }
            //}


            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
