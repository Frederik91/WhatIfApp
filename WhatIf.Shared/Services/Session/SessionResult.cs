using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Shared.Services.Session
{
    public class SessionResult
    {
        public Guid Id { get; set; }
        public int JoinId { get; set; }
        public string Name { get; set; }
        public Guid LeaderId { get; set; }
        public bool Started { get; set; }
        public bool Ended { get; set; }
    }
}
