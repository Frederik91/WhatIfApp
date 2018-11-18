using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Shared.Services.Session
{
    public interface IJoinIdGenerator
    {
        int Generate();
    }
}
