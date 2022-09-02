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
                ChessGame game = new ChessGame();

                while (!game.GameOver)
                {
                    Console.Clear();
                    Display.DisplayBoard(game.Board);

                    Console.WriteLine();
                    Console.Write("Type start position: ");
                    Position start = Display.ReadChessPosition().ToPosition();

                    bool[,] availableMoves = game.Board.Piece(start).PossibleMovements();

                    Console.Clear();
                    Display.DisplayBoard(game.Board, availableMoves);

                    Console.WriteLine();
                    Console.Write("Type end position: ");
                    Position end = Display.ReadChessPosition().ToPosition();

                    game.DoMovement(start, end);
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}