using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;

namespace WhatIf.Database.Services.Sessions
{
    public class SetCardAmountCommandHandler : ICommandHandler<SetCardAmountCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public SetCardAmountCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(SetCardAmountCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var session = _dbContext.Sessions.First(x => x.Id == command.SessionId);
            session.CardAmount = command.CardAmount;

            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
