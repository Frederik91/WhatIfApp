using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;

namespace WhatIf.Database.Services.Sessions
{
    public class MarkReadingRoundStartedCommandHandler : ICommandHandler<MarkReadingRoundStartedCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public MarkReadingRoundStartedCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(MarkReadingRoundStartedCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var session = _dbContext.Sessions.First(x => x.Id == command.SessionId);
            session.ReadingRoundHasStarted = true;

            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
