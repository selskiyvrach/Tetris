namespace Tetris.Model.Figures
{
    public class ClockwiseL : Figure
    {
        public override bool[,] Shape { get; set; } =
        {
            {true, true},
            {false, true},
            {false, true},
        };
    }
}