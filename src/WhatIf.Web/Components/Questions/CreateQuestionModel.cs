using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace WhatIf.Web.Components.Questions
{
    public class CreateQuestionModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
