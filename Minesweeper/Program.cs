using System;
using System.Collections.Generic;

namespace Minesweeper
{
    partial class Program
    {
        static void Main(string[] args)
        {
            bool newGame = true;
            int boardWidth = 8;
            int boardHeight = 8;
            int mineCount = 8;
            var letters = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };

            while (newGame)
            {
                var board = BoardCreator.CreateNewBoard(boardWidth, boardHeight, mineCount);
                bool gameOver = false;

                while (!gameOver)
                {
                    BoardHelper.DisplayBoard(board, letters);
                    Console.WriteLine("Please enter a column and row (e.g. A8):");
                    var input = Console.ReadLine().ToUpperInvariant();

                    var inputCoords = GridPointHelper.MapUserInputToCoordinates(letters, input);

                    var selectedPoint = board[inputCoords[0], inputCoords[1]];

                    if (selectedPoint.IsMine)
                    {
                        foreach (var point in board)
                        {
                            point.IsHidden = false;
                            GridPointHelper.SetDisplayCharacter(point);
                        }

                        BoardHelper.DisplayBoard(board, letters);
                        Console.WriteLine("**Sorry you hit a mine**");
                        Console.WriteLine("--Press any key to play again--");
                        Console.WriteLine();
                        gameOver = true;
                        Console.ReadKey();
                    }
                    else
                    {
                        selectedPoint.IsHidden = false;
                        GridPointHelper.SetDisplayCharacter(selectedPoint);
                        bool win = BoardHelper.CheckForWin(board, mineCount);
                        if (win)
                        {
                            BoardHelper.DisplayBoard(board, letters);
                            Console.WriteLine("**Congratulations - you won!**");
                            gameOver = true;
                            newGame = false;
                        }
                    }
                }
            }
        }
    }
}