using System;

namespace WhatIf.Database.Tables
{
    public class QuestionTbl
    {
        public Guid Id { get; set; }

        public Guid SessionsId { get; set; }
        public SessionTbl Session { get; set; }

        public Guid CreatedByPlayerId { get; set; }
        public PlayerTbl CreatedByPlayer { get; set; }

        public string Content { get; set; }
    }
}
