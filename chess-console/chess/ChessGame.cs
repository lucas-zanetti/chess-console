using board;

namespace chess
{
    internal class ChessGame
    {
        public Board Board { get; private set; }
        public bool GameOver { get; private set; }
        private int turn;
        private Color currentPlayer;

        public ChessGame()
        {
            Board = new Board(8, 8);
            GameOver = false;
            turn = 1;
            currentPlayer = Color.White;
            AddPieces();
        }

        public void DoMovement(Position start, Position end)
        {
            Piece p = Board.RemovePiece(start);
            p.IncreaseMoveQuantity();
            Piece takenPiece = Board.RemovePiece(end);
            Board.AddPiece(p, end);
        }

        private void AddPieces()
        {
            Board.AddPiece(new Pawn(Board, Color.Black), new ChessPosition('c', 1).ToPosition());
            Board.AddPiece(new Queen(Board, Color.White), new ChessPosition('d', 5).ToPosition());
        }
    }
}
