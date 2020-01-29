using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using WhatIf.Database.Services.Questions;

namespace WhatIf.Web.Components.Answers
{
    public class AnswersCreatorComponentBase : ComponentBase
    {
        [Inject] public IQuestionService QuestionService { get; set; }

        public List<CreateAnswerModel> Answers { get; set; }

        [Parameter] public EventCallback<List<CreateAnswerModel>> OnSubmit { get; set; }
        [Parameter] public Guid PlayerId { get; set; }

        protected override Task OnParametersSetAsync()
        {
            return Load(PlayerId);
        }

        public async Task Load(Guid playerId)
        {
            var questions = await QuestionService.GetQuestionsToAnswer(playerId);
            Answers = new List<CreateAnswerModel>();
            foreach (var question in questions)
            {
                Answers.Add(new CreateAnswerModel { Title = $"Question {questions.IndexOf(question) + 1}", Question = question, Content = string.Empty });
            }
        }

        public Task Submit()
        {
            return OnSubmit.InvokeAsync(Answers);
        }
    }
}
