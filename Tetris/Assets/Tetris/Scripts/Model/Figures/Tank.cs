namespace Tetris.Model.Figures
{
    public class Tank : Figure
    {
        public override bool[,] Shape { get; set; } =
        {
            {true, false},
            {true, true},
            {true, false},
        };
    }
}