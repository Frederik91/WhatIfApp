using CQRS.Query.Abstractions;
using System;
using System.Collections.Generic;
using WhatIf.Database.Concepts;

namespace WhatIf.Database.Services.Answers
{
    public class GetQuestionAnswersByPlayer : IQuery<List<QuestionAnswerCpt>>
    {
        public Guid PlayerId { get; set; }
    }
}