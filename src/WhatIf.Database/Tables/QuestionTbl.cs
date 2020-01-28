using System;

namespace WhatIf.Database.Tables
{
    public class QuestionTbl
    {
        public Guid Id { get; set; }

        public Guid CreatedByPlayerId { get; set; }
        public PlayerTbl CreatedByPlayer { get; set; }

        public string Content { get; set; }
        public Guid? AssignedToPlayerId { get; set; }
        public PlayerTbl? AssignedToPlayer { get; set; }
    }
}
