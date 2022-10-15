using Tetris.Model.Figures;
using Tetris.Model.TwoDBoolArrayExtensions;
using UnityEngine;

namespace Tetris.Model.Actions
{
    public class RotateAction : FigureAction
    {
        public override bool TryAct(bool[,] board, Figure figure, ref Vector2Int figurePositionOffset)
        {
            var sizeBefore = new Vector2Int(figure.Shape.GetLength(0), figure.Shape.GetLength(1));
            board.RemoveProjection(figure.Shape, figurePositionOffset);
            var newShape = figure.Shape.RotateCounterClockwise();
            var sizeAfter = new Vector2Int(newShape.GetLength(0), newShape.GetLength(1));
            var offsetDelta = (sizeBefore - sizeAfter) / 2;
            if (newShape.OverlapsOrOutOfBounds(board, figurePositionOffset + offsetDelta))
            {
                board.AddProjection(figure.Shape, figurePositionOffset);
                return false;
            }
            figure.Shape = newShape;
            figurePositionOffset += offsetDelta;
            board.AddProjection(figure.Shape, figurePositionOffset);
            return true;
        }
    }
}