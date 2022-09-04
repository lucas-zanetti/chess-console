using board;

namespace chess
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "B";
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

            //Northwest
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.SetValues(pos.Row - 1, pos.Column - 1);
            }

            //Northeast
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.SetValues(pos.Row - 1, pos.Column + 1);
            }

            //Southeast
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.SetValues(pos.Row + 1, pos.Column + 1);
            }

            //Southeast
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.SetValues(pos.Row + 1, pos.Column - 1);
            }

            return matrix;
        }
    }
}
