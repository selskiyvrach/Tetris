using UnityEngine;

namespace Features.Gameplay.Actions
{
    public class MoveLeftAction : MoveAction
    {
        protected override Vector2Int MoveDelta { get; } = new(-1, 0);
    }
}