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

        public Piece piece(int row, int column)
        {
            return _pieces[row, column];
        }

        public void addPiece(Piece p, Position pos)
        {
            _pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }
    }
}
