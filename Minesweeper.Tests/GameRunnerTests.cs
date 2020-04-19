using System.Collections.Generic;
using Xunit;

namespace Minesweeper.Tests
{
    public class GameRunnerTests
    {
        private readonly GameRunner _runner;

        public GameRunnerTests()
        {
            BoardSettings settings = new BoardSettings
            {
                Width = 1,
                Height = 1,
                MineCount = 0,
                Letters = new List<string> { "A" }
            };

            Messages messages = new Messages
            {
                Win = "Win",
                Lose = "Lose",
                Instruction = "Instruction",
                InvalidInput = "Invalid",
                PlayAgain = "PlayAgain"
            };

            TestConsoleInputRetriever inputRetriever = new TestConsoleInputRetriever();

            _runner = new GameRunner(settings, messages, inputRetriever);
        }

        [Fact]
        public void GameRunner_MineNotHit_AllPointsRevealed_StateIsWin()
        {
            var board = BoardCreator.CreateNewBoard(1, 1, 0);
            var point = board[0, 0];

            var actual = _runner.UpdateGameStateOnUserInput(board, point);

            Assert.Equal(GameState.Won, actual);
        }

        [Fact]
        public void GameRunner_MineHit_GameStateIsLose()
        {
            var board = BoardCreator.CreateNewBoard(1, 1, 1);
            var point = board[0, 0];

            var actual = _runner.UpdateGameStateOnUserInput(board, point);

            Assert.Equal(GameState.Lost, actual);

        }

        [Fact]
        public void GameRunner_MineNotHitPointsStillHidden_StateIsInProgess()
        {
            var board = BoardCreator.CreateNewBoard(2, 2, 0);
            var point = board[0, 0];

            var actual = _runner.UpdateGameStateOnUserInput(board, point);

            Assert.Equal(GameState.InProgress, actual);
        }

        [Fact]
        public void Gamerunner_GameCompletes_StateIsComplete()
        {
            var actual = _runner.RunGame();
            Assert.Equal(GameState.Complete, actual);
        }
    }
}
