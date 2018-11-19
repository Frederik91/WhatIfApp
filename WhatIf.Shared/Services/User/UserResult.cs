using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Shared.Services.User
{
    public class UserResult
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public Guid SessionId { get; set; }
    }
}
