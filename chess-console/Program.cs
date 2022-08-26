using board;
using chess;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.addPiece(new Pawn(board, Color.Black), new Position(0, 0));
            board.addPiece(new Knight(board, Color.Black), new Position(1, 3));
            board.addPiece(new King(board, Color.White), new Position(2, 4));

            Display.displayBoard(board);

            Console.ReadLine();
        }
    }
}