using CQRS.Query.Abstractions;
using System;

namespace WhatIf.Database.Services.Questions
{
    public class MarkQuestionAsCurrentCommand
    {
        public Guid QuestionId { get; set; }
        public bool IsCurrent { get; set; }
    }
}