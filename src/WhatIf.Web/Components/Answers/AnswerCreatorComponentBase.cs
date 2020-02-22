using Microsoft.AspNetCore.Components;

namespace WhatIf.Web.Components.Answers
{
    public class AnswerCreatorComponentBase : ComponentBase
    {
        [Parameter]
        public CreateAnswerModel CreateAnswer { get; set; }


        [Parameter] public EventCallback OnSubmit { get; set; }

        protected void Submit()
        {
            OnSubmit.InvokeAsync(null);
        }
    }
}
