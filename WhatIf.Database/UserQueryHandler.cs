using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace WhatIf.Database
{
    public class UserQueryHandler : IUserQueryHandler
    {
        private readonly IDbConnection dbConnection;

        public UserQueryHandler(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public async Task<string> GetUser(int id)
        {
            var user = await dbConnection.QueryFirstAsync<string>("select RoleId from User where UserId = 1337");
            return user;
        }
    }
}
