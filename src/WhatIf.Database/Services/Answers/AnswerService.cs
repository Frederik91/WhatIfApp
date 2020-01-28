using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;

namespace WhatIf.Database.Services.Answers
{
    public class AnswerService : IAnswerService
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
    }
}
