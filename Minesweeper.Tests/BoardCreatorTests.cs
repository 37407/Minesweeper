using Xunit;

namespace Minesweeper.Tests
{
    public class BoardCreatorTests
    {
        [Fact]
        public void CreateBoard_CreatesBoard_TypeIsCorrect()
        {
            var actual = BoardCreator.CreateNewBoard(8, 8, 8);
            Assert.IsType<GridPoint[,]>(actual);
        }

        [Fact]
        public void CreateBoard_CreatesBoard_DimensionsAreCorrect()
        {
            var actual = BoardCreator.CreateNewBoard(8, 8, 8);
            Assert.Equal(8, actual.GetLength(0));
            Assert.Equal(8, actual.GetLength(1));
        }

        [Fact]
        public void CreateBoard_CreatesBoard_NumberOfMinesIsCorrect()
        {
            var board = BoardCreator.CreateNewBoard(4, 4, 4);
            int mineCount = 0;

            foreach (var gridPoint in board)
            {
                if (gridPoint.IsMine) mineCount++;
            }

            Assert.Equal(4, mineCount);
        }
    }
}
