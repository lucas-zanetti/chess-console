using board;

namespace chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "K";
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

            //North
            pos.SetValues(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            //Northeast
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            //East
            pos.SetValues(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            //Southeast
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            //South
            pos.SetValues(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            //Southwest
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            //West
            pos.SetValues(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            //Northwest
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) matrix[pos.Row, pos.Column] = true;

            return matrix;
        }
    }
}
