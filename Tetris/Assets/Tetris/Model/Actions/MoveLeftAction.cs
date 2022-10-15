using UnityEngine;

namespace Tetris.Model.Actions
{
    public class MoveLeftAction : MoveAction
    {
        protected override Vector2Int MoveDelta { get; } = new(-1, 0);
    }
}