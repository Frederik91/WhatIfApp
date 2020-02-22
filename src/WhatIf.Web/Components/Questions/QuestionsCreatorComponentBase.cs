using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WhatIf.Web.Components.Questions
{
    public class QuestionsCreatorComponentBase : ComponentBase
    {
        [Parameter] public int QuestionCount { get; set; }

        protected CreateQuestionModel CurrentQuestion { get; set; }

         public List<CreateQuestionModel> Questions { get; set; }

         [Parameter] public EventCallback<List<CreateQuestionModel>> OnSubmit { get; set; }

        protected override void OnParametersSet()
        {
            Questions = new List<CreateQuestionModel>();
            for (var i = 1; i <= QuestionCount; i++)
            {
                Questions.Add(new CreateQuestionModel { Title = "Question " + i, Content = "What happens if " });
            }

            CurrentQuestion = Questions.First();
        }

        protected async Task NextQuestion()
        {
            CurrentQuestion.IsSubmitted = true;
            CurrentQuestion = Questions.FirstOrDefault(x => !x.IsSubmitted);
            if (CurrentQuestion is null)
                await OnSubmit.InvokeAsync(Questions);
        }
    }
}
