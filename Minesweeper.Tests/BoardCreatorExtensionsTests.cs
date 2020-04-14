using Xunit;

namespace Minesweeper.Tests
{
    public class BoardCreatorExtensionsTests
    {
        [Fact]
        public void AddPointsToBoard_PopulatesBoard_GridPointsAddedWithCorrectPropertyValues()
        {
            var board = new GridPoint[2, 2];
            board.AddGridPointsToBoard();

            foreach (var point in board)
            {
                Assert.IsType<GridPoint>(point);
                Assert.Equal(0, point.AdjacentMineCount);
                Assert.Null(point.DisplayCharacter);
                Assert.False(point.IsMine);
                Assert.True(point.IsHidden);
                Assert.Equal(3, point.NeighbourCoordinates.Count);
            }
            Assert.Equal(4, board.Length);
        }

        [Fact]
        public void AddMinesToBoard_AddsCorrectNumberOfMines()
        {
            var board = new GridPoint[3, 3];
            board.AddGridPointsToBoard();
            board.AddMinesToBoard(3);

            int placedMines = 0;
            foreach (var point in board)
            {
                if (point.IsMine) placedMines++;
            }

            Assert.Equal(3, placedMines);
        }

        [Fact]
        public void CalculateAdjacentMineCount_ReturnsCorrectValue()
        {
            var board = new GridPoint[3, 3];
            board.AddGridPointsToBoard();
            var firstMineLocation = board[0, 0];
            var secondMineLocation = board[1, 1];
            firstMineLocation.IsMine = true;
            secondMineLocation.IsMine = true;

            board.CalculateAdjacentMineCount();

            Assert.Equal(2, board[0, 1].AdjacentMineCount);
            Assert.Equal(1, board[0, 2].AdjacentMineCount);
            Assert.Equal(2, board[1, 0].AdjacentMineCount);
            Assert.Equal(1, board[1, 2].AdjacentMineCount);
            Assert.Equal(1, board[2, 0].AdjacentMineCount);
            Assert.Equal(1, board[2, 1].AdjacentMineCount);
            Assert.Equal(1, board[2, 2].AdjacentMineCount);
        }
    }
}
