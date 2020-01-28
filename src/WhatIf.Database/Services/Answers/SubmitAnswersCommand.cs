using System;
using System.Collections.Generic;

namespace WhatIf.Database.Services.Answers
{
    public class SubmitAnswersCommand
    {
        public Guid PlayerId { get; set; }
        public List<SubmitAnswerRequest> Requests { get; set; }
    }
}