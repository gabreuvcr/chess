using System;
using Chess.Entities.BoardLayer;

namespace Chess.Entities
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write($"- ");
                    }
                    else
                    {
                        Console.Write($"{board.GetPiece(i, j)} ");
                    }
                }
                Console.WriteLine();
            }
        }    
    }
}
