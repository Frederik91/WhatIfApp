﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Core.Models
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SessionId { get; set; }
    }
}