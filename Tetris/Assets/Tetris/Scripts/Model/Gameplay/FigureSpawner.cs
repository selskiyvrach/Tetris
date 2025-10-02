using Tetris.Model.Actions;

namespace Tetris.Model.Gameplay
{
    internal class FigureSpawner
    {
        private readonly IGameplayHandle _gameplayHandle;
        private readonly PlaceNewFigureAtBoardAction _placeNewFigureAction = new();

        public FigureSpawner(IGameplayHandle gameplayHandle) => 
            _gameplayHandle = gameplayHandle;

        internal void SpawnNewFigure(out bool figureFits)
        {
            _gameplayHandle.CurrentShape = Shape.Random;
            SetupPosition(out bool figureFitsLocal);
            _gameplayHandle.RaiseOnChanged();
            figureFits = figureFitsLocal;
        }

        private void SetupPosition(out bool figureFits)
        {
            var figurePosition = _gameplayHandle.ShapePosition;
            figureFits = _placeNewFigureAction.TryAct(
                _gameplayHandle.Board,
                _gameplayHandle.CurrentShape,
                ref figurePosition);
            _gameplayHandle.ShapePosition = figurePosition;
        }
    }
}