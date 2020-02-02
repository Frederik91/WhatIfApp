using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;

namespace WhatIf.Database.Services.Sessions
{
    public class MarkSessionFinishedCommandCommand : ICommandHandler<MarkSessionFinishedCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public MarkSessionFinishedCommandCommand(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(MarkSessionFinishedCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var session = _dbContext.Sessions.First(x => x.Id == command.SessionId);
            session.IsFinished = true;

            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
