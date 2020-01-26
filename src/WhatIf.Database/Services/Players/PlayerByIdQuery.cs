using CQRS.Query.Abstractions;
using System;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Players
{
    public class PlayerByIdQuery : IQuery<PlayerTbl>
    {
        public Guid Id { get; set; }
    }
}