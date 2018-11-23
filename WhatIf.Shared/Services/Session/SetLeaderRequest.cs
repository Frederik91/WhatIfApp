using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Shared.Services.Session
{
    public class SetLeaderRequest
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
    }
}
