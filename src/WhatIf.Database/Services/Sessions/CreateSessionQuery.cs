using CQRS.Query.Abstractions;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Sessions
{
    public class CreateSessionQuery : IQuery<SessionTbl>
    {
        public string Name { get; set; }
    }
}