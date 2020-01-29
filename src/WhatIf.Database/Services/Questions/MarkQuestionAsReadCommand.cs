using CQRS.Query.Abstractions;
using System;

namespace WhatIf.Database.Services.Questions
{
    public class MarkQuestionAsReadCommand
    {
        public Guid QuestionId { get; set; }
    }
}