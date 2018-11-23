using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Database.Session
{
    public class SessionQueryHandler : ISessionQueryHandler
    {
        readonly IDbConnection _dbConnection;

        public SessionQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Create(SessionResult session)
        {
                await _dbConnection.ExecuteAsync(Sql.InsertSession, session);
        }

        public async Task<SessionResult> Get(int joinId)
        {
            var session = await _dbConnection.QueryFirstAsync<SessionResult>(Sql.FindSessionByJoinId, new { JoinId = joinId });
            return session;
        }

        public async Task SetLeader(SetLeaderRequest request)
        {
            await _dbConnection.ExecuteAsync(Sql.UpdateSessionLeader, request);
        }
    }
}
