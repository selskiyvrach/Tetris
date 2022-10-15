using Tetris.Model.TwoDBoolArrayExtensions;

namespace Tetris.Model.Actions
{
    public class CloseRowGapsAction : BoardAction
    {
        public override bool TryAct(bool[,] board)
        {
            while (true)
            {
                MoveOneLineDown(board, out bool switchesPerformed);
                if(!switchesPerformed)
                    break;
            }
            return true;
        }

        private static void MoveOneLineDown(bool[,] board, out bool switchesPerformed)
        {
            switchesPerformed = false;

            for (int y = board.GetLength(1) - 1; y >= 0; y--)
            {
                if(!board.RowIsEmpty(y))
                    continue;
                if(y == 0)
                    break;
                if(board.RowIsEmpty(y - 1))
                    continue;
                board.SwitchRows(y, y - 1);
                switchesPerformed = true;
            }
        }
    }
}