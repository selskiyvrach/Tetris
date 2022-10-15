using UnityEngine;

namespace Tetris.Model.Actions
{
    public class MoveRightAction : MoveAction
    {
        protected override Vector2Int MoveDelta { get; } = new(1, 0);
    }
}