using UnityEngine;

namespace Features.Gameplay.Actions
{
    public class RotateAction : FigureAction
    {
        public override bool TryAct(bool[,] board, Shape shape, ref Vector2Int position) => 
            TryRotate(board, shape, ref position);

        private static bool TryRotate(bool[,] board, Shape shape, ref Vector2Int position)
        {
            var boundsSizeBefore = new Vector2Int(shape.Cells.GetLength(0), shape.Cells.GetLength(1));
            board.RemoveProjection(shape.Cells, position);

            var newShape = shape.Cells.RotateCounterClockwise();
            var boundsSizeAfter = new Vector2Int(newShape.GetLength(0), newShape.GetLength(1));
            var positionDelta = (boundsSizeBefore - boundsSizeAfter) / 2;

            if (newShape.OverlapsOrOutOfBounds(board, position + positionDelta))
            {
                board.AddProjection(shape.Cells, position);
                return false;
            }

            shape.Cells = newShape;
            position += positionDelta;
            board.AddProjection(shape.Cells, position);
            return true;
        }
    }
}