using System.Collections.Generic;

namespace Minesweeper
{
    public class BoardSettings
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int MineCount { get; set; }
        public List<string> Letters { get; set; }
    }
}
