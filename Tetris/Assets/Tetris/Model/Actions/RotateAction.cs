using Tetris.Model.Figures;
using Tetris.Model.TwoDBoolArrayExtensions;
using UnityEngine;

namespace Tetris.Model.Actions
{
    public class RotateAction : FigureAction
    {
        public override bool TryAct(bool[,] board, Figure figure, ref Vector2Int position) => 
            TryRotate(board, figure, ref position);

        private static bool TryRotate(bool[,] board, Figure figure, ref Vector2Int position)
        {
            var boundsSizeBefore = new Vector2Int(figure.Shape.GetLength(0), figure.Shape.GetLength(1));
            board.RemoveProjection(figure.Shape, position);

            var newShape = figure.Shape.RotateCounterClockwise();
            var boundsSizeAfter = new Vector2Int(newShape.GetLength(0), newShape.GetLength(1));
            var positionDelta = (boundsSizeBefore - boundsSizeAfter) / 2;

            if (newShape.OverlapsOrOutOfBounds(board, position + positionDelta))
            {
                board.AddProjection(figure.Shape, position);
                return false;
            }

            figure.Shape = newShape;
            position += positionDelta;
            board.AddProjection(figure.Shape, position);
            return true;
        }
    }
}