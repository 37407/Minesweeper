using System;

namespace Minesweeper
{
    public class GameRunner
    {
        private GameState _state;
        private readonly BoardSettings _boardSettings;
        private readonly Messages _messages;

        public GameRunner(BoardSettings boardSettings, Messages messages)
        {
            _boardSettings = boardSettings;
            _messages = messages;
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

                    string input = Console.ReadLine().ToUpperInvariant();
                    bool validInput = GamePlay.UserInputValid(input, _boardSettings.Letters, _boardSettings.Height);

                    while (!validInput)
                    {
                        Console.WriteLine(_messages.InvalidInput);
                        input = Console.ReadLine().ToUpperInvariant();
                        validInput = GamePlay.UserInputValid(input, _boardSettings.Letters, _boardSettings.Height);
                    }

                    var inputCoords = GamePlay.MapUserInputToCoordinates(_boardSettings.Letters, input);

                    var selectedPoint = board[inputCoords[0], inputCoords[1]];

                    gameOver = selectedPoint.IsMine;

                    if (gameOver)
                    {
                        _state = GamePlay.MineHit(board, _boardSettings.Letters, _messages);
                        Console.ReadKey();
                    }
                    else
                    {
                        GamePlay.MineNotHit(selectedPoint);
                        _state = GamePlay.CheckForWin(board, _boardSettings.MineCount);
                        if (_state == GameState.Won)
                        {
                            GamePlay.DisplayBoard(board, _boardSettings.Letters);
                            Console.WriteLine(_messages.Win);
                            gameOver = true;
                        }
                    }
                }
            }
        }
    }
}
