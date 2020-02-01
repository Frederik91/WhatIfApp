using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhatIf.Core.Models;

namespace WhatIf.Database.Services.Answers
{
    public interface IAnswerService
    {
        Task SubmitAnswers(Guid playerId, List<SubmitAnswerRequest> requests);
        Task AssignAnswersAndQuestions(Guid sessionId);
        Task<List<QuestionAnswerDto>> GetQuestionAnswersForPlayer(Guid playerId);
        Task MarkAnswerAsRead(Guid answerId);
        Task<int> GetRemainingAnswerCount(Guid sessionId);
    }
}