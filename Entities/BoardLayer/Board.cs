using System;

namespace Chess.Entities.BoardLayer
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces { get; set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];
        }
        
        public Piece GetPiece(int row, int column)
        {
            return Pieces[row, column];
        }

        public void InsertPiece(Piece piece, Position pos)
        {
            Pieces[pos.Row, pos.Column] = piece;
            piece.Position = pos;
        }
    }
}
