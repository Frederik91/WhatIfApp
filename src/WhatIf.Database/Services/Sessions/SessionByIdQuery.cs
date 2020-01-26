using CQRS.Query.Abstractions;
using System;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Sessions
{
    public class SessionByIdQuery : IQuery<SessionTbl>
    {
        public Guid Id { get; set; }
    }
}