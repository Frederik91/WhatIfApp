using CQRS.Query.Abstractions;
using System;
using System.Collections.Generic;
using WhatIf.Core.Models;
using WhatIf.Database.Tables;

namespace WhatIf.Database.Services.Questions
{
    public class QuestionsToAnswerQuery : IQuery<List<QuestionTbl>>
    {
        public Guid PlayerId { get; set; }
    }
}