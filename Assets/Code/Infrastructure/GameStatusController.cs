using Code.Gameplay.UI;
using Code.Infrastructure.MessageBrokers;
using UniRx;
using UnityEngine;

namespace Code.Infrastructure
{
    public class GameStatusController
    {
        private readonly EndGameWindow _endGameWindow;
        private readonly ScoreHolder _scoreHolder;
        private readonly CompositeDisposable _disposable = new ();

        public GameStatusController(EndGameWindow endGameWindow, ScoreHolder scoreHolder)
        {
            _endGameWindow = endGameWindow;
            _scoreHolder = scoreHolder;

            MessageBrokerHolder.Game
                .Receive<M_GameOver>()
                .Subscribe(_ => HandleGameOver())
                .AddTo(_disposable);   
        }

        public void Disable()
        {
            _disposable?.Dispose();
        }
        
        private void HandleGameOver()
        {
            _endGameWindow.ShowResult(_scoreHolder.CurrentScore);
            Time.timeScale = 0f;
        }
    }
}