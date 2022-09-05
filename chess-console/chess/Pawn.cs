using board;

namespace chess
{
    internal class Pawn : Piece
    {
        private ChessGame _game;

        public Pawn(Board board, Color color, ChessGame game) : base(board, color)
        {
            _game = game;
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
                if (Board.ValidPosition(pos) && FreeSquare(pos)) matrix[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(pos) && FreeSquare(pos) && MoveQuantity == 0) matrix[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && IsThereOpponentPiece(pos)) matrix[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && IsThereOpponentPiece(pos)) matrix[pos.Row, pos.Column] = true;

                //#Special Play 'En Passant'
                if (Position.Row == 3)
                {
                    Position leftSide = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(leftSide) && IsThereOpponentPiece(leftSide) && Board.Piece(leftSide) == _game.EligibleEnPassant) matrix[leftSide.Row - 1, leftSide.Column] = true;
                    Position rightSide = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(rightSide) && IsThereOpponentPiece(rightSide) && Board.Piece(rightSide) == _game.EligibleEnPassant) matrix[rightSide.Row - 1, rightSide.Column] = true;
                }
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

                //#Special Play 'En Passant'
                if (Position.Row == 4)
                {
                    Position leftSide = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(leftSide) && IsThereOpponentPiece(leftSide) && Board.Piece(leftSide) == _game.EligibleEnPassant) matrix[leftSide.Row + 1, leftSide.Column] = true;
                    Position rightSide = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(rightSide) && IsThereOpponentPiece(rightSide) && Board.Piece(rightSide) == _game.EligibleEnPassant) matrix[rightSide.Row + 1, rightSide.Column] = true;
                }
            }

            return matrix;
        }
    }
}
