using UnityEngine;

namespace Features.Gameplay.Actions
{
    public abstract class MoveAction : FigureAction
    {
        protected abstract Vector2Int MoveDelta { get; }

        public sealed override bool TryAct(bool[,] board, Shape shape, ref Vector2Int position)
        {
            board.RemoveProjection(shape.Cells, position);
            if (shape.Cells.OverlapsOrOutOfBounds(board, position + MoveDelta))
            {
                board.AddProjection(shape.Cells, position);
                return false;
            }
            position += MoveDelta;
            board.AddProjection(shape.Cells, position);
            return true;
        }
    }
}