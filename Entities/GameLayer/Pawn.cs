using Chess.Entities.BoardLayer;

namespace Chess.Entities.GameLayer
{
    class Pawn : Piece
    {
        private Match Match;

        public Pawn(Board board, Color color, Match match)
            : base(board, color)
        {
            Match = match;
        }

        private bool ThereIsEnemy(Position pos)
        {
            Piece piece = Board.GetPiece(pos);
            return piece != null && piece.Color != Color;
        }

        private bool Free(Position pos)
        {
            return Board.GetPiece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {  
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.DefineValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos) && NumMoves == 0)
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                //En Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && ThereIsEnemy(left) && Board.GetPiece(left) == Match.VulnerableEnpassant)
                    {
                        matrix[left.Row - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && ThereIsEnemy(right) && Board.GetPiece(right) == Match.VulnerableEnpassant)
                    {
                        matrix[right.Row - 1, right.Column] = true;
                    }

                }
            }
            else 
            {
                pos.DefineValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos) && NumMoves == 0)
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                //En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && ThereIsEnemy(left) && Board.GetPiece(left) == Match.VulnerableEnpassant)
                    {
                        matrix[left.Row + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && ThereIsEnemy(right) && Board.GetPiece(right) == Match.VulnerableEnpassant)
                    {
                        matrix[right.Row + 1, right.Column] = true;
                    }

                }
            }

            return matrix;
        }

        public override string ToString()
        {
            return "P";
        }       
        
    }
}
