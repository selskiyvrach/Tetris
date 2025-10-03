using UnityEngine;

namespace Features.Gameplay.Actions
{
    public abstract class FigureAction
    {
        public abstract bool TryAct(Board board, Shape shape, ref Vector2Int position);
    }
}