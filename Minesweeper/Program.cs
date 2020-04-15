using System;
using System.Collections.Generic;

namespace Minesweeper
{
    partial class Program
    {
        static void Main(string[] args)
        {
            bool newGame = true;
            int height = 8;
            int width = 8;
            int mineCount = 8;
            var letters = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };

            while (newGame)
            {
                var board = BoardCreator.CreateNewBoard(height, width, mineCount);
                bool gameOver = false;

                while (!gameOver)
                {
                    GamePlay.DisplayBoard(board, letters);
                    Console.WriteLine("Please enter a column and row (e.g. A8):");
                    
                    string input = Console.ReadLine().ToUpperInvariant();
                    bool validInput = GamePlay.UserInputValid(input, letters, height);

                    while (!validInput)
                    {
                        Console.WriteLine("Input is invalid - please enter a column and row in the format A8.");
                        input = Console.ReadLine().ToUpperInvariant();
                        validInput = GamePlay.UserInputValid(input, letters, height);
                    }

                    var inputCoords = GamePlay.MapUserInputToCoordinates(letters, input);

                    var selectedPoint = board[inputCoords[0], inputCoords[1]];

                    if (selectedPoint.IsMine)
                    {
                        foreach (var point in board)
                        {
                            point.IsHidden = false;
                            GridPointHelper.SetDisplayCharacter(point);
                        }

                        GamePlay.DisplayBoard(board, letters);
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
                        bool win = GamePlay.IsWin(board, mineCount);
                        if (win)
                        {
                            GamePlay.DisplayBoard(board, letters);
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