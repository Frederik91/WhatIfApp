using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WhatIf.Shared.Services.User;

namespace WhatIf.Database.User
{
    public class UserQueryHandler : IUserQueryHandler
    {
        private readonly IDbConnection _dbConnection;

        public UserQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UserResult> GetUser(Guid id)
        {
            var user = await _dbConnection.QueryFirstAsync<UserResult>(Sql.FindUser, new { Id = id });
            return user;
        }

        public async Task CreateUser(UserResult user)
        {
            await _dbConnection.ExecuteAsync(Sql.InsertUser, user);
        }

        public async Task<ICollection<UserResult>> GetUsersInSession(Guid sessionId)
        {
            var users = await _dbConnection.QueryAsync<UserResult>(Sql.FindUsersInSession, new { SessionId = sessionId });
            return users.ToList();
        }
    }
}
