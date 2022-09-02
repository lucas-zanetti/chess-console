using board;
using System.Collections.Generic;

namespace chess
{
    internal class ChessGame
    {
        public Board Board { get; private set; }
        public bool GameOver { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _taken;

        public ChessGame()
        {
            Board = new Board(8, 8);
            GameOver = false;
            Turn = 1;
            CurrentPlayer = Color.White;
            _pieces = new HashSet<Piece>();
            _taken = new HashSet<Piece>();
            AddPieces();            
        }

        public void DoMovement(Position start, Position end)
        {
            Piece p = Board.RemovePiece(start);
            p.IncreaseMoveQuantity();
            Piece takenPiece = Board.RemovePiece(end);
            Board.AddPiece(p, end);
            if(takenPiece != null) _taken.Add(takenPiece);
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

        public HashSet<Piece> TakenPieces(Color color)
        {
            HashSet<Piece> result = new HashSet<Piece>();
            foreach(Piece piece in _taken)
            {
                if(piece.Color == color) result.Add(piece);
            }
            return result;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> result = new HashSet<Piece>();
            foreach (Piece piece in _pieces)
            {
                if (piece.Color == color) result.Add(piece);
            }
            result.ExceptWith(TakenPieces(color));
            return result;
        }

        public void AddNewPiece(char column, int row, Piece piece)
        {
            Board.AddPiece(piece, new ChessPosition(column, row).ToPosition());
            _pieces.Add(piece);
        }

        private void AddPieces()
        {
            AddNewPiece('c', 1, new Rook(Board, Color.Black));
            AddNewPiece('d', 1, new King(Board, Color.Black));
            AddNewPiece('e', 1, new Rook(Board, Color.Black));
            AddNewPiece('c', 2, new Rook(Board, Color.Black));
            AddNewPiece('d', 2, new Rook(Board, Color.Black));
            AddNewPiece('e', 2, new Rook(Board, Color.Black));

            AddNewPiece('c', 7, new Rook(Board, Color.White));
            AddNewPiece('d', 7, new King(Board, Color.White));
            AddNewPiece('e', 7, new Rook(Board, Color.White));
            AddNewPiece('c', 8, new Rook(Board, Color.White));
            AddNewPiece('d', 8, new Rook(Board, Color.White));
            AddNewPiece('e', 8, new Rook(Board, Color.White));
        }
    }
}
