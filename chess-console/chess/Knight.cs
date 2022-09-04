using board;

namespace chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "N";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Cols];
            Position pos = new Position(0, 0);

            pos.SetValues(Position.Row - 1, Position.Column - 2);
            if(Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row + 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            return matrix;
        }
    }
}
