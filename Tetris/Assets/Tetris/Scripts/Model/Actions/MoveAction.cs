using Tetris.Model.Figures;
using Tetris.Model.TwoDBoolArrayExtensions;
using UnityEngine;

namespace Tetris.Model.Actions
{
    public abstract class MoveAction : FigureAction
    {
        protected abstract Vector2Int MoveDelta { get; }

        public sealed override bool TryAct(bool[,] board, Figure figure, ref Vector2Int position)
        {
            board.RemoveProjection(figure.Shape, position);
            if (figure.Shape.OverlapsOrOutOfBounds(board, position + MoveDelta))
            {
                board.AddProjection(figure.Shape, position);
                return false;
            }
            position += MoveDelta;
            board.AddProjection(figure.Shape, position);
            return true;
        }
    }
}