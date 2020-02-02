using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Database.Services.Sessions
{
    public class MarkReadingRoundStartedCommand
    {
        public Guid SessionId { get; set; }
    }
}
