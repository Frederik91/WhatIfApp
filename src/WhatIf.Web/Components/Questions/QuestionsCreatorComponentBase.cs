using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WhatIf.Web.Components.Questions
{
    public class QuestionsCreatorComponentBase : ComponentBase
    {
        [Parameter] public int QuestionCount { get; set; }

         public List<CreateQuestionModel> Questions { get; set; }

         [Parameter] public EventCallback<List<CreateQuestionModel>> OnSubmit { get; set; }

        protected override void OnParametersSet()
        {
            Questions = new List<CreateQuestionModel>();
            for (var i = 1; i <= QuestionCount; i++)
            {
                Questions.Add(new CreateQuestionModel { Name = "Question " + i, Content = "What happens if " });
            }
        }

        public Task Submit()
        {
            return OnSubmit.InvokeAsync(Questions);
        }
    }
}
