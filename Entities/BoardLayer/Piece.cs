using System;

namespace Chess.Entities.BoardLayer
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            NumMoves = 0;
        }

        public void IncrementNumMoves()
        {
            NumMoves++;
        }

        public void DecrementNumMoves()
        {
            NumMoves--;
        }

        public bool ThereArePossibleMovements()
        {
            bool [,] matrix = PossibleMovements();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (matrix[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMovements()[pos.Row, pos.Column];
        }

        public abstract bool [,] PossibleMovements();
    }
}
