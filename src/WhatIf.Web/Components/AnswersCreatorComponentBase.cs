using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using WhatIf.Database.Services.Questions;

namespace WhatIf.Web.Components
{
    public class AnswersCreatorComponentBase : ComponentBase
    {
        [Inject] public IQuestionService QuestionService { get; set; }

        public List<AnswerModel> Answers { get; set; }

        [Parameter] public EventCallback<List<AnswerModel>> OnSubmit { get; set; }
        [Parameter] public Guid PlayerId { get; set; }

        protected override Task OnParametersSetAsync()
        {
            return Load(PlayerId);
        }

        public async Task Load(Guid playerId)
        {
            var questions = await QuestionService.GetQuestionsToAnswer(playerId);
            Answers = new List<AnswerModel>();
            foreach (var question in questions)
            {
                Answers.Add(new AnswerModel { Title = $"Question {questions.IndexOf(question) + 1}", Question = question, Content = string.Empty });
            }
        }

        public Task Submit()
        {
            return OnSubmit.InvokeAsync(Answers);
        }
    }
}
