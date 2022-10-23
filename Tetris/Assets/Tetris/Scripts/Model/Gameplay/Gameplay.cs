using System;
using Tetris.ModelDefinition;

namespace Tetris.Model.Gameplay
{
    internal class Gameplay : IGameplay, IDisposable, IGameOverHandler
    {
        private readonly FiguresDataBase _figuresDataBase = new();
        private readonly GameplaySharedMatter _gameplaySharedMatter;
        private readonly FigureSpawner _figureSpawner;
        private readonly CoreActionsPerformer _coreActionsPerformer;
        private readonly PlayerActionsPerformer _playerActionsPerformer;

        public event Action OnGameOver;
        public event Action<bool[,]> OnBoardStateChanged;
        public IGameplayControlsListener ControlsListener => _playerActionsPerformer;
        
        public Gameplay()
        {
            _gameplaySharedMatter = new GameplaySharedMatter(OnBoardChangedCallback);
            _figureSpawner = new FigureSpawner(_figuresDataBase, _gameplaySharedMatter);
            _coreActionsPerformer = new CoreActionsPerformer(_gameplaySharedMatter, gameOverHandler: this, _figureSpawner);
            _playerActionsPerformer = new PlayerActionsPerformer(_gameplaySharedMatter);
        }

        public void Run() =>
            _coreActionsPerformer.Run();

        public void RaiseOnGameOver() => 
            OnGameOver?.Invoke();

        public void Dispose()
        {
            _gameplaySharedMatter.Dispose();
            _coreActionsPerformer.Dispose();
        }

        private void OnBoardChangedCallback(bool[,] board) => 
            OnBoardStateChanged?.Invoke(board);
    }
}