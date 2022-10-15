using System;
using System.Threading;
using System.Threading.Tasks;
using Tetris.Model.Actions;
using Tetris.Model.Figures;
using Tetris.Model.TwoDBoolArrayExtensions;
using Tetris.ModelDefinition;
using UnityEngine;

namespace Tetris.Model
{
    public class Tetris : ITetrisModel
    {
        private readonly FiguresDataBase _figuresDataBase = new();
        
        private readonly MoveDownAction _moveDownAction = new();
        private readonly MoveLeftAction _moveLeftAction = new();
        private readonly MoveRightAction _moveRightAction = new();
        private readonly RotateAction _rotateAction = new();
        private readonly PlaceNewFigureAtBoardAction _placeNewFigureAction = new();

        private readonly ClearFullRowsAction _clearFullRowsAction = new();
        private readonly CloseRowGapsAction _closeRowGapsAction = new();

        private CancellationTokenSource _gameOverCancellationTokenSource = new();

        private readonly bool[,] _board = new bool[10, 20];
        private Figure _currentFigure;
        private Vector2Int _figurePosition;

        public event Action<bool[,]> OnBoardStateChanged;

        public async void Start()
        {
            SpawnNewFigure();
            var token = _gameOverCancellationTokenSource.Token;
            while (true)
            {
                await Task.Delay(350);
                if(token.IsCancellationRequested)
                    break;
                PerformGameplayTick();
            }
        }
        
        public void MoveLeft() => 
            PerformFigureAction(_moveLeftAction);

        public void MoveRight() => 
            PerformFigureAction(_moveRightAction);

        public void MoveDown() => 
            PerformFigureAction(_moveDownAction);

        public void Rotate() => 
            PerformFigureAction(_rotateAction);

        private void PerformGameplayTick()
        {
            var figurePosition = _figurePosition;
            if (_moveDownAction.TryAct(_board, _currentFigure, ref figurePosition))
            {
                OnBoardStateChanged?.Invoke(_board);
                _figurePosition = figurePosition;
                return;
            }
            if (_clearFullRowsAction.TryAct(_board))
            {
                _closeRowGapsAction.TryAct(_board);
                OnBoardStateChanged?.Invoke(_board);
            }
            SpawnNewFigure();
        }

        private void SpawnNewFigure()
        {
            _currentFigure = _figuresDataBase.GetRandom();
            var figurePosition = _figurePosition;
            var figureFit = _placeNewFigureAction.TryAct(_board, _currentFigure, ref figurePosition);
            _figurePosition = figurePosition;
            OnBoardStateChanged?.Invoke(_board);
            if (!figureFit)
                RestartGame();
        }

        private void RestartGame()
        {
            _board.Clear();
            _currentFigure = null;
            _gameOverCancellationTokenSource.Cancel();
            _gameOverCancellationTokenSource = new();
            Start();
        }

        private void PerformFigureAction(FigureAction action)
        {
            var figurePosition = _figurePosition;
            if (!action.TryAct(_board, _currentFigure, ref figurePosition))
                return;
            _figurePosition = figurePosition;
            OnBoardStateChanged?.Invoke(_board);
        }
    }
}
