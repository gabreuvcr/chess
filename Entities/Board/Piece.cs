using System;

namespace Chess.Entities.Board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Position pos, Board board, Color color)
        {
            Position = pos;
            Board = board;
            Color = color;
            NumMoves = 0;
        }

    }
}
