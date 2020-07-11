using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Binottery.Model;
using Binottery.Util;

namespace Binottery
{
    public class GameEngine
    {
        private GameStage Stage { get; set; } = GameStage.MainMenu;
        private IGamePersister Persister { get; }
        private static State State { get; set; }
        public bool InGame { get; private set; }

        public GameEngine(IGamePersister persister)
        {
            Persister = persister;
        }

        #region Options
        //new
        public void NewGame()
        {
            State = new State();
            Stage = GameStage.Started;

            GenerateRandomMatrix();
            GenerateRandomWinningNumbers();

            Printer.ClearScreen();
            Printer.PrintAvailableOptions(Stage);

            Read();
        }
        //continue
        public void ContinueGame()
        {
            LoadState();
            Stage = GameStage.Started;
            Printer.ClearScreen();
            Printer.PrintAvailableOptions(Stage);

            Read();
        }
        //show
        public void OpenOrNewSession()
        {
            if (Stage == GameStage.InGame || Stage == GameStage.EndGame)
            {
                State.UserNumbers = new List<int>();
                GenerateRandomMatrix();
                GenerateRandomWinningNumbers();
            }

            Stage = State.UserNumbers.Count == Constants.NumberOfTries ? GameStage.EndGame : GameStage.InGame;
            Printer.ClearScreen();

            if (Stage == GameStage.EndGame)
            {
                Printer.PrintScore(State.UserCredit);
            }
            else
            {
                Printer.PrintGrid(State);
            }

            Printer.PrintAvailableOptions(Stage);

            Read();
        }
        //end
        public void EndSession()
        {
            Stage = GameStage.Started;
            Printer.ClearScreen();
            Printer.PrintScore(State.UserCredit);
            Printer.PrintAvailableOptions(Stage);

            Read();
        }
        //exit
        public void ExitGame()
        {
            SaveState();
            Environment.Exit(1);
        }
        //0-89
        public void GoToNextStep(int userNumber)
        {
            Printer.ClearScreen();
            State.UserNumbers.Add(userNumber);
            Printer.PrintGrid(State);
            Printer.PrintEmptyLines(1);

            if (State.UserNumbers.Count == Constants.NumberOfTries)
            {
                var currentLuckyNumbers = State.UserNumbers.Count(userNr => State.WinningNumbers.Contains(userNr));

                State.UserCredit += currentLuckyNumbers;
                if (currentLuckyNumbers == Constants.NumberOfTries)
                {
                    //
                    State.UserCredit *= 2;
                }
                Stage = GameStage.EndGame;
                Printer.PrintScore(State.UserCredit);
                Printer.PrintAvailableOptions(Stage);
            }
            else
            {
                Printer.PrintAvailableOptions(Stage);
            }

            Read();
        }

        #endregion

        public void Read()
        {
            while (true)
            {
                var input = Console.ReadLine()?.Trim();
                switch (input)
                {
                    case Constants.NewGame:
                        if (Stage == GameStage.MainMenu)
                        {
                            NewGame();
                        }
                        else
                        {
                            Printer.PrintInvalidInput();
                        }

                        break;
                    case Constants.ContinueGame:
                        if (Stage == GameStage.MainMenu)
                        {
                            ContinueGame();
                        }
                        else
                        {
                            Printer.PrintInvalidInput();
                        }

                        break;
                    case Constants.NewSession:
                        if (Stage != GameStage.MainMenu)
                        {
                            OpenOrNewSession();
                        }
                        else
                        {
                            Printer.PrintInvalidInput();
                        }

                        break;
                    case Constants.EndSession:
                        if (Stage == GameStage.InGame)
                        {
                            EndSession();
                        }
                        else
                        {
                            Printer.PrintInvalidInput();
                        }

                        break;
                    case Constants.ExitGame:
                        ExitGame();
                        break;
                    default:
                        if (Stage == GameStage.InGame)
                        {
                            if (int.TryParse(input, out var userNumber))
                            {
                                if (VerifyNumber(userNumber))
                                {
                                    GoToNextStep(userNumber);
                                }
                                else
                                {
                                    Printer.PrintInvalidNumber(userNumber);
                                }
                            }
                            else
                            {
                                Printer.PrintInvalidInput();
                            }
                        }
                        else
                        {
                            Printer.PrintInvalidInput();
                        }

                        continue;
                }

                break;
            }
        }

        private void SaveState()
        {
            Persister.Save(State);
        }
        private void LoadState()
        {
            State = Persister.Load();
        }

        public bool VerifyNumber(int userNumber)
        {
            if (State.GeneratedNumbers.Contains(userNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void GenerateRandomMatrix()
        {
            var numberGenerator = new NumberGenerator();

            for (var colNumber = 0; colNumber < Constants.MatrixNumberOfColumns; colNumber++)
            {
                var numbers = numberGenerator.GenerateXNumbers(colNumber);
                for (var index = 0; index < Constants.MatrixNumberOfRows; index++)
                {
                    State.GeneratedNumbers[colNumber * 3 + index] = numbers[index];
                }
            }
        }
        private static void GenerateRandomWinningNumbers()
        {
            var numberGenerator = new NumberGenerator();
            var winningNumbersIndexes = numberGenerator.GenerateWinningIndexes(State.GeneratedNumbers);

            for (var i = 0; i < Constants.NumberOfWinningOptions; i++)
            {
                State.WinningNumbers[i] = State.GeneratedNumbers[winningNumbersIndexes[i]];
            }
        }

        public void PrintOptions()
        {
            Printer.PrintAvailableOptions(Stage);
        }
    }
}