using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Core.Helpers
{
    public class SessionIdGenerator : ISessionIdGenerator
    {
        private static readonly Random _rnd = new Random(DateTime.Now.Millisecond);

        public int Generate()
        {
            return _rnd.Next(1000, 9999);
        }
    }
}
