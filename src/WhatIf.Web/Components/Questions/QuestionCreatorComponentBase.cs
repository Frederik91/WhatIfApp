using Microsoft.AspNetCore.Components;

namespace WhatIf.Web.Components.Questions
{
    public class QuestionCreatorComponentBase : ComponentBase
    {
        [Parameter]
        public CreateQuestionModel CreateQuestion { get; set; }


        [Parameter] public EventCallback OnSubmit { get; set; }

        protected void Submit()
        {
            OnSubmit.InvokeAsync(null);
        }
    }
}
