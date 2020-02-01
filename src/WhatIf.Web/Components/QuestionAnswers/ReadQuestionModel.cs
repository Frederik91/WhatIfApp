using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WhatIf.Web.Components.QuestionAnswers
{
    public class ReadQuestionModel
    {
        public string Content { get; set; }
        public Guid AssignedAnswerId { get; set; }
        public Guid Id { get; set; }
        public bool IsRead { get; set; }
    }
}
