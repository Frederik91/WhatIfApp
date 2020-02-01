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

        public DbSet<SessionTbl> Sessions { get; set; }
        public DbSet<PlayerTbl> Players { get; set; }
        public DbSet<QuestionTbl> Questions { get; set; }
        public DbSet<AnswerTbl> Answers { get; set; }

        public WhatIfDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
