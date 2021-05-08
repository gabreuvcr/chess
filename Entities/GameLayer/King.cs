using Chess.Entities.BoardLayer;

namespace Chess.Entities.GameLayer
{
    class King : Piece
    {
        public King(Board board, Color color)
            : base(board, color) {}

        public override string ToString()
        {
            return "K";
        }
    }
}
