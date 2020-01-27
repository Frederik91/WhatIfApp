using CQRS.Query.Abstractions;
using System;
using System.Collections.Generic;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Players
{
    public class PlayersInSessionQuery : IQuery<List<PlayerTbl>>
    {
        public Guid SessionId { get; set; }
    }
}