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

                    gameOver = selectedPoint.IsMine;

                    if (gameOver)
                    {
                        GamePlay.MineHit(board, letters);
                        Console.ReadKey();
                    }
                    else
                    {
                        GamePlay.MineNotHit(selectedPoint);
                        GameState state = GamePlay.CheckForWin(board, mineCount);
                        if (state == GameState.Won)
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