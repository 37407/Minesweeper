using System.Collections.Generic;

namespace Minesweeper
{
    public static class GridPointHelper
    {
        public static IList<int[]> CalculateNeighbourCoordinates(int startXcoord, int startYcoord, int boardWidth, int boardHeight)
        {
            List<int[]> neighbourCoordinates = new List<int[]>();
            for (int x = startXcoord + 1; x >= startXcoord - 1; x--)
            {
                for (int y = startYcoord + 1; y >= startYcoord - 1; y--)
                {
                    if ((x == startXcoord && y == startYcoord) || x < 0 || y < 0 || x >= boardWidth || y >= boardHeight) continue;
                    neighbourCoordinates.Add(new int[] { x, y });
                }
            }
            return neighbourCoordinates;
        }

        public static GridPoint SetDisplayCharacter(GridPoint point)
        {
            point.DisplayCharacter = point.IsHidden ? "#" : point.IsMine ? "m" : point.AdjacentMineCount.ToString();
            return point;
        }
    }
}
