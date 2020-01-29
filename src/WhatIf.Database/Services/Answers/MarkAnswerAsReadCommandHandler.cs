using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Answers
{
    public class MarkAnswerAsReadCommandHandler : ICommandHandler<MarkAnswerAsReadCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public MarkAnswerAsReadCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(MarkAnswerAsReadCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var question = _dbContext.Answers.First(x => x.Id == command.AnswerId);
            question.IsRead = true;
            _dbContext.Entry(question).Property(x => x.IsRead).IsModified = true;
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
