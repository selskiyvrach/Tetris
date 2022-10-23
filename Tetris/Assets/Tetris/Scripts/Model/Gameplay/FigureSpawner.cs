using Tetris.Model.Actions;

namespace Tetris.Model.Gameplay
{
    internal class FigureSpawner
    {
        private readonly IGameplayHandle _gameplayHandle;
        private readonly IFiguresDataBase _figuresDataBase;
        private readonly PlaceNewFigureAtBoardAction _placeNewFigureAction = new();

        public FigureSpawner(IFiguresDataBase figuresDataBase, IGameplayHandle gameplayHandle)
        {
            _figuresDataBase = figuresDataBase;
            _gameplayHandle = gameplayHandle;
        }

        internal void SpawnNewFigure(out bool figureFits)
        {
            _gameplayHandle.CurrentFigure = _figuresDataBase.GetRandom();
            SetupPosition(out bool figureFitsLocal);
            _gameplayHandle.RaiseOnChanged();
            figureFits = figureFitsLocal;
        }

        private void SetupPosition(out bool figureFits)
        {
            var figurePosition = _gameplayHandle.FigurePosition;
            figureFits = _placeNewFigureAction.TryAct(
                _gameplayHandle.Board,
                _gameplayHandle.CurrentFigure,
                ref figurePosition);
            _gameplayHandle.FigurePosition = figurePosition;
        }
    }
}