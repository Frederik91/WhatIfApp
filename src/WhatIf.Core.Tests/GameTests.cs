using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using LightInject;
using LightInject.xUnit2;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using WhatIf.Core.Models;
using WhatIf.Core.Services;
using WhatIf.Database;
using WhatIf.Database.Services.Answers;
using WhatIf.Database.Services.Questions;
using WhatIf.Database.Services.Sessions;
using Xunit;

namespace WhatIf.Core.Tests
{
    public class GameTests
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void Configure(IServiceContainer container)
        {
            container.RegisterFrom<CompositionRoot>();
        }

        [Theory, Scoped, InjectData]
        public async Task CreateSession(ISessionService sessionService)
        {
            var session = await sessionService.Create("Test");
            var fetchedSession = await sessionService.Get(session.Id);

            Assert.Equal(session.Name, fetchedSession.Name);
        }

        [Theory, Scoped, InjectData]
        public async Task PlayGame(ISessionService sessionService, IPlayerService playerService, IQuestionService questionService, IAnswerService answerService)
        {
            var session = await sessionService.Create("Test");

            var players = new List<PlayerDto>();
            session.CardAmount = 3;
            for (var i = 0; i < session.CardAmount; i++)
            {
                var player = await playerService.Create(Guid.NewGuid().ToString(), session.Id, i == 0);
                players.Add(player);
            }

            await sessionService.Start(session.Id, 3);
            foreach (var player in players)
            {
                var questions = new List<string>();
                for (var i = 0; i < session.CardAmount; i++)
                    questions.Add(Guid.NewGuid().ToString());

                await questionService.SubmitQuestions(player.Id, questions);
            }

            await questionService.AssignQuestions(session.Id);

            foreach (var player in players)
            {
                var answers = await questionService.GetQuestionsToAnswer(player.Id);
                Assert.Equal(3, answers.Count);

                await answerService.SubmitAnswers(player.Id, answers.Select(x => new SubmitAnswerRequest
                {
                    Answer = Guid.NewGuid().ToString(),
                    QuestionId = x.Id
                }).ToList());
            }

            await answerService.AssignAnswersAndQuestions(session.Id);
            foreach (var player in players)
            {
                var answerQuestions = await answerService.GetQuestionAnswersForPlayer(player.Id);
                Assert.True(answerQuestions.DistinctBy(x => x.Question.AssignedAnswerId).Count() == answerQuestions.Count);
                Assert.True(answerQuestions.DistinctBy(x => x.Answer.Id).Count() == answerQuestions.Count);
                Assert.True(answerQuestions.All(x => answerQuestions.All(y => y.Answer.Id != x.Question.AssignedAnswerId)));
            }
        }
    }
}
