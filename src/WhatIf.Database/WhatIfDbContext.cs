using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WhatIf.Database.Tables;

namespace WhatIf.Database
{
    public class WhatIfDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<SessionTbl> Sessions { get; set; }
        public DbSet<PlayerTbl> Players { get; set; }
        public DbSet<QuestionTbl> Questions { get; set; }
        public DbSet<AnswerTbl> Answers { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
