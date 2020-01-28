using System;

namespace WhatIf.Database.Tables
{
    public class PlayerTbl
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SessionId { get; set; }
        public bool IsGameMaster { get; set; }
        public SessionTbl Session { get; set; }
        public bool HasSubmittedQuestions { get; set; }
        public bool HasSubmittedAnswers { get; set; }
    }
}
