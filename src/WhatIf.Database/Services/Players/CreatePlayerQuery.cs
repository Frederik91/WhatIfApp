using System;
using CQRS.Query.Abstractions;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Players
{
    public class CreatePlayerQuery : IQuery<PlayerTbl>
    {
        public string Name { get; set; }
        public Guid SessionId { get; set; }
        public bool IsGameMaster { get; set; }
    }
}