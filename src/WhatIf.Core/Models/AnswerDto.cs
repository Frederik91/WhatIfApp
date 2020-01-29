using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Core.Models
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}
