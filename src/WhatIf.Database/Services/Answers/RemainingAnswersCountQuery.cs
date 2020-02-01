using CQRS.Query.Abstractions;
using System;

namespace WhatIf.Database.Services.Answers
{
    public class RemainingAnswersCountQuery : IQuery<int>
    {
        public Guid SessionId { get; set; }
    }
}