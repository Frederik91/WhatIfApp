using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatIf.Web.Components.QuestionAnswers
{
    public class ReadAnswerModel
    {
        public string Content { get; set; }
        public Guid Id { get; set; }
        public bool IsRead { get; set; }
        public bool IsCurrent { get; set; }
    }
}
