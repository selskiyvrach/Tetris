using UnityEngine;

namespace Features.Gameplay.Actions
{
    public class MoveRightAction : MoveAction
    {
        protected override Vector2Int MoveDelta { get; } = new(1, 0);
    }
}