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
    public class MarkQuestionAsCurrentCommandHandler : ICommandHandler<MarkQuestionAsCurrentCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public MarkQuestionAsCurrentCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(MarkQuestionAsCurrentCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var question = _dbContext.Questions.First(x => x.Id == command.QuestionId);
            question.IsCurrent = true;
            _dbContext.Entry(question).Property(x => x.IsRead).IsModified = true;
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
