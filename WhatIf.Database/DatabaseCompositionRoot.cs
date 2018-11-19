using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using WhatIf.Migrations.Migrations.Iteration1;

namespace WhatIf.Database
{
    public static class DatabaseCompositionRoot
    {
        private static readonly string dbConnection = "Data Source=D:\\mydb.db;";
        public static void Compose(IServiceCollection services)
        {
            services.AddSingleton<IDbConnection>(CreateSQLiteConnection());
            services.AddTransient<IUserQueryHandler, UserQueryHandler>();
            
            UpdateDatabase(services);

        }
        private static void UpdateDatabase(IServiceCollection services)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(dbConnection)
                    .ScanIn(typeof(Mig001_CreateUserTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false)
                .GetRequiredService<IMigrationRunner>()
                .MigrateUp();
            
        }
        private static SQLiteConnection CreateSQLiteConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(dbConnection);
            connection.Open();
            return connection;
        }
    }
}
