using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    partial class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var settings = configuration.GetSection(nameof(BoardSettings));
            var messages = configuration.GetSection(nameof(Messages));
            var letters = new List<string>();

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

            GameRunner gameRunner = new GameRunner(boardSettings, gameMessages);
            gameRunner.RunGame();
        }
    }
}