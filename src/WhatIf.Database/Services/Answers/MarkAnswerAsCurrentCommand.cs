﻿using System;

namespace WhatIf.Database.Services.Answers
{
    public class MarkAnswerAsCurrentCommand
    {
        public Guid AnswerId { get; set; }
        public bool IsCurrent { get; set; }
    }
}