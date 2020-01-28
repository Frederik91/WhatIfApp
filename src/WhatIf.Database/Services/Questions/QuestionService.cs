using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using WhatIf.Core.Models;

namespace WhatIf.Database.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public QuestionService(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public Task SubmitQuestions(Guid playerId, List<string> questions)
        {
            return _commandExecutor.ExecuteAsync(new CreateQuestionCommand { PlayerId = playerId, Questions = questions });
        }

        public Task AssignQuestions(Guid sessionId)
        {
            return _commandExecutor.ExecuteAsync(new AssignQuestionsCommand { SessionId = sessionId });
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsToAnswer(Guid playerId)
        {
            var questions = await _queryExecutor.ExecuteAsync(new QuestionsToAnswerQuery { PlayerId = playerId });
            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }
    }
}
