using System;

namespace WhatIf.Database.Services.Sessions
{
    public class SetGameMasterCommand
    {
        public Guid PlayerId { get; set; }
        public Guid SessionId { get; set; }
    }
}