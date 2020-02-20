using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Core.Models
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public bool IsFinished { get; set; }
        public string Name { get; set; }
        public int CardAmount { get; set; }
        public bool CreateQuestionsRoundIsCompleted { get; set; }
        public bool CreateAnswersRoundIsCompleted { get; set; }
        public bool ReadingRoundHasStarted { get; set; }
    }
}
