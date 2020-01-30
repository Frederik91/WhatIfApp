using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using WhatIf.Core.Models;

namespace WhatIf.Database.Services.Answers
{
    public class AnswerService : IAnswerService
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public AnswerService(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public Task SubmitAnswers(Guid playerId, List<SubmitAnswerRequest> requests)
        {
            return _commandExecutor.ExecuteAsync(new SubmitAnswersCommand { PlayerId = playerId, Requests = requests });
        }

        public Task AssignAnswersAndQuestions(Guid sessionId)
        {
            return _commandExecutor.ExecuteAsync(new AssignAnswersAndQuestionsCommand { SessionId = sessionId });
        }

        public async Task<List<QuestionAnswerDto>> GetQuestionAnswersFromPlayer(Guid playerId)
        {
            var questionAnswerCpt = await _queryExecutor.ExecuteAsync(new GetQuestionAnswersByPlayer { PlayerId = playerId });
            return _mapper.Map<List<QuestionAnswerDto>>(questionAnswerCpt);
        }

        public Task MarkAnswerAsRead(Guid answerId)
        {
            return _commandExecutor.ExecuteAsync(new MarkAnswerAsReadCommand { AnswerId = answerId });
        }
    }
}
