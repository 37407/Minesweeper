using System;

namespace Minesweeper
{
    public static class BoardCreatorExtensions
    {
        public static GridPoint[,] AddGridPointsToBoard(this GridPoint[,] board)
        {
            var height = board.GetLength(0);
            var width = board.GetLength(1);

            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    board[x, y] = new GridPoint
                    {
                        NeighbourCoordinates = GridPointHelper.CalculateNeighbourCoordinates(x, y, height, width),
                        IsHidden = true,
                        IsMine = false,
                        AdjacentMineCount = 0
                    };
                };
            };
            return board;
        }

        public static GridPoint[,] AddMinesToBoard(this GridPoint[,] board, int minesToPlace)
        {
            while (minesToPlace > 0)
            {
                var random = new Random();
                int mineXcoord = random.Next(board.GetLength(0));
                int mineYcoord = random.Next(board.GetLength(1));

                var point = board[mineXcoord, mineYcoord];

                if (!point.IsMine)
                {
                    point.IsMine = true;
                    minesToPlace--;
                }
            }
            return board;
        }

        public static GridPoint[,] CalculateAdjacentMineCount(this GridPoint[,] board)
        {
            foreach (var gridPoint in board)
            {
                foreach (var neighbour in gridPoint.NeighbourCoordinates)
                {
                    var point = board[neighbour[0], neighbour[1]];
                    if (point.IsMine) gridPoint.AdjacentMineCount++;
                }
            }
            return board;
        }
    }
}
