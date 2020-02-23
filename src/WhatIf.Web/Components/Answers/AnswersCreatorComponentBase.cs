using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using WhatIf.Database.Services.Questions;

namespace WhatIf.Web.Components.Answers
{
    public class AnswersCreatorComponentBase : ComponentBase
    {
        [Inject] public ProtectedSessionStorage Storage { get; set; }
        [Inject] public IQuestionService QuestionService { get; set; }

        public List<CreateAnswerModel> Answers { get; set; }

        protected CreateAnswerModel CurrentAnswer { get; set; }

        [Parameter] public EventCallback<List<CreateAnswerModel>> OnSubmit { get; set; }
        [Parameter] public Guid PlayerId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Answers = await Storage.GetAsync<List<CreateAnswerModel>>($"{PlayerId}-Answers");
            if (Answers != null)
            {
                await SetNextAnswer();
                return;
            }
            var questions = await QuestionService.GetQuestionsToAnswer(PlayerId);
            Answers = new List<CreateAnswerModel>();
            foreach (var question in questions)
            {
                Answers.Add(new CreateAnswerModel { Title = $"Question {questions.IndexOf(question) + 1}", Question = question, Content = string.Empty });
            }

            CurrentAnswer = Answers.First();
        }

        public Task Submit()
        {
            return OnSubmit.InvokeAsync(Answers);
        }

        protected async Task NextQuestion()
        {
            CurrentAnswer.IsAnswered = true;
            await SetNextAnswer();
        }

        private async Task SetNextAnswer()
        {
            CurrentAnswer = Answers.FirstOrDefault(x => !x.IsAnswered);
            await Storage.SetAsync($"{PlayerId}-Answers", Answers);
            if (CurrentAnswer is null)
            {
                await OnSubmit.InvokeAsync(Answers);
                await Storage.DeleteAsync($"{PlayerId}-Answers");
            }

        }
    }
}
