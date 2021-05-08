using System;

namespace Chess.Entities.BoardLayer
{
    public class BoardException : ApplicationException
    {
        public BoardException(string message)
            : base(message) {}
    }
}
