using System;

namespace WhatIf.Database.Tables
{
    public class SessionTbl
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public bool IsFinished { get; set; }
        public string Name { get; set; }
    }
}
