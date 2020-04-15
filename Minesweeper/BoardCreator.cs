namespace Minesweeper
{
    public static class BoardCreator
    {
        public static GridPoint[,] CreateNewBoard(int width, int height, int minesToPlace)
        {
            GridPoint[,] board = new GridPoint[width, height];
            board.AddGridPointsToBoard();
            board.AddMinesToBoard(minesToPlace);
            board.CalculateAdjacentMineCount();

            foreach (var point in board)
            {
                GridPointHelper.SetDisplayCharacter(point);
            }

            return board;
        }
    }
}
