using Tetris.Model.Figures;
using Tetris.Model.TwoDBoolArrayExtensions;
using UnityEngine;

namespace Tetris.Model.Actions
{
    public class PlaceNewFigureAtBoardAction : FigureAction
    {
        public override bool TryAct(bool[,] board, Figure figure, ref Vector2Int figurePositionOffset)
        {
            figurePositionOffset = new Vector2Int(board.GetLength(0) / 2 - figure.Shape.GetLength(0) / 2, 0);
            var success = !figure.Shape.OverlapsOrOutOfBounds(board, figurePositionOffset);
            board.AddProjection(figure.Shape, figurePositionOffset);
            return success;
        }
    }
}