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
    public class MarkQuestionAsReadCommandHandler : ICommandHandler<MarkQuestionAsReadCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public MarkQuestionAsReadCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(MarkQuestionAsReadCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var question = _dbContext.Questions.First(x => x.Id == command.QuestionId);
            question.IsRead = true;
            _dbContext.Entry(question).Property(x => x.IsRead).IsModified = true;
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
