using UnityEngine;

namespace Features.Gameplay.Actions
{
    public class MoveDownAction : MoveAction
    {
        protected override Vector2Int MoveDelta { get; } = new(0, 1);
    }
}