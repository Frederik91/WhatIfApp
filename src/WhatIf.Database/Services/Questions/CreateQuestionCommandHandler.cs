using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Questions
{
    public class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public CreateQuestionCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(CreateQuestionCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var questions = command.Questions.Select(x => new QuestionTbl { Id = Guid.NewGuid(), CreatedByPlayerId = command.PlayerId, Content = x });
            _dbContext.Questions.AddRange(questions);
            _dbContext.Players.First(x => x.Id == command.PlayerId).HasSubmittedQuestions = true;
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
