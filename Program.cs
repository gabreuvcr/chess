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
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(match.Board);
                        
                        Console.WriteLine($"\nTurn: {match.Turn}");
                        Console.WriteLine($"Awaiting move: {match.CurrentPlayer}");

                        Console.Write("\nOrigin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidOriginPosition(origin);

                        bool[,] possibleMovements = match.Board.GetPiece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possibleMovements);

                        Console.Write("\nDestiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        match.ValidDestinyPosition(origin, destiny);

                        match.Move(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine($"Board error: {e.Message}");
                        Console.ReadKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Unexpected error: {e.Message}");
                        Console.ReadKey();
                    }
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
