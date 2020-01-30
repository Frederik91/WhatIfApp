using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using WhatIf.Core.Models;
using WhatIf.Core.Services;
using WhatIf.Database.Services.Answers;
using WhatIf.Database.Services.Questions;
using WhatIf.Web.Components.Questions;

namespace WhatIf.Web.Components.QuestionAnswers
{
    public class QuestionAnswersReaderComponentBase : ComponentBase, IDisposable
    {
        private IDisposable _readAnswerHandler;
        private bool _readQuestion;
        private bool _readAnswer;
        private bool _showStartupScreen;
        private QuestionAnswerModel _current;
        [Inject] private IAnswerService AnswerService { get; set; }
        [Inject] private IQuestionService QuestionService { get; set; }

        [Parameter] public Guid PlayerId { get; set; }
        [Parameter] public HubConnection Connection { get; set; }

        public bool ReadAnswer
        {
            get => _readAnswer;
            set { _readAnswer = value; StateHasChanged(); }
        }

        public bool ReadQuestion
        {
            get => _readQuestion;
            set { _readQuestion = value; StateHasChanged(); }
        }

        public bool ShowStartupScreen
        {
            get => _showStartupScreen;
            set { _showStartupScreen = value; StateHasChanged(); }
        }


        public List<QuestionAnswerModel> QuestionAnswers { get; set; }

        public QuestionAnswerModel Current
        {
            get => _current;
            set { _current = value; StateHasChanged(); }
        }

        protected override async Task OnParametersSetAsync()
        {
            _readAnswerHandler = Connection.On<Guid>("ReadAnswer", OnReadAnswer);
            var questionAnswers = await AnswerService.GetQuestionAnswersFromPlayer(PlayerId);
            QuestionAnswers = new List<QuestionAnswerModel>();
            foreach (var questionAnswer in questionAnswers)
            {
                QuestionAnswers.Add(new QuestionAnswerModel { Question = new ReadQuestionModel { Content = questionAnswer.Question.Content }, Answer = new ReadAnswerModel { Id = questionAnswer.Answer.Id, Content = questionAnswer.Question.Content } });
            }

            ShowStartupScreen = true;
        }


        private void OnReadAnswer(Guid answerId)
        {
            Current = QuestionAnswers.FirstOrDefault(x => x.Answer.Id == answerId);
            if (Current is null)
                return;

            ReadAnswer = true;
            ReadQuestion = false;
        }

        public void Dispose()
        {
            _readAnswerHandler?.Dispose();
        }

        protected async Task Start()
        {
            ReadQuestion = true;
            ReadAnswer = false;
            Current = QuestionAnswers.First();
            await Connection.SendAsync("RequestNextAnswer", Current.Question.AssignedAnswerId);
        }

        protected async Task OnNextQuestion()
        {
            ReadAnswer = false;
            ReadQuestion = true;
            await Connection.SendAsync("RequestNextAnswer", Current.Question.AssignedAnswerId);
            await AnswerService.MarkAnswerAsRead(Current.Answer.Id);
            await QuestionService.MarkQuestionAsRead(Current.Question.Id);
        }
    }
}
