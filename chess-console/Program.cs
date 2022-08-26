using board;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Display.displayBoard(board);

            Console.ReadLine();
        }
    }
}