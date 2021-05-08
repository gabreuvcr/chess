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

        public Piece GetPiece(Position pos)
        {
            return Pieces[pos.Row, pos.Column];
        }

        public bool ThereIsPiece(Position pos)
        {
            ValidatePosition(pos);
            return GetPiece(pos) != null;
        }

        public void InsertPiece(Piece piece, Position pos)
        {   
            if (ThereIsPiece(pos))
            {
                throw new BoardException("There is already a piece in that position");
            }
            Pieces[pos.Row, pos.Column] = piece;
            piece.Position = pos;
        }

        public bool ValidPosition(Position pos)
        {
            if (pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (ValidPosition(pos) is not true)
            {
                throw new BoardException("Invalid position");
            }
        }
    }
}
