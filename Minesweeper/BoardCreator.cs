namespace Minesweeper
{
    public static class BoardCreator
    {
        public static GridPoint[,] CreateNewBoard(int boardHeight, int boardWidth, int minesToPlace)
        {
            GridPoint[,] board = new GridPoint[boardHeight, boardWidth];
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
