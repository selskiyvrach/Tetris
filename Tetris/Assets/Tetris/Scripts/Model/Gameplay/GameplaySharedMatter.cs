using System;
using UnityEngine;

namespace Tetris.Model.Gameplay
{
    public class GameplaySharedMatter : IGameplayHandle
    {
        public event Action OnBoardChanged;
        public Board Board { get; set; }
        public Shape CurrentShape { get; set; }
        public Vector2Int ShapePosition { get; set; }

        public void RaiseOnChanged() => 
            OnBoardChanged?.Invoke();
    }
}