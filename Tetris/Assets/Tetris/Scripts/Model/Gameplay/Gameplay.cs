using System;
using Tetris.Model.Figures;
using Tetris.Model.TwoDBoolArrayExtensions;
using Tetris.ModelDefinition;
using UnityEngine;

namespace Tetris.Model.Gameplay
{
    internal class Gameplay : IGameplay, IGameplayHandle, IDisposable, IGameOverHandler
    {
        private readonly CoreActionsPerformer _coreActionsPerformer;
        private readonly PlayerActionsPerformer _playerActionsPerformer;
        
        public event Action OnGameOver;
        public event Action<bool[,]> OnBoardStateChanged;
        public IGameplayControlsListener ControlsListener => _playerActionsPerformer;
        public bool[,] Board { get; } = new bool[10, 20];
        public Figure CurrentFigure { get; set; }
        public Vector2Int FigurePosition { get; set; }

        public Gameplay()
        {
            _coreActionsPerformer = new CoreActionsPerformer(this, this);
            _playerActionsPerformer = new PlayerActionsPerformer(this);
        }
        
        public void Run() =>
            _coreActionsPerformer.Run();

        public void RaiseOnChanged() => 
            OnBoardStateChanged?.Invoke(Board);

        public void RaiseOnGameOver() => 
            OnGameOver?.Invoke();

        public void Dispose()
        {
            Board.Clear();
            OnBoardStateChanged?.Invoke(Board);
            _coreActionsPerformer.Dispose();
        }
    }
}