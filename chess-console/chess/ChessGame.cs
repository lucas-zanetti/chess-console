using board;

namespace chess
{
    internal class ChessGame
    {
        public Board Board { get; private set; }
        public bool GameOver { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            GameOver = false;
            Turn = 1;
            CurrentPlayer = Color.White;
            AddPieces();
        }

        public void DoMovement(Position start, Position end)
        {
            Piece p = Board.RemovePiece(start);
            p.IncreaseMoveQuantity();
            Piece takenPiece = Board.RemovePiece(end);
            Board.AddPiece(p, end);
        }

        public void ExecutePlay(Position start, Position end)
        {
            DoMovement(start, end);
            Turn++;
            SwitchPlayer();
        }

        public void ValidateStartPosition(Position pos)
        {
            if (Board.Piece(pos) == null) throw new BoardException("There is no piece in chosen start position!");
            if (CurrentPlayer != Board.Piece(pos).Color) throw new BoardException("The chosen piece doesn't belongs to the current player!");
            if (!Board.Piece(pos).ExistsPossibleMoviments()) throw new BoardException("There are no possible moviments for the chosen piece!");
        }

        public void ValidateEndPosition(Position start, Position end)
        {
            if (!Board.Piece(start).CanMoveTo(end)) throw new BoardException("Invalid end position!");
        }

        private void SwitchPlayer()
        {
            if (CurrentPlayer == Color.White) CurrentPlayer = Color.Black;
            else CurrentPlayer = Color.White;
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
