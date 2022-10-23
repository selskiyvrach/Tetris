using System;
using Tetris.Model.Figures;
using Tetris.Model.TwoDBoolArrayExtensions;
using UnityEngine;

namespace Tetris.Model.Gameplay
{
    public class GameplaySharedMatter : IGameplayHandle
    {
        private readonly Action<bool[,]> _onBoardChangedCallback;
        public bool[,] Board { get; } = new bool[10, 20];
        public Figure CurrentFigure { get; set; }
        public Vector2Int FigurePosition { get; set; }

        public GameplaySharedMatter(Action<bool[,]> onBoardChangedCallback) => 
            _onBoardChangedCallback = onBoardChangedCallback;

        public void RaiseOnChanged() => 
            _onBoardChangedCallback?.Invoke(Board);

        public void Dispose()
        {
            Board.Clear();
            _onBoardChangedCallback?.Invoke(Board);
        }
    }
}