using System;

namespace WhatIf.Database.Services.Sessions
{
    public class StartSessionCommand
    {
        public Guid SessionId { get; set; }
        public int CardAmount { get; set; }
    }
}