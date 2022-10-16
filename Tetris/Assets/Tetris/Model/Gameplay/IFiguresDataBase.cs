using Tetris.Model.Figures;

namespace Tetris.Model.Gameplay
{
    internal interface IFiguresDataBase
    {
        Figure GetRandom();
    }
}