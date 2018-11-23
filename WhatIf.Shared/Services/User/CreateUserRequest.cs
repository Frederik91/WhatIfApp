using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Shared.Services.User
{
    public class CreateUserRequest
    {
        public string Nickname { get; set; }
        public Guid SessionId { get; set; }
    }
}
