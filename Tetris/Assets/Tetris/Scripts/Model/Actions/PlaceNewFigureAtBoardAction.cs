using UnityEngine;

namespace Tetris.Model.Actions
{
    public class PlaceNewFigureAtBoardAction : FigureAction
    {
        public override bool TryAct(Board board, Shape shape, ref Vector2Int position)
        {
            position = new Vector2Int(board.GetLength(0) / 2 - shape.Cells.GetLength(0) / 2, 0);
            var success = !shape.Cells.OverlapsOrOutOfBounds(board, position);
            board.AddProjection(shape.Cells, position);
            return success;
        }
    }
}