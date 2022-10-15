namespace Tetris.Model.Figures
{
    public class Line : Figure
    {
        public override bool[,] Shape { get; set; } =
        {
            {true},
            {true},
            {true},
            {true}
        };
    }
}