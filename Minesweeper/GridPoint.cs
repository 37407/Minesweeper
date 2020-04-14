using System.Collections.Generic;

namespace Minesweeper
{
    public class GridPoint
    {
        public bool IsMine { get; set; }
        public bool IsHidden { get; set; }
        public IList<int[]> NeighbourCoordinates { get; set; }
        public int AdjacentMineCount { get; set; }
        public string DisplayCharacter { get; set; }
    }
}