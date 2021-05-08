using Chess.Entities.BoardLayer;

namespace Chess.Entities.GameLayer
{
    class Rook : Piece
    {
        public Rook(Board board, Color color)
            : base(board, color) {}

        public override string ToString()
        {
            return "R";
        }
    }
}
