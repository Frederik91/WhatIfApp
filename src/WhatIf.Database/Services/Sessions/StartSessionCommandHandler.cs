using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;

namespace WhatIf.Database.Services.Sessions
{
    public class StartSessionCommandHandler : ICommandHandler<StartSessionCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public StartSessionCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(StartSessionCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var session = _dbContext.Sessions.First(x => x.Id == command.SessionId);
            session.StartTime = DateTimeOffset.Now;
            session.CardAmount = command.CardAmount;

            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
