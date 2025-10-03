namespace Features.Gameplay
{
    public sealed class Board
    {
        private readonly uint[] _rows;

        public int Columns { get; }
        public int Rows { get; }

        public Board(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
            _rows = new uint[rows];
        }

        public bool ContainsPoint(int x, int y) =>
            (_rows[y] & (1u << x)) != 0;

        public void SetCell(int x, int y) =>
            _rows[y] |= 1u << x;

        public void ClearCell(int x, int y) =>
            _rows[y] &= ~(1u << x);

        public void ClearRow(int y) => 
            _rows[y] = 0;

        public bool RowIsFull(int y) => 
            _rows[y] == (1u << Columns) - 1;
    }
}