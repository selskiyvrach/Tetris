namespace Tetris.Model.Actions
{
    public abstract class BoardAction
    {
        public abstract bool TryAct(bool[,] board);
    }
}