using System;

namespace WhatIf.Database.Services.Sessions
{
    public class SetCardAmountCommand
    {
        public Guid SessionId { get; set; }
        public int CardAmount { get; set; }
    }
}