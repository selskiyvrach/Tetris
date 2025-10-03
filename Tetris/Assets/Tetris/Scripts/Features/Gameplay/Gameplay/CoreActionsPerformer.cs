using System;
using System.Threading;
using System.Threading.Tasks;
using Features.Gameplay.Actions;

namespace Features.Gameplay.Gameplay
{
    internal class CoreActionsPerformer
    {
        private readonly FigureSpawner _figureSpawner;
        private readonly IGameplayHandle _gameplayHandle;
        private readonly IGameOverHandler _gameOverHandler;
        private readonly MoveDownAction _moveDownAction = new();
        private readonly ClearFullRowsAction _clearFullRowsAction = new();
        private readonly CloseRowGapsAction _closeRowGapsAction = new();
        private CancellationTokenSource _cancellationTokenSource = new();

        public CoreActionsPerformer(IGameplayHandle gameplayHandle, IGameOverHandler gameOverHandler, FigureSpawner figureSpawner)
        {
            _gameplayHandle = gameplayHandle;
            _gameOverHandler = gameOverHandler;
            _figureSpawner = figureSpawner;
        }

        internal async void Run()
        {
            try
            {
                SpawnNewFigure();
                _cancellationTokenSource = new CancellationTokenSource();
                var token = _cancellationTokenSource.Token;
                while (true)
                {
                    try
                    {
                        await Task.Delay(350, token);
                    }
                    catch (TaskCanceledException exception)
                    {
                        break;
                    }
                    PerformGameplayTick();
                }
            }
            catch (Exception e)
            {
                throw; // TODO handle exception
            }
        }

        private void SpawnNewFigure()
        {
            _figureSpawner.SpawnNewFigure(out var figureFits);
            if (!figureFits)
                _gameOverHandler.RaiseOnGameOver();
        }

        private void PerformGameplayTick()
        {
            var figurePosition = _gameplayHandle.ShapePosition;
            if (_moveDownAction.TryAct(_gameplayHandle.Board, _gameplayHandle.CurrentShape, ref figurePosition))
            {
                _gameplayHandle.RaiseOnChanged();
                _gameplayHandle.ShapePosition = figurePosition;
                return;
            }
            if (_clearFullRowsAction.TryAct(_gameplayHandle.Board))
            {
                _closeRowGapsAction.TryAct(_gameplayHandle.Board);
                _gameplayHandle.RaiseOnChanged();
            }
            SpawnNewFigure();
        }
    }
}