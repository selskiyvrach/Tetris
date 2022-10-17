using Tetris.Model.Figures;
using Tetris.Model.TwoDBoolArrayExtensions;
using UnityEngine;

namespace Tetris.Model.Actions
{
    public class PlaceNewFigureAtBoardAction : FigureAction
    {
        public override bool TryAct(bool[,] board, Figure figure, ref Vector2Int position)
        {
            position = new Vector2Int(board.GetLength(0) / 2 - figure.Shape.GetLength(0) / 2, 0);
            var success = !figure.Shape.OverlapsOrOutOfBounds(board, position);
            board.AddProjection(figure.Shape, position);
            return success;
        }
    }
}