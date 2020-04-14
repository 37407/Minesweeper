using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public static class GamePlay
    {
        public static void DisplayBoard(GridPoint[,] board, List<string> letters)
        {
            Console.Write("  ");
            foreach (var letter in letters)
            {
                Console.Write(" " + letter);
            }
            Console.WriteLine();

            for (int x = 0; x < board.GetLength(0); x++)
            {
                Console.Write($"{x + 1}  ");
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    Console.Write(board[x, y].DisplayCharacter + " ");
                }
                Console.WriteLine();
            }
        }

        public static bool UserInputValid(string input, List<string> letters, int boardHeight)
        {
            return input.Length == 2
                && letters.Contains(input[0].ToString())
                && int.TryParse(input[1].ToString(), out int column)
                && column < boardHeight;
        }

        public static int[] MapUserInputToCoordinates(List<string> letters, string input)
        {
            int column = letters.IndexOf(input.Substring(0, 1));
            int row = int.Parse(input.Substring(1, 1)) - 1;
            return new int[] { row, column };
        }

        public static bool IsWin(GridPoint[,] board, int mineCount)
        {
            int unhiddenPoints = 0;
            foreach (var point in board)
            {
                if (!point.IsHidden && !point.IsMine) unhiddenPoints++;
            }
            return unhiddenPoints == board.Length - mineCount;
        }
    }
}
