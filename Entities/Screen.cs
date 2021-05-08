using System;
using Chess.Entities.BoardLayer;
using Chess.Entities.GameLayer;

namespace Chess.Entities
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write($"- ");
                    }
                    else
                    {
                        PrintPiece(board.GetPiece(i, j));
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition ReadChessPosition()
        {
            string positionString = Console.ReadLine();
            char column = positionString[0];
            int row = Int32.Parse(positionString[1].ToString());
            
            return new ChessPosition(column, row);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece.Color == Color.White)
            {
                Console.Write($"{piece} ");   
            }
            else 
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{piece} ");   
                Console.ForegroundColor = tmp;
            }
        } 
    }
}
