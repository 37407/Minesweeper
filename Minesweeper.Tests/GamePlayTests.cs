﻿using System.Collections.Generic;
using Xunit;

namespace Minesweeper.Tests
{
    public class GamePlayTests
    {
        [Theory]
        [InlineData("A1", true)]
        [InlineData("A4", true)]
        [InlineData("A", false)]
        [InlineData("A1A", false)]
        [InlineData("1A", false)]
        [InlineData("E1", false)]
        [InlineData("AZ", false)]
        [InlineData("A5", false)]
        [InlineData("A0", false)]
        [InlineData("", false)]
        public void UserInputValid_ValidatesInputStringsCorrectly(string input, bool expected)
        {
            List<string> letters = new List<string> { "A", "B", "C", "D" };
            var actual = GamePlay.UserInputValid(input, letters, 4);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("A1", 0, 0)]
        [InlineData("A2", 1, 0)]
        [InlineData("A3", 2, 0)]
        [InlineData("B1", 0, 1)]
        [InlineData("B2", 1, 1)]
        [InlineData("B3", 2, 1)]
        [InlineData("C3", 2, 2)]
        [InlineData("D4", 3, 3)]
        [InlineData("E5", 4, 4)]
        [InlineData("F6", 5, 5)]
        [InlineData("G7", 6, 6)]
        [InlineData("H8", 7, 7)]
        public void MapUserInputToCoordinates_Maps_CorrectValues(string input, int column, int row)
        {
            List<string> letters = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };

            var actual = GamePlay.MapUserInputToCoordinates(letters, input);

            Assert.IsType<int[]>(actual);
            Assert.Equal(column, actual[0]);
            Assert.Equal(row, actual[1]);
        }

        [Fact]
        public void MineHit_UnhidesAllPoints_ReturnsGameStateLost()
        {
            var board = BoardCreator.CreateNewBoard(3, 3, 3);
            List<string> letters = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };
            Messages messages = new Messages { Lose = "Lose", PlayAgain = "PlayAgain" };

            var actual = GamePlay.MineHit(board, letters, messages);

            foreach (var point in board)
            {
                Assert.False(point.IsHidden);
                Assert.NotEqual("*", point.DisplayCharacter);
            }
            Assert.Equal(GameState.Lost, actual);
        }

        [Fact]
        public void MineNotHit_UnhidesPoint_ReturnsGameStateInProgress()
        {
            var point = new GridPoint { IsHidden = true, IsMine = false, DisplayCharacter = "*", AdjacentMineCount = 1 };
            var actual = GamePlay.MineNotHit(point);

            Assert.False(point.IsHidden);
            Assert.Equal(point.DisplayCharacter, point.AdjacentMineCount.ToString());
            Assert.Equal(GameState.InProgress, actual);
        }

        [Fact]
        public void CheckForWin_AllNonMinePointsRevealed_ReturnsGameStateWon()
        {
            var board = BoardCreator.CreateNewBoard(3, 3, 3);
            foreach (var point in board)
            {
                if (!point.IsMine) point.IsHidden = false;
            }

            var actual = GamePlay.CheckForWin(board, 3);

            Assert.Equal(GameState.Won, actual);
        }

        [Fact]
        public void CheckForWin_NonMinePointsHidden_ReturnsGameStateInProgress()
        {
            var board = BoardCreator.CreateNewBoard(3, 3, 3);

            var actual = GamePlay.CheckForWin(board, 3);
            
            Assert.Equal(GameState.InProgress, actual);
        }
    }
}
