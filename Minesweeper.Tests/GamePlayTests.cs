using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Minesweeper.Tests
{
    public class GamePlayTests
    {
        [Fact]
        public void CheckForWin_AllNonMinePointsRevealed_ReturnsTrue()
        {
            var board = BoardCreator.CreateNewBoard(3, 3, 3);
            foreach (var point in board)
            {
                if (!point.IsMine) point.IsHidden = false;
            }

            var actual = GamePlay.CheckForWin(board, 3);

            Assert.True(actual);
        }

        [Fact]
        public void CheckForWin_NonMinePointsHidden_ReturnsFalse()
        {
            var board = BoardCreator.CreateNewBoard(3, 3, 3);

            var actual = GamePlay.CheckForWin(board, 3);
            
            Assert.False(actual);
        }
    }
}
