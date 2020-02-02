using System;
using System.Threading.Tasks;
using WhatIf.Core.Models;

namespace WhatIf.Core.Services
{
    public interface ISessionService
    {
        Task<SessionDto> Get(int number);
        Task<SessionDto> Create(string name);
        Task<SessionDto> Get(Guid sessionId);
        Task Start(Guid sessionId, int cardAmount);
        Task MarkCreateQuestionsRoundFinished(Guid sessionId);
        Task MarkCreateAnswersRoundFinished(Guid sessionId);
        Task MarkReadingRoundStarted(Guid sessionId);
        Task MarkSessionFinished (Guid sessionId);
    }
}
