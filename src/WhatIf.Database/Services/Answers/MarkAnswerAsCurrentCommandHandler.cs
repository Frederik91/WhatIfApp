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
    public class MarkAnswerAsCurrentCommandHandler : ICommandHandler<MarkAnswerAsCurrentCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public MarkAnswerAsCurrentCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(MarkAnswerAsCurrentCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var answer = _dbContext.Answers.First(x => x.Id == command.AnswerId);
            answer.IsCurrent = true;
            _dbContext.Entry(answer).Property(x => x.IsCurrent).IsModified = true;
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
