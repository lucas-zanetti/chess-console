﻿namespace board
{
    internal abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveQuantity { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            Board = board;
            MoveQuantity = 0;
        }

        public void IncreaseMoveQuantity()
        {
            MoveQuantity++;
        }

        public abstract bool[,] PossibleMovements();
    }
}
