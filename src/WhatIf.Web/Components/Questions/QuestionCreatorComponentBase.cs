using Microsoft.AspNetCore.Components;

namespace WhatIf.Web.Components.Questions
{
    public class QuestionCreatorComponentBase : ComponentBase
    {
        [Parameter]
        public CreateQuestionModel CreateQuestion { get; set; }

    }
}
