using Tetris.Model.TwoDBoolArrayExtensions;

namespace Tetris.Model.Actions
{
    public class ClearFullRowsAction : BoardAction
    {
        public override bool TryAct(bool[,] board)
        {
            var atLeastOneCleared = false;
            for (int y = 0; y < board.GetLength(1); y++)
            for (int x = 0; x < board.GetLength(0); x++)
            {
                if (!board[x, y])
                    break;
                if (x < board.GetLength(0) - 1)
                    continue;
                board.ClearRow(y);
                atLeastOneCleared = true;
            }
            return atLeastOneCleared;
        }
    }
}