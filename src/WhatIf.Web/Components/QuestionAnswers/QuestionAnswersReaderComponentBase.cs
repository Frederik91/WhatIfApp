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
        private PlayerDto _player;
        private bool _playerIsFinished;
        private bool _gameHasEnded;
        [Inject] private IAnswerService AnswerService { get; set; }
        [Inject] private IQuestionService QuestionService { get; set; }
        [Inject] private IPlayerService PlayerService { get; set; }

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

        public bool PlayerIsFinished
        {
            get => _playerIsFinished;
            set { _playerIsFinished = value; StateHasChanged(); }
        }

        public List<QuestionAnswerModel> QuestionAnswers { get; set; }

        public QuestionAnswerModel Current
        {
            get => _current;
            set { _current = value; StateHasChanged(); }
        }

        public PlayerDto Player
        {
            get => _player;
            set { _player = value; StateHasChanged(); }
        }

        public bool GameHasEnded
        {
            get => _gameHasEnded;
            set { _gameHasEnded = value; StateHasChanged(); }
        }

        protected override async Task OnParametersSetAsync()
        {
            _readAnswerHandler = Connection.On<Guid>("NextAnswer", OnReadAnswer);
            _readAnswerHandler = Connection.On("GameEnded", OnGameEnded);
            Player = await PlayerService.Get(PlayerId);
            var questionAnswers = await AnswerService.GetQuestionAnswersForPlayer(PlayerId);
            QuestionAnswers = new List<QuestionAnswerModel>();
            foreach (var questionAnswer in questionAnswers)
            {
                QuestionAnswers.Add(new QuestionAnswerModel
                {
                    Question = new ReadQuestionModel
                    {
                        Content = questionAnswer.Question.Content,
                        AssignedAnswerId = questionAnswer.Question.AssignedAnswerId,
                        Id = questionAnswer.Question.Id,
                    },
                    Answer = new ReadAnswerModel
                    {
                        Id = questionAnswer.Answer.Id,
                        Content = questionAnswer.Question.Content
                    }
                });
            }

            ShowStartupScreen = true;
        }

        private void OnGameEnded()
        {
            PlayerIsFinished = false;
            ReadAnswer = false;
            ReadQuestion = false;
            ShowStartupScreen = false;
            GameHasEnded = true;
        }

        private void OnReadAnswer(Guid answerId)
        {
            Current = QuestionAnswers.FirstOrDefault(x => x.Answer.Id == answerId);
            if (Current is null)
                return;

            if (Current.Answer.IsRead)
            {
                PlayerIsFinished = true;
                ReadAnswer = false;
                ReadQuestion = false;
                ShowStartupScreen = false;
                return;
            }

            ReadAnswer = true;
            ReadQuestion = false;
            ShowStartupScreen = false;
        }

        public void Dispose()
        {
            _readAnswerHandler?.Dispose();
        }

        protected async Task Start()
        {
            ShowStartupScreen = false;
            ReadQuestion = true;
            ReadAnswer = false;
            Current = QuestionAnswers.First();
            await Connection.SendAsync("RequestNextAnswer", _player.SessionId, Current.Question.AssignedAnswerId);
        }

        protected async Task OnNextQuestion()
        {
            ReadAnswer = false;
            ReadQuestion = true;
            Current.Question.IsRead = true;
            Current.Answer.IsRead = true;
            await AnswerService.MarkAnswerAsRead(Current.Answer.Id);
            await QuestionService.MarkQuestionAsRead(Current.Question.Id);
            var remainingAnswers = await AnswerService.GetRemainingAnswerCount(_player.SessionId);
            if (remainingAnswers > 0)
                await Connection.SendAsync("RequestNextAnswer", _player.SessionId, Current.Question.AssignedAnswerId);
            else
                await Connection.SendAsync("NotifyGameEnded", _player.SessionId);

        }
    }
}
