using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WhatIf.Database
{
    public class WhatIfDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public WhatIfDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = _configuration.GetConnectionString("WhatIfDatabase");
            options.UseSqlite(connectionString);
            base.OnConfiguring(options);
        }
    }
}
