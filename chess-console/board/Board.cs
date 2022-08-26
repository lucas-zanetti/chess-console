namespace board
{
    internal class Board
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        private Piece[,] _pieces;

        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            _pieces = new Piece[Rows, Cols];
        }
    }
}
