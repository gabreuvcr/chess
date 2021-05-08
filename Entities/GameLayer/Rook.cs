using Chess.Entities.BoardLayer;

namespace Chess.Entities.GameLayer
{
    class Rook : Piece
    {
        public Rook(Board board, Color color)
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
            pos.DefineValues(Position.Row - 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Row--;
            }

            pos.DefineValues(Position.Row + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Row++;
            }

            pos.DefineValues(Position.Row, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Column++;
            }

            pos.DefineValues(Position.Row, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Column--;
            }

            return matrix;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
