using System;

namespace WhatIf.Database.Tables
{
    public class SessionTbl
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public bool IsFinished { get; set; }
        public int CardAmount { get; set; }
        public bool CreateQuestionsRoundIsCompleted { get; set; }
        public bool CreateAnswersRoundIsCompleted { get; set; }
        public bool ReadingRoundHasStarted { get; set; }
    }
}
