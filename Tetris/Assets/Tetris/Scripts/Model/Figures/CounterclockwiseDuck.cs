namespace Tetris.Model.Figures
{
    public class CounterclockwiseDuck : Figure
    {
        public override bool[,] Shape { get; set; } =
        {
            {true, false},
            {true, true},
            {false, true},
        };
    }
}