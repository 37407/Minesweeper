using System;

namespace Minesweeper
{
    public class GameRunner
    {
        private IConsoleInputRetriever _inputRetriever;
        private GameState _state;
        private readonly BoardSettings _boardSettings;
        private readonly Messages _messages;

        public GameRunner(BoardSettings boardSettings, Messages messages, IConsoleInputRetriever inputRetriever)
        {
            _boardSettings = boardSettings;
            _messages = messages;
            _inputRetriever = inputRetriever;
            _state = GameState.InProgress;
        }

        public void RunGame()
        {
            while (_state != GameState.Won)
            {
                var board = BoardCreator.CreateNewBoard(
                    _boardSettings.Height, _boardSettings.Width, _boardSettings.MineCount);
                bool gameOver = false;

                while (!gameOver)
                {
                    GamePlay.DisplayBoard(board, _boardSettings.Letters);
                    Console.WriteLine(_messages.Instruction);

                    string input = _inputRetriever.UserInputReadLine();
                    bool validInput = GamePlay.UserInputValid(input, _boardSettings.Letters, _boardSettings.Height);

                    while (!validInput)
                    {
                        Console.WriteLine(_messages.InvalidInput);
                        input = _inputRetriever.UserInputReadLine();
                        validInput = GamePlay.UserInputValid(input, _boardSettings.Letters, _boardSettings.Height);
                    }

                    var inputCoords = GamePlay.MapUserInputToCoordinates(_boardSettings.Letters, input);

                    var selectedPoint = board[inputCoords[0], inputCoords[1]];

                    _state = UpdateGameStateOnUserInput(board, selectedPoint);

                    switch (_state)
                    {
                        case GameState.Won:
                        case GameState.Lost:
                            gameOver = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public GameState UpdateGameStateOnUserInput(GridPoint[,] board, GridPoint selectedPoint)
        {
            if (selectedPoint.IsMine)
            {
                _state = GamePlay.MineHit(board, _boardSettings.Letters, _messages);
                _inputRetriever.UserInputReadKey();
                return _state;
            }
            else
            {
                GamePlay.MineNotHit(selectedPoint);
                _state = GamePlay.CheckForWin(board, _boardSettings.MineCount);
                if (_state == GameState.Won)
                {
                    GamePlay.DisplayBoard(board, _boardSettings.Letters);
                    Console.WriteLine(_messages.Win);
                    return _state;
                }
                return _state;
            }
        }
    }
}
