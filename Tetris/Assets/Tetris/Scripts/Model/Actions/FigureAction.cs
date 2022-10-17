using Tetris.Model.Figures;
using UnityEngine;

namespace Tetris.Model.Actions
{
    public abstract class FigureAction
    {
        public abstract bool TryAct(bool[,] board, Figure figure, ref Vector2Int position);
    }
}