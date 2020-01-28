using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WhatIf.Web.Components
{
    public class QuestionsCreatorComponentBase : ComponentBase
    {
        [Parameter] public int QuestionCount { get; set; }

         public List<QuestionModel> Questions { get; set; }

         [Parameter] public EventCallback<List<QuestionModel>> OnSubmit { get; set; }

        protected override void OnParametersSet()
        {
            Questions = new List<QuestionModel>();
            for (var i = 1; i <= QuestionCount; i++)
            {
                Questions.Add(new QuestionModel { Name = "Question " + i, Content = "What happens if " });
            }
        }

        public Task Submit()
        {
            return OnSubmit.InvokeAsync(Questions);
        }
    }
}
