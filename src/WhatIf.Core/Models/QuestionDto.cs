using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Core.Models
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid AssignedAnswerId { get; set; }
    }
}
