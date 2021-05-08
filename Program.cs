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
            try
            {
                Match match = new Match();

                while (match.Finished is not true)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.Write("\nOrigem: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possibleMovements = match.Board.GetPiece(origin).PossibleMovements();

                    Console.Clear();
                    Screen.PrintBoard(match.Board, possibleMovements);

                    Console.Write("\nDestino: ");
                    Position destiny = Screen.ReadChessPosition().ToPosition();

                    match.Move(origin, destiny);
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine($"Board error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
            
        }
    }
}
