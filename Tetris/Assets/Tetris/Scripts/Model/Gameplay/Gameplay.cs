using System;
using Tetris.ModelDefinition;

namespace Tetris.Model.Gameplay
{
    internal class Gameplay : IGameplay, IDisposable, IGameOverHandler
    {
        private readonly GameplaySharedMatter _gameplaySharedMatter;
        private readonly CoreActionsPerformer _coreActionsPerformer;
        private readonly PlayerActionsPerformer _playerActionsPerformer;
        
        public event Action OnGameOver;
        public event Action<bool[,]> OnBoardStateChanged;
        public IGameplayControlsListener ControlsListener => _playerActionsPerformer;
        
        public Gameplay()
        {
            _gameplaySharedMatter = new GameplaySharedMatter(boardState => OnBoardStateChanged?.Invoke(boardState));
            _coreActionsPerformer = new CoreActionsPerformer(_gameplaySharedMatter, this);
            _playerActionsPerformer = new PlayerActionsPerformer(_gameplaySharedMatter);
        }
        
        public void Run() =>
            _coreActionsPerformer.Run();

        public void RaiseOnGameOver() => 
            OnGameOver?.Invoke();

        public void Dispose()
        {
            _gameplaySharedMatter.Reset();
            _coreActionsPerformer.Dispose();
        }
    }
}