using Tetris.Model.Actions;
using Tetris.ModelDefinition;

namespace Tetris.Model.Gameplay
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
            var figurePosition = _gameplayHandle.FigurePosition;
            if (!action.TryAct(_gameplayHandle.Board, _gameplayHandle.CurrentFigure, ref figurePosition))
                return;
            _gameplayHandle.FigurePosition = figurePosition;
            _gameplayHandle.RaiseOnChanged();
        }
    }
}