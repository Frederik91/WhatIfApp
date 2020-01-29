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
        Task<List<QuestionAnswerDto>> GetQuestionAnswersFromPlayer(Guid playerId);
        Task MarkAnswerAsRead(Guid answerId);
    }
}