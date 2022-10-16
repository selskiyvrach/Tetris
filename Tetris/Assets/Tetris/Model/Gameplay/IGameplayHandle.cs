using Tetris.Model.Figures;
using UnityEngine;

namespace Tetris.Model.Gameplay
{
    internal interface IGameplayHandle
    {
        bool[,] Board { get; }
        Figure CurrentFigure { get; set; }
        Vector2Int FigurePosition { get; set; }
        void RaiseOnChanged();
    }
}