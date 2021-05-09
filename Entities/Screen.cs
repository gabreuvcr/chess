using System;
using System.Collections.Generic;
using Chess.Entities.BoardLayer;
using Chess.Entities.GameLayer;

namespace Chess.Entities
{
    class Screen
    {
        public static void PrintMatch(Match match)
        {
            Screen.PrintBoard(match.Board);
            PrintCapturedPieces(match);
            Console.WriteLine($"\nTurn: {match.Turn}");
            if (!match.Finished)
            {
                Console.WriteLine($"Awaiting move: {match.CurrentPlayer}");
                if (match.Check)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else 
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine($"Winner: {match.CurrentPlayer}");
            }
        }

        public static void PrintCapturedPieces(Match match)
        {
            Console.WriteLine("\nCaptured pieces: ");
            Console.Write("White: ");
            PrintPieceSet(match.CapturedPieces(Color.White));
            Console.Write("Black: ");
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            PrintPieceSet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = tmp;
        }

        public static void PrintPieceSet(HashSet<Piece> pieceSet)
        {
            Console.Write("[");
            foreach (Piece piece in pieceSet)
            {
                Console.Write($"{piece} ");
            }
            Console.Write("]\n");
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] possibleMovements)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor possibleBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMovements[i, j])
                    {
                        Console.BackgroundColor = possibleBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.BackgroundColor = originalBackground;
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition ReadChessPosition()
        {
            string positionString = Console.ReadLine().Trim().ToLower();
            char column = positionString[0];
            int row = Int32.Parse(positionString[1].ToString());
            
            return new ChessPosition(column, row);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else 
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
}
