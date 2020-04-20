using Microsoft.Extensions.Configuration;

namespace Minesweeper
{
    partial class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("GameConfig.json")
                .Build();

            var settings = configuration.GetSection("BoardSettings").Get<BoardSettings>();
            var messages = configuration.GetSection("Messages").Get<Messages>();

            ConsoleInputRetriever inputRetriever = new ConsoleInputRetriever();

            GameRunner gameRunner = new GameRunner(settings, messages, inputRetriever);
            gameRunner.RunGame();
        }
    }
}