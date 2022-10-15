using Tetris.Model.Figures;

namespace Tetris.Model
{
    internal interface IFiguresDataBase
    {
        Figure GetRandom();
    }
}