using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public static class BoardHelper
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

        public static bool CheckForWin(GridPoint[,] board, int mineCount)
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
