using board;

namespace chess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool IsThereOpponentPiece(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool FreeSquare(Position pos)
        {
            return Board.Piece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Cols];
            Position pos = new Position(0, 0);

            //White Pawns
            if (Color == Color.White)
            {
                pos.SetValues(Position.Row - 1, Position.Column);
                if(Board.ValidPosition(pos) && FreeSquare(pos)) matrix[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(pos) && FreeSquare(pos) && MoveQuantity == 0) matrix[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && IsThereOpponentPiece(pos)) matrix[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && IsThereOpponentPiece(pos)) matrix[pos.Row, pos.Column] = true;
            }
            //Black Pawns
            else
            {
                pos.SetValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && FreeSquare(pos)) matrix[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(pos) && FreeSquare(pos) && MoveQuantity == 0) matrix[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && IsThereOpponentPiece(pos)) matrix[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && IsThereOpponentPiece(pos)) matrix[pos.Row, pos.Column] = true;
            }

            return matrix;
        }
    }
}
