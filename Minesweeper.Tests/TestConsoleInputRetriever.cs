namespace Minesweeper.Tests
{
    public class TestConsoleInputRetriever : IConsoleInputRetriever
    {
        public string UserInputReadLine()
        {
            return "A1";
        }

        public void UserInputReadKey()
        {

        }
    }
}
