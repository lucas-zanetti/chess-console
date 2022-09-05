using board;

namespace chess
{
    internal class King : Piece
    {
        private ChessGame _game;

        public King(Board board, Color color, ChessGame game) : base(board, color)
        {
            _game = game;
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

        private bool CheckRookToCastle(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.MoveQuantity == 0;
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

            //#Special Play Castle
            if (MoveQuantity == 0 && !_game.Check)
            {
                //#Castle Kingside
                Position posRookKing = new Position(Position.Row, Position.Column + 3);
                if (CheckRookToCastle(posRookKing))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null) matrix[Position.Row, Position.Column + 2] = true;
                }

                //#Castle Queenside
                Position posRookQueen = new Position(Position.Row, Position.Column - 4);
                if (CheckRookToCastle(posRookQueen))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null) matrix[Position.Row, Position.Column - 2] = true;
                }
            }

            return matrix;
        }
    }
}
