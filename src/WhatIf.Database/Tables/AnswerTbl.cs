﻿using System;

namespace WhatIf.Database.Tables
{
    public class AnswerTbl
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }
        public SessionTbl Session { get; set; }

        public Guid CreatedByPlayerId { get; set; }
        public PlayerTbl CreatedByPlayer { get; set; }

        public Guid QuestionId { get; set; }
        public QuestionTbl Question { get; set; }

        public string Content { get; set; }
    }
}
