﻿using DeepEqual.Syntax;
using System.Collections.Generic;
using Xunit;

namespace Minesweeper.Tests
{
    public class GridPointHelperTests
    {
        [Fact]
        public void CalculateNeighbourCoordinates_ForCorner_ReturnsCorrectCoordinates()
        {
            var expected = new List<int[]>
            {
                new int[2] { 1, 1 },
                new int[2] { 1, 0 },
                new int[2] { 0, 1 }
            };

            var actual = GridPointHelper.CalculateNeighbourCoordinates(0, 0, 5, 5);

            Assert.Equal(3, actual.Count);
            Assert.True(actual.IsDeepEqual(expected));
        }

        [Fact]
        public void CalculateNeighbourCoordinates_ForHorizontalEdge_ReturnsCorrectCoordinates()
        {
            var expected = new List<int[]>
            {
                new int[2] { 3, 1 },
                new int[2] { 3, 0 },
                new int[2] { 2, 1 },
                new int[2] { 1, 1 },
                new int[2] { 1, 0 }
            };

            var actual = GridPointHelper.CalculateNeighbourCoordinates(2, 0, 5, 5);

            Assert.Equal(5, actual.Count);
            Assert.True(actual.IsDeepEqual(expected));
        }

        [Fact]
        public void CalculateNeighbourCoordinates_ForVerticalEdge_ReturnsCorrectCoordinates()
        {
            var expected = new List<int[]>
            {
                new int[2] { 1, 3 },
                new int[2] { 1, 2 },
                new int[2] { 1, 1 },
                new int[2] { 0, 3 },
                new int[2] { 0, 1 }
            };

            var actual = GridPointHelper.CalculateNeighbourCoordinates(0, 2, 5, 5);

            Assert.Equal(5, actual.Count);
            Assert.True(actual.IsDeepEqual(expected));
        }

        [Fact]
        public void CalculateNeighbourCoordinates_ForCentralPoint_ReturnsCorrectCoordinates()
        {
            var expected = new List<int[]>
            {
                new int[2] { 3, 3 },
                new int[2] { 3, 2 },
                new int[2] { 3, 1 },
                new int[2] { 2, 3 },
                new int[2] { 2, 1 },
                new int[2] { 1, 3 },
                new int[2] { 1, 2 },
                new int[2] { 1, 1 }
            };

            var actual = GridPointHelper.CalculateNeighbourCoordinates(2, 2, 5, 5);

            Assert.Equal(8, actual.Count);
            Assert.True(actual.IsDeepEqual(expected));
        }

        [Theory]
        [InlineData(true, false, 0, "#")]
        [InlineData(true, false, 1, "#")]
        [InlineData(true, true, 0, "#")]
        [InlineData(true, true, 1, "#")]
        [InlineData(false, true, 0, "m")]
        [InlineData(false, true, 1, "m")]
        [InlineData(false, false, 0, "0")]
        [InlineData(false, false, 1, "1")]
        public void GridPoint_SetDisplayCharacter_SetsCorrectCharacter(bool hidden, bool isMine, int adjacentMines, string display)
        {
            var point = new GridPoint
            {
                IsHidden = hidden,
                IsMine = isMine,
                AdjacentMineCount = adjacentMines,
            };

            GridPointHelper.SetDisplayCharacter(point);

            Assert.Equal(display, point.DisplayCharacter);
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
        public void MapUserInputToCoordinates_Maps_CorrectValues(string input, int horizontal, int vertical)
        {
            List<string> letters = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };

            var actual = GridPointHelper.MapUserInputToCoordinates(letters, input);

            Assert.IsType<int[]>(actual);
            Assert.Equal(horizontal, actual[0]);
            Assert.Equal(vertical, actual[1]);
        }
    }
}
