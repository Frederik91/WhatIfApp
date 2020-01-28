using System;
using System.Collections.Generic;

namespace WhatIf.Database.Services.Questions
{
    public class CreateQuestionCommand
    {
        public Guid PlayerId { get; set; }
        public List<string> Questions { get; set; }
    }
}