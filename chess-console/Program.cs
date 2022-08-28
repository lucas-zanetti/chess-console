using board;
using chess;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.AddPiece(new Pawn(board, Color.Black), new Position(0, 0));
                board.AddPiece(new Knight(board, Color.Black), new Position(1, 3));
                board.AddPiece(new King(board, Color.White), new Position(2, 4));
                board.AddPiece(new Queen(board, Color.White), new Position(0, 2));
                board.AddPiece(new Bishop(board, Color.White), new Position(1, 7));
                board.AddPiece(new Pawn(board, Color.Black), new Position(2, 6));

                Display.DisplayBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}