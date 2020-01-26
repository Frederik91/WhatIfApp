using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Sessions
{
    public class SetGameMasterCommandHandler : ICommandHandler<SetGameMasterCommand>
    {
        private readonly WhatIfDbContext _dbContext;

        public SetGameMasterCommandHandler(WhatIfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(SetGameMasterCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var gameMaster = new GameMasterTbl { Id = Guid.NewGuid(), SessionId = command.SessionId, PlayerId = command.PlayerId };
            _dbContext.GameMasters.Add(gameMaster);

            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
