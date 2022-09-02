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
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('c', 1).ToPosition());
            Board.AddPiece(new King(Board, Color.Black), new ChessPosition('d', 1).ToPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('e', 1).ToPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('c', 2).ToPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('d', 2).ToPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('e', 2).ToPosition());

            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('c', 8).ToPosition());
            Board.AddPiece(new King(Board, Color.White), new ChessPosition('d', 8).ToPosition());
            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('e', 8).ToPosition());
            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('c', 7).ToPosition());
            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('d', 7).ToPosition());
            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('e', 7).ToPosition());
        }
    }
}
