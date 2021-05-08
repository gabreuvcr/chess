using System;
using Chess.Entities;
using Chess.Entities.BoardLayer;
using Chess.Entities.GameLayer;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.InsertPiece(new Rook(board, Color.Black), new Position(0, 0));
            board.InsertPiece(new Rook(board, Color.White), new Position(1, 3));
            board.InsertPiece(new King(board, Color.Black), new Position(2, 4));
            
            Screen.PrintBoard(board);
        }
    }
}
