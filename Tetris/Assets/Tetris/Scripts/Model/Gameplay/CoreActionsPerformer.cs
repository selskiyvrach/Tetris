using System.Threading;
using System.Threading.Tasks;
using Tetris.Model.Actions;

namespace Tetris.Model.Gameplay
{
    internal class CoreActionsPerformer
    {
        private readonly IGameplayHandle _gameplayHandle;
        private readonly IGameOverHandler _gameOverHandler;
        private readonly FiguresDataBase _figuresDataBase = new();
        private readonly MoveDownAction _moveDownAction = new();
        private readonly PlaceNewFigureAtBoardAction _placeNewFigureAction = new();
        private readonly ClearFullRowsAction _clearFullRowsAction = new();
        private readonly CloseRowGapsAction _closeRowGapsAction = new();
        private CancellationTokenSource _cancellationTokenSource = new();

        public CoreActionsPerformer(IGameplayHandle gameplayHandle, IGameOverHandler gameOverHandler)
        {
            _gameplayHandle = gameplayHandle;
            _gameOverHandler = gameOverHandler;
        }

        internal async void Run()
        {
            SpawnNewFigure();
            _gameplayHandle.RaiseOnChanged();
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;
            while (true)
            {
                await Task.Delay(350);
                if(token.IsCancellationRequested)
                    break;
                PerformGameplayTick();
            }
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

        private void SpawnNewFigure()
        {
            _gameplayHandle.CurrentFigure = _figuresDataBase.GetRandom();
            var figurePosition = _gameplayHandle.FigurePosition;
            var figureFit = _placeNewFigureAction.TryAct(
                _gameplayHandle.Board, 
                _gameplayHandle.CurrentFigure, 
                ref figurePosition);
            _gameplayHandle.FigurePosition = figurePosition;
            _gameplayHandle.RaiseOnChanged();
            if (!figureFit)
                GameOver();
        }

        private void GameOver()
        {
            _cancellationTokenSource.Cancel();
            _gameOverHandler.RaiseOnGameOver();
        }

        public void Dispose() =>
            _cancellationTokenSource.Cancel();
    }
}