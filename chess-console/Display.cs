using board;

namespace chess_console
{
    internal class Display
    {
        public static void displayBoard(Board board)
        {
            for(int i=0; i<board.Rows; i++)
            {
                for(int j=0; j<board.Cols; j++)
                {
                    if (board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
