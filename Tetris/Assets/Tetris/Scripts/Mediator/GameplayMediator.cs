using System;
using Tetris.ModelDefinition;
using Tetris.ViewDefinition;

namespace Tetris.Mediator
{
    public class GameplayMediator : IDisposable
    {
        private readonly IGameplay _gameplay;
        private readonly IGameplayView _gameplayView;
        private readonly GameplayControlsMediator _gameplayControlsMediator;
        public event Action OnGameOver;

        public GameplayMediator(IGameplay gameplay, IGameplayView gameplayView)
        {
            _gameplay = gameplay;
            _gameplayView = gameplayView;
            _gameplayControlsMediator = new GameplayControlsMediator(_gameplayView.GameplayControls, _gameplay.ControlsListener);
            _gameplay.OnBoardStateChanged += _gameplayView.UpdateBoardState;
            _gameplay.OnGameOver += RaiseGameOver;
            _gameplay.Run();
        }

        private void RaiseGameOver() => 
            OnGameOver?.Invoke();

        public void Dispose()
        {
            _gameplayControlsMediator.Dispose();
            _gameplay.OnBoardStateChanged -= _gameplayView.UpdateBoardState;
            _gameplay.OnGameOver -= RaiseGameOver;
        }
    }
}