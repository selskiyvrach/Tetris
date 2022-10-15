using Tetris.Model.Figures;
using Tetris.Model.TwoDBoolArrayExtensions;
using UnityEngine;

namespace Tetris.Model.Actions
{
    public abstract class MoveAction : FigureAction
    {
        protected abstract Vector2Int MoveDelta { get; }

        public sealed override bool TryAct(bool[,] board, Figure figure, ref Vector2Int figurePositionOffset)
        {
            board.RemoveProjection(figure.Shape, figurePositionOffset);
            if (figure.Shape.OverlapsOrOutOfBounds(board, figurePositionOffset + MoveDelta))
            {
                board.AddProjection(figure.Shape, figurePositionOffset);
                return false;
            }
            figurePositionOffset += MoveDelta;
            board.AddProjection(figure.Shape, figurePositionOffset);
            return true;
        }
    }
}