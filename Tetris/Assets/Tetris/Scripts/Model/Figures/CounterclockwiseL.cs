namespace Tetris.Model.Figures
{
    public class CounterclockwiseL : Figure
    {
        public override bool[,] Shape { get; set; } =
        {
            {true, true},
            {true, false},
            {true, false},
        };
    }
}