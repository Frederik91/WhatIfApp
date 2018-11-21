using System;
using System.Collections.Generic;
using System.Text;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Server.Services.Session
{
    public class JoinIdGenerator : IJoinIdGenerator
    {
        public int Generate()
        {
            return new Random(DateTime.Now.Millisecond).Next(0, 9999);
        }
    }
}
