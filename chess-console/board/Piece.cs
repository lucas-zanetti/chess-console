namespace board
{
    internal abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveQuantity { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            Board = board;
            MoveQuantity = 0;
        }

        public void IncreaseMoveQuantity()
        {
            MoveQuantity++;
        }

        public bool ExistsPossibleMoviments()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Cols; j++)
                {
                    if (mat[i, j]) return true;
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMovements()[pos.Row, pos.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
