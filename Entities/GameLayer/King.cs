using Chess.Entities.BoardLayer;

namespace Chess.Entities.GameLayer
{
    class King : Piece
    {
        private Match Match;

        public King(Board board, Color color, Match match)
            : base(board, color) 
        {
            Match = match;
        }

        private bool CanMove(Position pos)
        {
            Piece piece = Board.GetPiece(pos);
            return piece == null || piece.Color != Color;
        }

        private bool testRookForCastling(Position pos)
        {
            Piece piece = Board.GetPiece(pos);
            return piece != null && piece is Rook && piece.Color == Color && piece.NumMoves == 0;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            
            Position pos = new Position(0, 0);

            pos.DefineValues(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row , Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Castling
            if (NumMoves == 0 && !Match.Check)
            {
                Position posRook = new Position(Position.Row, Position.Column + 3);
                if (testRookForCastling(posRook))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null)
                    {
                        matrix[Position.Row, Position.Column + 2] = true;
                    }
                }
                posRook = new Position(Position.Row, Position.Column - 4);
                if (testRookForCastling(posRook))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null && Board.GetPiece(p3) == null)
                    {
                        matrix[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return matrix;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
