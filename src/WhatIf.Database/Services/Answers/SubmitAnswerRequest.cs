using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Database.Services.Answers
{
    public class SubmitAnswerRequest
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
