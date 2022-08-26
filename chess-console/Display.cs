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
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.Piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
