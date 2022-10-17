namespace Tetris.Model.Figures
{
    public class Box : Figure
    {
        public override bool[,] Shape { get; set; } =
        {
            {true, true}, 
            {true, true}
        };
    }
}