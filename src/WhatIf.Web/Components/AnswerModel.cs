using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatIf.Core.Models;

namespace WhatIf.Web.Components
{
    public class AnswerModel
    {
        public QuestionDto Question { get; set; }
        public string Content { get; set; }
    }
}
