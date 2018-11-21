using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace WhatIf.Database
{
    public class UserQueryHandler : IUserQueryHandler
    {
        private readonly IDbConnection _dbConnection;

        public UserQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<string> GetUser(int id)
        {
            var user = await _dbConnection.QueryFirstAsync<string>("select RoleId from User where UserId = 1337");
            return user;
        }
    }
}
