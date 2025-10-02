using System;
using Tetris.ModelDefinition;
using Tetris.ViewDefinition;

namespace Tetris.Presenter
{
    public class GameplayMediator : IDisposable
    {
        private readonly IGameplay _gameplay;
        private readonly IGameplayView _gameplayView;
        private readonly GameplayPresenter _gameplayPresenter;
        public event Action OnGameOver;

        public GameplayMediator(IGameplay gameplay, IGameplayView gameplayView)
        {
            _gameplay = gameplay;
            _gameplayView = gameplayView;
            _gameplayPresenter = new GameplayPresenter(_gameplayView.GameplayControls, _gameplay.ControlsListener);
            _gameplay.OnBoardStateChanged += _gameplayView.UpdateBoardState;
            _gameplay.OnGameOver += RaiseGameOver;
            _gameplay.Run();
        }

        private void RaiseGameOver() => 
            OnGameOver?.Invoke();

        public void Dispose()
        {
            _gameplayPresenter.Dispose();
            _gameplay.OnBoardStateChanged -= _gameplayView.UpdateBoardState;
            _gameplay.OnGameOver -= RaiseGameOver;
        }
    }
}