using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using LightInject;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WhatIf.Database;

namespace WhatIf.Core.Tests
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<WhatIf.Core.CompositionRoot>();
            serviceRegistry.RegisterFrom<WhatIf.Database.CompositionRoot>();


            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<WhatIfDbContext>()
                .UseSqlite(connection)
                .Options;

            serviceRegistry.RegisterInstance<DbContextOptions>(options);
            serviceRegistry.Register<WhatIfDbContext>();

            if (serviceRegistry is IServiceFactory factory)
                factory.GetInstance<WhatIfDbContext>().Database.EnsureCreated();
        }
    }
}
