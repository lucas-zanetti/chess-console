using board;
using chess;
using System.Collections.Generic;

namespace chess_console
{
    internal class Display
    {
        public static void DisplayGame(ChessGame game)
        {
            DisplayBoard(game.Board);
            Console.WriteLine();
            DisplayTakenPieces(game);
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.Turn);
            Console.WriteLine("Waiting play: " + game.CurrentPlayer);
            Console.WriteLine();
            if (game.Check) Console.WriteLine("CHECK!");
        }

        public static void DisplayTakenPieces(ChessGame game)
        {
            Console.WriteLine("Taken pieces:");
            Console.Write("White: ");
            DisplaySet(game.TakenPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            DisplaySet(game.TakenPieces(Color.Black));
            Console.ForegroundColor = defaultColor;
            Console.WriteLine();
        }

        public static void DisplaySet(HashSet<Piece> piecesSet)
        {
            Console.Write("[ ");
            foreach (Piece piece in piecesSet) Console.Write(piece + " ");
            Console.Write("]");
        }

        public static void DisplayBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Cols; j++)
                {
                    DisplayPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void DisplayBoard(Board board, bool[,] availableMoves)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            ConsoleColor customColor = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Cols; j++)
                {
                    if (availableMoves[i, j]) Console.BackgroundColor = customColor;
                    else Console.BackgroundColor = defaultColor;
                    DisplayPiece(board.Piece(i, j));
                    Console.BackgroundColor = defaultColor;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = defaultColor;
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char col = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(col, row);
        }

        public static void DisplayPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
