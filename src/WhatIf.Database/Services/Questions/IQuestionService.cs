using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhatIf.Core.Models;

namespace WhatIf.Database.Services.Questions
{
    public interface IQuestionService
    {
        Task SubmitQuestions(Guid playerId, List<string> questions);
        Task AssignQuestions(Guid sessionId);
        Task<IReadOnlyCollection<QuestionDto>> GetQuestionsToAnswer(Guid playerId);
        Task MarkQuestionAsRead(Guid questionId);
    }
}