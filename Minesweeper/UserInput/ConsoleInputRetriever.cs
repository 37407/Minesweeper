using System;

namespace Minesweeper
{
    public class ConsoleInputRetriever : IConsoleInputRetriever
    {
        public string UserInputReadLine() => Console.ReadLine().ToUpperInvariant();
        public void UserInputReadKey() => Console.ReadKey();
    }
}
