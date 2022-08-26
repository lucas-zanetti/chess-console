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

        public Piece Piece(int row, int column)
        {
            return _pieces[row, column];
        }

        public Piece Piece(Position pos)
        {
            return _pieces[pos.Row, pos.Column];
        }

        public bool ExistsPiece(Position pos)
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }

        public void AddPiece(Piece p, Position pos)
        {
            if (ExistsPiece(pos)) throw new BoardException("There is a piece already occupying this position!");
            _pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }

        public bool ValidPosition(Position pos)
        {
            if (pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Cols) return false;
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos)) throw new BoardException("Invalid position!");
        }
    }
}
