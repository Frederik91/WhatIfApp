using WhatIf.Core.Models;

namespace WhatIf.Web.Components.Answers
{
    public class CreateAnswerModel
    {
        public string Title { get; set; }
        public QuestionDto Question { get; set; }
        public string Content { get; set; }
    }
}
