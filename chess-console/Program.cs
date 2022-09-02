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
                ChessGame game = new();

                while (!game.GameOver)
                {
                    try
                    {
                        Console.Clear();
                        Display.DisplayBoard(game.Board);

                        Console.WriteLine();
                        Console.WriteLine("Turn: " + game.Turn);
                        Console.WriteLine("Waiting play: " + game.CurrentPlayer);

                        Console.Write("Type start position: ");
                        Position start = Display.ReadChessPosition().ToPosition();
                        game.ValidateStartPosition(start);

                        bool[,] availableMoves = game.Board.Piece(start).PossibleMovements();

                        Console.Clear();
                        Display.DisplayBoard(game.Board, availableMoves);

                        Console.WriteLine();
                        Console.Write("Type end position: ");
                        Position end = Display.ReadChessPosition().ToPosition();
                        game.ValidateEndPosition(start, end);

                        game.ExecutePlay(start, end);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}