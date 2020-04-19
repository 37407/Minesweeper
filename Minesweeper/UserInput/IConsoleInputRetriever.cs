namespace Minesweeper
{
    public interface IConsoleInputRetriever
    {
        string UserInputReadLine();
        void UserInputReadKey();
    }
}