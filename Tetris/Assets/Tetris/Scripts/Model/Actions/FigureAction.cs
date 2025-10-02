using UnityEngine;

namespace Tetris.Model.Actions
{
    public abstract class FigureAction
    {
        public abstract bool TryAct(Board board, Shape shape, ref Vector2Int position);
    }
}