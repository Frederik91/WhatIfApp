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


            var assignedAnswers = new HashSet<Guid>();
            var cards = new List<(QuestionTbl, AnswerTbl)>();
            foreach (var question in questions)
            {
                var answer = answers.FirstOrDefault(y => y.QuestionId != question.Id && !assignedAnswers.Contains(y.Id));
                if (answer is null)
                {
                    var assignedAnswer = answers.First(y => assignedAnswers.Contains(y.Id) && y.QuestionId != question.Id);
                    var card = cards.First(x => x.Item2.Id == assignedAnswer.Id);
                    answer = card.Item2;
                    card.Item2 = answers.First(y => y.QuestionId != question.Id && !assignedAnswers.Contains(y.Id));
                    assignedAnswers.Add(card.Item2.Id);
                }
                cards.Add((question, answer));
            }

            
            foreach (var card in cards)
            {
                
            }

            var cardsPerPlayerCount = cards.Count / playerIds.Count;
            var cardIndex = 0;
            for (var i = 0; i < playerIds.Count; i++)
            {
                var questionPlayerId = playerIds[i];
                var answerPlayerId = playerIds[i + 1 < playerIds.Count ? i + 1 : 0];
                for (var j = 0; j < cardsPerPlayerCount; j++)
                {
                    var card = cards[cardIndex];
                    card.Item1.AssignedAnswerId = card.Item2.Id;
                    card.Item1.PlayerToReadQuestionId = questionPlayerId;
                    card.Item2.PlayerToReadAnswerId = answerPlayerId;
                    cardIndex++;
                }
            }



            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
