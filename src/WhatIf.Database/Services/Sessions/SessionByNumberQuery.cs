using CQRS.Query.Abstractions;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Sessions
{
    public class SessionByNumberQuery : IQuery<SessionTbl>
    {
        public int Number { get; set; }
    }
}