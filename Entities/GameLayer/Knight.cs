using Chess.Entities.BoardLayer;

namespace Chess.Entities.GameLayer
{
    class Knight : Piece
    {
        public Knight(Board board, Color color)
            : base(board, color) {}

        private bool CanMove(Position pos)
        {
            Piece piece = Board.GetPiece(pos);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {  
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0, 0);

            pos.DefineValues(Position.Row - 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            
            pos.DefineValues(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row +1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            return matrix;
        }

        public override string ToString()
        {
            return "N";
        }       
        
    }
}
