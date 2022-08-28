using board;
using chess;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessPosition pos = new ChessPosition('c', 7);
            Console.WriteLine(pos);
            Console.WriteLine(pos.toPosition());
        }
    }
}