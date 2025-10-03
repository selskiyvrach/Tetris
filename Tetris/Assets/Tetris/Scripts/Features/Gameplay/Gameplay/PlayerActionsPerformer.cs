using Features.Gameplay.Actions;

namespace Features.Gameplay.Gameplay
{
    internal class PlayerActionsPerformer : IGameplayControlsListener
    {
        private readonly IGameplayHandle _gameplayHandle;
        private readonly MoveDownAction _moveDownAction = new();
        private readonly MoveLeftAction _moveLeftAction = new();
        private readonly MoveRightAction _moveRightAction = new();
        private readonly RotateAction _rotateAction = new();

        public PlayerActionsPerformer(IGameplayHandle gameplayHandle) => 
            _gameplayHandle = gameplayHandle;

        public void MoveLeft() => 
            PerformFigureAction(_moveLeftAction);

        public void MoveRight() => 
            PerformFigureAction(_moveRightAction);

        public void MoveDown() => 
            PerformFigureAction(_moveDownAction);

        public void Rotate() => 
            PerformFigureAction(_rotateAction);
        
        private void PerformFigureAction(FigureAction action)
        {
            var figurePosition = _gameplayHandle.ShapePosition;
            if (!action.TryAct(_gameplayHandle.Board, _gameplayHandle.CurrentShape, ref figurePosition))
                return;
            _gameplayHandle.ShapePosition = figurePosition;
            _gameplayHandle.RaiseOnChanged();
        }
    }
}