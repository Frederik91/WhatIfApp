using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Core.Models
{
    public class QuestionAnswerDto
    {
        public QuestionDto Question { get; set; }
        public AnswerDto Answer { get; set; }
    }
}
