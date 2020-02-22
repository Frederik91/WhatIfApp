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
        private bool _isReadingQuestion;
        private bool _isReadingAnswer;
        private bool _showStartupScreen;
        private QuestionAnswerModel _current;
        private PlayerDto _player;
        private bool _playerIsFinished;
        private bool _gameHasEnded;
        [Inject] private IAnswerService AnswerService { get; set; }
        [Inject] private IQuestionService QuestionService { get; set; }
        [Inject] private IPlayerService PlayerService { get; set; }
        [Inject] private ISessionService SessionService { get; set; }

        [Parameter] public Guid PlayerId { get; set; }
        [Parameter] public HubConnection Connection { get; set; }

        public bool IsReadingAnswer
        {
            get => _isReadingAnswer;
            set { _isReadingAnswer = value; StateHasChanged(); }
        }

        public bool IsReadingQuestion
        {
            get => _isReadingQuestion;
            set { _isReadingQuestion = value; StateHasChanged(); }
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
                        IsRead = questionAnswer.Question.IsRead,
                        IsCurrent = questionAnswer.Question.IsCurrent
                    },
                    Answer = new ReadAnswerModel
                    {
                        Id = questionAnswer.Answer.Id,
                        Content = questionAnswer.Answer.Content,
                        IsRead = questionAnswer.Answer.IsRead,
                        IsCurrent = questionAnswer.Answer.IsCurrent
                    }
                });
            }

            var session = await SessionService.Get(_player.SessionId);
            ShowStartupScreen = !session.ReadingRoundHasStarted;
            if (session.IsFinished)
                SetRoundFinished();

            if (!session.ReadingRoundHasStarted)
                return;
            
            if (QuestionAnswers.FirstOrDefault(x => x.Question.IsCurrent) is { } currentQuestion)
                ShowQuestion(currentQuestion);
            else if (QuestionAnswers.FirstOrDefault(x => x.Answer.IsCurrent) is { } currentAnswer)
                ShowAnswer(currentAnswer);
        }

        private async Task OnGameEnded()
        {
            PlayerIsFinished = false;
            IsReadingAnswer = false;
            IsReadingQuestion = false;
            ShowStartupScreen = false;
            GameHasEnded = true;
            if (Player.IsGameMaster)
                await SessionService.MarkSessionFinished(_player.SessionId);
        }

        private void OnReadAnswer(Guid answerId)
        {
            var qa = QuestionAnswers.FirstOrDefault(x => x.Answer.Id == answerId);
            if (qa is null)
                return;

            if (RoundIsFinished())
            {
                SetRoundFinished();
                return;
            }

            ShowAnswer(qa);
        }

        private bool RoundIsFinished()
        {
            return QuestionAnswers.All(x => x.Answer.IsRead && x.Question.IsRead);
        }

        private void SetRoundFinished()
        {
            PlayerIsFinished = true;
            IsReadingAnswer = false;
            IsReadingQuestion = false;
            ShowStartupScreen = false;
        }

        private void ShowAnswer(QuestionAnswerModel qa)
        {
            Current = qa;
            Current.Answer.IsCurrent = true;
            IsReadingAnswer = true;
            IsReadingQuestion = false;
            ShowStartupScreen = false;
        }

        public void Dispose()
        {
            _readAnswerHandler?.Dispose();
        }

        protected async Task Start()
        {
            ShowStartupScreen = false;
            var qa = QuestionAnswers.First();
            await SessionService.MarkReadingRoundStarted(_player.SessionId);
            await ReadQuestion(qa);
        }

        private async Task ReadQuestion(QuestionAnswerModel questionAnswerModel)
        {
            await QuestionService.MarkQuestionAsCurrent(Current.Question.Id);
            await AnswerService.MarkAnswerAsCurrent(Current.Question.AssignedAnswerId);
            await Connection.SendAsync("RequestNextAnswer", _player.SessionId, Current.Question.AssignedAnswerId);
            ShowQuestion(questionAnswerModel);
        }

        private void ShowQuestion(QuestionAnswerModel questionAnswerModel)
        {
            Current.Question.IsCurrent = true;
            Current = questionAnswerModel;
            IsReadingQuestion = true;
            IsReadingAnswer = false;
        }

        protected async Task OnNextQuestion()
        {
            IsReadingAnswer = false;
            IsReadingQuestion = true;

            Current.Question.IsRead = true;
            await QuestionService.MarkQuestionAsRead(Current.Question.Id);
            await AnswerService.MarkAnswerAsRead(Current.Answer.Id);
            var remainingAnswers = await AnswerService.GetRemainingAnswerCount(_player.SessionId);
            if (remainingAnswers > 0)
                await Connection.SendAsync("RequestNextAnswer", _player.SessionId, Current.Question.AssignedAnswerId);
            else
                await Connection.SendAsync("NotifyGameEnded", _player.SessionId);

        }
    }
}
