﻿using System;

namespace WhatIf.Database.Tables
{
    public class QuestionTbl
    {
        public Guid Id { get; set; }

        public Guid CreatedByPlayerId { get; set; }
        public PlayerTbl CreatedByPlayer { get; set; }

        public string Content { get; set; }
        public Guid? AssignedToPlayerId { get; set; }
        public PlayerTbl AssignedToPlayer { get; set; }

        public Guid AssignedAnswerId { get; set; }

        public bool IsRead { get; set; }
        public Guid SessionId { get; set; }
        public SessionTbl Session { get; set; }
        public Guid PlayerToReadQuestionId { get; set; }
        public bool IsCurrent { get; set; }
    }
}
