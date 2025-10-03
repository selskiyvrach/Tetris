namespace Features.Gameplay.Actions
{
    public class CloseRowGapsAction : BoardAction
    {
        public override bool TryAct(bool[,] board)
        {
            CollapseRowGaps(board);
            return true;
        }

        private static void CollapseRowGaps(bool[,] board)
        {
            while (true)
            {
                PerformDownTopSetOfSwaps(board, out bool switchesPerformed);
                if (!switchesPerformed)
                    break;
            }
        }

        private static void PerformDownTopSetOfSwaps(bool[,] board, out bool switchesPerformed)
        {
            switchesPerformed = false;
            for (int y = board.GetLength(1) - 1; y > 0; y--)
            {
                if(!board.RowIsEmpty(y))
                    continue;
                if(board.RowIsEmpty(y - 1))
                    continue;
                board.SwapRows(y, y - 1);
                switchesPerformed = true;
            }
        }
    }
}