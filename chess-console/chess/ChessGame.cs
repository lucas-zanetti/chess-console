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
        public bool Check { get; private set; }
        public Piece EligibleEnPassant { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _taken;

        public ChessGame()
        {
            Board = new Board(8, 8);
            GameOver = false;
            Turn = 1;
            CurrentPlayer = Color.White;
            Check = false;
            EligibleEnPassant = null;
            _pieces = new HashSet<Piece>();
            _taken = new HashSet<Piece>();
            AddPieces();
        }

        public Piece DoMovement(Position start, Position end)
        {
            Piece p = Board.RemovePiece(start);
            p.IncreaseMoveQuantity();
            Piece takenPiece = Board.RemovePiece(end);
            Board.AddPiece(p, end);
            if (takenPiece != null) _taken.Add(takenPiece);

            //#Special Play Castle Kingside
            if (p is King && end.Column == start.Column + 2)
            {
                Position startRook = new Position(start.Row, start.Column + 3);
                Position endRook = new Position(start.Row, start.Column + 1);
                Piece rook = Board.RemovePiece(startRook);
                rook.IncreaseMoveQuantity();
                Board.AddPiece(rook, endRook);
            }

            //#Special Play Castle Queenside
            if (p is King && end.Column == start.Column - 2)
            {
                Position startRook = new Position(start.Row, start.Column - 4);
                Position endRook = new Position(start.Row, start.Column - 1);
                Piece rook = Board.RemovePiece(startRook);
                rook.IncreaseMoveQuantity();
                Board.AddPiece(rook, endRook);
            }

            //#Special Play 'En Passant'
            if(p is Pawn)
            {
                if(start.Column != end.Column && takenPiece == null)
                {
                    Position posPawn;
                    if(p.Color == Color.White) posPawn = new Position(end.Row + 1, end.Column);
                    else posPawn = new Position(end.Row - 1, end.Column);
                    takenPiece = Board.RemovePiece(posPawn);
                    _taken.Add(takenPiece);
                }
            }

            return takenPiece;
        }

        public void ExecutePlay(Position start, Position end)
        {
            Piece takenPiece = DoMovement(start, end);
            if (IsCheck(CurrentPlayer))
            {
                UndoMovement(start, end, takenPiece);
                throw new BoardException("Player can't put his King in check condition!");
            }

            Piece p = Board.Piece(end);

            //#Special Play Promotion
            if (p is Pawn)
            {
                if ((p.Color == Color.White && end.Row == 0) || (p.Color == Color.Black && end.Row == 7))
                {
                    p = Board.RemovePiece(end);
                    _pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.AddPiece(queen, end);
                    _pieces.Add(queen);
                }
            }

            if (IsCheck(OpponentColor(CurrentPlayer))) Check = true;
            else Check = false;

            if (IsMate(OpponentColor(CurrentPlayer))) GameOver = true;
            else
            {
                Turn++;
                SwitchPlayer();
            }

            //#Special Play 'En Passant'
            if (p is Pawn && (end.Row == start.Row - 2 || end.Row == start.Row + 2)) EligibleEnPassant = p;
            else EligibleEnPassant = null;
        }

        public void UndoMovement(Position start, Position end, Piece takenPiece)
        {
            Piece p = Board.RemovePiece(end);
            p.DecreaseMoveQuantity();
            if (takenPiece != null)
            {
                Board.AddPiece(takenPiece, end);
                _taken.Remove(takenPiece);
            }
            Board.AddPiece(p, start);

            //#Special Play Castle Kingside
            if (p is King && end.Column == start.Column + 2)
            {
                Position startRook = new Position(start.Row, start.Column + 3);
                Position endRook = new Position(start.Row, start.Column + 1);
                Piece rook = Board.RemovePiece(endRook);
                rook.DecreaseMoveQuantity();
                Board.AddPiece(rook, startRook);
            }

            //#Special Play Castle Queenside
            if (p is King && end.Column == start.Column - 2)
            {
                Position startRook = new Position(start.Row, start.Column - 4);
                Position endRook = new Position(start.Row, start.Column - 1);
                Piece rook = Board.RemovePiece(endRook);
                rook.DecreaseMoveQuantity();
                Board.AddPiece(rook, startRook);
            }

            //#Special Play 'En Passant'
            if (p is Pawn)
            {
                if (start.Column != end.Column && takenPiece == EligibleEnPassant)
                {
                    Piece pawn = Board.RemovePiece(end);
                    Position posPawn;
                    if (p.Color == Color.White) posPawn = new Position(3, end.Column);
                    else posPawn = new Position(4, end.Column);
                    Board.AddPiece(pawn, posPawn);
                }
            }
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
            foreach (Piece piece in _taken)
            {
                if (piece.Color == color) result.Add(piece);
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

        private Color OpponentColor(Color playerColor)
        {
            if (playerColor == Color.White) return Color.Black;
            else return Color.White;
        }

        private Piece King(Color color)
        {
            foreach (Piece p in PiecesInGame(color))
            {
                if (p is King) return p;
            }
            return null;
        }

        public bool IsCheck(Color color)
        {
            Piece king = King(color);
            if (king == null) throw new BoardException($"There is no {color} King in the board!");
            foreach (Piece p in PiecesInGame(OpponentColor(color)))
            {
                bool[,] mat = p.PossibleMovements();
                if (mat[king.Position.Row, king.Position.Column]) return true;
            }
            return false;
        }

        public bool IsMate(Color color)
        {
            if (!IsCheck(color)) return false;
            foreach (Piece p in PiecesInGame(color))
            {
                bool[,] mat = p.PossibleMovements();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Cols; j++)
                    {
                        if (mat[i, j])
                        {
                            Position start = p.Position;
                            Position end = new Position(i, j);
                            Piece takenPiece = DoMovement(start, end);
                            bool checkTest = IsCheck(color);
                            UndoMovement(start, end, takenPiece);
                            if (!checkTest) return false;
                        }
                    }
                }
            }
            return true;
        }

        public void AddNewPiece(char column, int row, Piece piece)
        {
            Board.AddPiece(piece, new ChessPosition(column, row).ToPosition());
            _pieces.Add(piece);
        }

        private void AddPieces()
        {
            //White Pieces
            AddNewPiece('a', 1, new Rook(Board, Color.White));
            AddNewPiece('b', 1, new Knight(Board, Color.White));
            AddNewPiece('c', 1, new Bishop(Board, Color.White));
            AddNewPiece('d', 1, new Queen(Board, Color.White));
            AddNewPiece('e', 1, new King(Board, Color.White, this));
            AddNewPiece('f', 1, new Bishop(Board, Color.White));
            AddNewPiece('g', 1, new Knight(Board, Color.White));
            AddNewPiece('h', 1, new Rook(Board, Color.White));
            AddNewPiece('a', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('b', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('c', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('d', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('e', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('f', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('g', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('h', 2, new Pawn(Board, Color.White, this));

            //Black Pieces
            AddNewPiece('a', 8, new Rook(Board, Color.Black));
            AddNewPiece('b', 8, new Knight(Board, Color.Black));
            AddNewPiece('c', 8, new Bishop(Board, Color.Black));
            AddNewPiece('d', 8, new Queen(Board, Color.Black));
            AddNewPiece('e', 8, new King(Board, Color.Black, this));
            AddNewPiece('f', 8, new Bishop(Board, Color.Black));
            AddNewPiece('g', 8, new Knight(Board, Color.Black));
            AddNewPiece('h', 8, new Rook(Board, Color.Black));
            AddNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
