using System;
using System.Threading.Tasks;
using WhatIf.Database;

namespace WhatIf.Core.Services.Sessions
{
    public class SessionService : ISessionService
    {
        private readonly WhatIfDbContext _whatIfDbContext;

        public SessionService(WhatIfDbContext whatIfDbContext)
        {
            _whatIfDbContext = whatIfDbContext;
        }

        public Task Create(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public Task Join(Guid playerId, int sessionId)
        {
            throw new NotImplementedException();
        }

        public Task Get(Guid sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
