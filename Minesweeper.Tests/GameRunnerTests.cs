using Microsoft.Extensions.Configuration;
using System.Linq;
using Xunit;

namespace Minesweeper.Tests
{
    public class GameRunnerTests
    {
        private readonly GameRunner _runner;

        public GameRunnerTests()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("TestConfig.json")
                .Build();

            var settings = configuration.GetSection("BoardSettings");
            var messages = configuration.GetSection("Messages");

            BoardSettings boardSettings = new BoardSettings
            {
                Height = int.Parse(settings["Height"]),
                Width = int.Parse(settings["Width"]),
                MineCount = int.Parse(settings["MineCount"]),
                Letters = settings["Letters"].Split(",").ToList()
            };

            Messages gameMessages = new Messages
            {
                Instruction = messages["Instruction"],
                InvalidInput = messages["InvalidInput"],
                Win = messages["Win"],
                Lose = messages["Lose"],
                PlayAgain = messages["PlayAgain"]
            };

            TestConsoleInputRetriever inputRetriever = new TestConsoleInputRetriever();

            _runner = new GameRunner(boardSettings, gameMessages, inputRetriever);
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
