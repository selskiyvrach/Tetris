using UnityEngine;

namespace Tetris.Model.Actions
{
    public class MoveDownAction : MoveAction
    {
        protected override Vector2Int MoveDelta { get; } = new(0, 1);
    }
}