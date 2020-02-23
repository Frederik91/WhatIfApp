using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.ProtectedBrowserStorage;

namespace WhatIf.Web.Components.Questions
{
    public class QuestionsCreatorComponentBase : ComponentBase
    {
        [Inject] public ProtectedSessionStorage Storage { get; set; }

        [Parameter] public int QuestionCount { get; set; }
        [Parameter] public Guid SessionId { get; set; }

        protected CreateQuestionModel CurrentQuestion { get; set; }

         public List<CreateQuestionModel> Questions { get; set; }

         [Parameter] public EventCallback<List<CreateQuestionModel>> OnSubmit { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Questions = await Storage.GetAsync<List<CreateQuestionModel>>($"{SessionId}-Questions");
            if (Questions != null)
            {
                await SetNextQuestion();
                return;
            }
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
            await Storage.SetAsync($"{SessionId}-Questions", Questions);
            await SetNextQuestion();
        }

        private async Task SetNextQuestion()
        {
            CurrentQuestion = Questions.FirstOrDefault(x => !x.IsSubmitted);
            if (CurrentQuestion is null)
            {
                await OnSubmit.InvokeAsync(Questions);
                await Storage.DeleteAsync($"{SessionId}-Questions");
            }
        }
    }
}
