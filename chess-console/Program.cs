using board;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position(3, 4);

            Console.WriteLine(position);
        }
    }
}