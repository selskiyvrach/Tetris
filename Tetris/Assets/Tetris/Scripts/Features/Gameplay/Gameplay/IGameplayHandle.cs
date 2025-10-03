using UnityEngine;

namespace Features.Gameplay.Gameplay
{
    internal interface IGameplayHandle
    {
        Board Board { get; }
        Shape CurrentShape { get; set; }
        Vector2Int ShapePosition { get; set; }
        void RaiseOnChanged();
    }
}