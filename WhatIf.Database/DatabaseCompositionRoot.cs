using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;
using WhatIf.Database.Session;
using WhatIf.Database.User;
using WhatIf.Migrations.Migrations.Iteration1;

namespace WhatIf.Database
{
    public static class DatabaseCompositionRoot
    {
        private static readonly string _dbConnection = "Data Source=" + GetDbFileLocation();

        public static void Compose(IServiceCollection services)
        {
            services.AddSingleton<IDbConnection>(CreateSQLiteConnection());
            services.AddTransient<IUserQueryHandler, UserQueryHandler>();
            services.AddTransient<ISessionQueryHandler, SessionQueryHandler>();
            
            UpdateDatabase(services);

        }
        private static void UpdateDatabase(IServiceCollection services)
        {
            if (!File.Exists(GetDbFileLocation()))
                CreateDbFile();

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(_dbConnection)
                    .ScanIn(typeof(Mig001_CreateUserTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false)
                .GetRequiredService<IMigrationRunner>()
                .MigrateUp();
            
        }

        private static string GetDbFileLocation()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(directory, "mydb.db");
            return path;
        }

        private static void CreateDbFile()
        {
            File.WriteAllText(GetDbFileLocation(), "");
        }

        private static SQLiteConnection CreateSQLiteConnection()
        {
            var connection = new SQLiteConnection(_dbConnection);
            connection.Open();
            return connection;
        }
    }
}
