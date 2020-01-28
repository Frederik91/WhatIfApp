using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhatIf.Database.Services.Answers
{
    public interface IAnswerService
    {
        Task SubmitAnswers(Guid playerId, List<SubmitAnswerRequest> requests);
    }
}