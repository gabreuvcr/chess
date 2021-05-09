using System.Collections.Generic;
using Chess.Entities.BoardLayer;

namespace Chess.Entities.GameLayer
{
    class Match
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished {get; private set;}
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; private set; }
        public Piece VulnerableEnpassant { get; private set; }

        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            VulnerableEnpassant = null;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            InsertPieces();
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> tmp = new HashSet<Piece>();
            foreach (Piece piece in Captured)
            {
                if (piece.Color == color)
                {
                    tmp.Add(piece);
                }
            }
            return tmp;
        }

        public HashSet<Piece> PiecesInMatch(Color color)
        {
            HashSet<Piece> tmp = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    tmp.Add(piece);
                }
            }
            tmp.ExceptWith(CapturedPieces(color));
            return tmp;
        }

        private Color Adversary(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        private Piece KingPiece(Color color)
        {
            foreach (Piece piece in PiecesInMatch(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }

            return null;
        }

        private bool IsCheck(Color color)
        {
            Piece king = KingPiece(color);
            if (king == null)
            {
                throw new BoardException($"There is no king of {color} color on the board");
            }

            foreach (Piece piece in PiecesInMatch(Adversary(color)))
            {
                bool[,] matrix = piece.PossibleMovements();
                if (matrix[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCheckMate(Color color)
        {
            if (!IsCheck(color))
            {
                return false;
            }

            foreach (Piece piece in PiecesInMatch(color))
            {
                bool[,] matrix = piece.PossibleMovements();
                for (int i  = 0; i < Board.Rows; i++) {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (matrix[i, j])
                        {
                            Position origin = piece.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = MakeMovement(origin, destiny);
                            bool testIsCheck = IsCheck(color);
                            UndoMovement(origin, destiny, capturedPiece);
                            if (!testIsCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            
            return true;
        }

        public void InsertNewPiece(char column, int row, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        private void InsertPieces()
        {   
            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            InsertNewPiece('b', 1, new Knight(Board, Color.White));
            InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White, this));
            InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            InsertNewPiece('g', 1, new Knight(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));
            InsertNewPiece('a', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White, this));

            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('b', 8, new Knight(Board, Color.Black));
            InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black, this));
            InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('g', 8, new Knight(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            InsertNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }

        public Piece MakeMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementNumMoves();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(piece, destiny);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }

            //Castling
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                 Position originRook = new Position(origin.Row, origin.Column + 3);
                 Position destinyRook = new Position(origin.Row, origin.Column + 1);
                 Piece rook = Board.RemovePiece(originRook);
                 rook.IncrementNumMoves();
                 Board.InsertPiece(rook, destinyRook);
            }
            //Castling
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                 Position originRook = new Position(origin.Row, origin.Column - 4);
                 Position destinyRook = new Position(origin.Row, origin.Column - 1);
                 Piece rook = Board.RemovePiece(originRook);
                 rook.IncrementNumMoves();
                 Board.InsertPiece(rook, destinyRook);
            }

            //En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position positionPawn;
                    if (piece.Color == Color.White)
                    {
                        positionPawn = new Position(destiny.Row + 1, destiny.Column);
                    }
                    else
                    {
                        positionPawn = new Position(destiny.Row - 1, destiny.Column);
                    }
                    capturedPiece = Board.RemovePiece(positionPawn);
                    Captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destiny);
            piece.DecrementNumMoves();
            
            if (capturedPiece != null)
            {
                Board.InsertPiece(capturedPiece, destiny);
                Captured.Remove(capturedPiece);
            }
            Board.InsertPiece(piece, origin);

            //Castling
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                 Position originRook = new Position(origin.Row, origin.Column + 3);
                 Position destinyRook = new Position(origin.Row, origin.Column + 1);
                 Piece rook = Board.RemovePiece(destinyRook);
                 rook.DecrementNumMoves();
                 Board.InsertPiece(rook, originRook);
            }
            //Castling
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                 Position originRook = new Position(origin.Row, origin.Column - 4);
                 Position destinyRook = new Position(origin.Row, origin.Column - 1);
                 Piece rook = Board.RemovePiece(destinyRook);
                 rook.DecrementNumMoves();
                 Board.InsertPiece(rook, originRook);
            }

            //En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == VulnerableEnpassant)
                {
                    Piece pawn  = Board.RemovePiece(destiny);
                    Position posPawn;
                    if (piece.Color == Color.White)
                    {
                        posPawn = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posPawn = new Position(4, destiny.Column);
                    }
                    Board.InsertPiece(pawn, posPawn);
                }
            }
        }

        public void Move(Position origin, Position destiny)
        {
            Piece capturedPiece = MakeMovement(origin, destiny);

            if (IsCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, capturedPiece);
                throw new BoardException("You cannot put yourself in check");
            }

            Piece piece = Board.GetPiece(destiny);

            //Promotion
            if (piece is Pawn)
            {
                if ((piece.Color == Color.White && destiny.Row == 0) ||
                    (piece.Color == Color.Black && destiny.Row == 7))
                {
                    piece = Board.RemovePiece(destiny);
                    Pieces.Remove(piece);
                    Piece queen = new Queen(Board, piece.Color);
                    Board.InsertPiece(queen, destiny);
                    Pieces.Add(queen);
                }
            }


            Check = IsCheck(Adversary(CurrentPlayer));

            if (IsCheckMate(Adversary(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
            }
            
            //En Passant
            if (piece is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            {
                VulnerableEnpassant = piece;
            }
            else
            {
                VulnerableEnpassant = null;
            }
        }

        public void ValidOriginPosition(Position pos)
        {
            if (Board.GetPiece(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen origin position");
            }
            if (CurrentPlayer != Board.GetPiece(pos).Color)
            {
                throw new BoardException("The origin piece chosen is not yours");
            }
            if (!Board.GetPiece(pos).ThereArePossibleMovements())
            {
                throw new BoardException("There are no movements for the chosen origin piece");
            }
        }

        public void ValidDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.GetPiece(origin).PossibleMovement(destiny))
            {
                throw new BoardException("Invalid destiny position");
            }
        }
    }
}
