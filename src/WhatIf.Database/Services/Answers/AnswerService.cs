using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using WhatIf.Core.Models;

namespace WhatIf.Database.Services.Answers
{
    public abstract class AnswerService : IAnswerService
    {
        private readonly ICommandExecutor _commandExecutor;

        public AnswerService(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public Task SubmitAnswers(Guid playerId, List<SubmitAnswerRequest> requests)
        {
            return _commandExecutor.ExecuteAsync(new SubmitAnswersCommand { PlayerId = playerId, Requests = requests });
        }

        public Task AssignAnswersAndQuestions(Guid sessionId)
        {
            return _commandExecutor.ExecuteAsync(new AssignAnswersAndQuestionsCommand { SessionId = sessionId });.
        }

        public Task<List<QuestionAnswerDto>> GetQuestionAnswersFromPlayer(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public Task MarkAnswerAsRead(Guid answerId)
        {
            return _commandExecutor.ExecuteAsync(new MarkAnswerAsReadCommand { AnswerId = answerId });
        }
    }
}
