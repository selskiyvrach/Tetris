using System.Threading;
using System.Threading.Tasks;
using Tetris.Model.Actions;

namespace Tetris.Model.Gameplay
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
            SpawnNewFigure();
            _gameplayHandle.RaiseOnChanged();
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

        private void SpawnNewFigure()
        {
            _figureSpawner.SpawnNewFigure(out var figureFits);
            if (!figureFits)
                _gameOverHandler.RaiseOnGameOver();
        }

        private void PerformGameplayTick()
        {
            var figurePosition = _gameplayHandle.FigurePosition;
            if (_moveDownAction.TryAct(_gameplayHandle.Board, _gameplayHandle.CurrentFigure, ref figurePosition))
            {
                _gameplayHandle.RaiseOnChanged();
                _gameplayHandle.FigurePosition = figurePosition;
                return;
            }
            if (_clearFullRowsAction.TryAct(_gameplayHandle.Board))
            {
                _closeRowGapsAction.TryAct(_gameplayHandle.Board);
                _gameplayHandle.RaiseOnChanged();
            }
            SpawnNewFigure();
        }
        
        public void Dispose() =>
            _cancellationTokenSource.Cancel();
    }
}