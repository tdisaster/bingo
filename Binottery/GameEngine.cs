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
        private static Random _rand = new Random();

        public GameStage Stage { get; set; } = GameStage.MainMenu;

        private IGamePersister Persister { get; }

        private int currentLuckyNumbers {get;set;} = 0;

        public GameEngine(IGamePersister persister)
        {
            Persister = persister;
        }

        public void Print()
        {
            Printer.PrintAvailableOptions(Stage);
        }

        private static State State { get; set; }
        public bool InGame { get; private set; }

        #region Options

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

        public void ContinueGame()
        {
            LoadState();
            Stage = GameStage.Started;
            Printer.ClearScreen();
            Printer.PrintAvailableOptions(Stage);

            Read();
        }

        public void NewSession()
        {
            if (Stage == GameStage.InGame || Stage == GameStage.EndGame)
            {
                State.UserNumbers = new List<int>();
                GenerateRandomMatrix();
                GenerateRandomWinningNumbers();
            }

            Stage = GameStage.InGame;
            Printer.ClearScreen();
            Printer.PrintGrid(State);
            Printer.PrintAvailableOptions(Stage);

            Read();
        }

        public void EndSession()
        {
            Stage = GameStage.Started;
            Printer.ClearScreen();
            Printer.PrintScore(State.UserCredit);
            Printer.PrintAvailableOptions(Stage);

            Read();
        }

        public void ExitGame()
        {
            //Save state
            SaveState();
            Environment.Exit(1);
        }

        #endregion

        private void SaveState()
        {
            Persister.Save(State);
        }

        private void LoadState()
        {
            State = Persister.Load();
        }

        public void StartGame()
        {
            GenerateRandomMatrix();
            GenerateRandomWinningNumbers();
            Printer.PrintGrid(State);
        }

        public bool VerifyNumber(int userNumber)
        {
            if (State.GeneratedNumbers.Contains(userNumber))
            {
                return true;
            }
            else
            {
                Printer.PrintInvalidNumber(userNumber);
                return false;
            }
        }

        public void GoToNextStep(int userNumber)
        {
            Printer.ClearScreen();
            State.UserNumbers.Add(userNumber);
            Printer.PrintGrid(State);
            Printer.PrintEmptyLines(2);

            if (State.WinningNumbers.Contains(userNumber))
            {
                currentLuckyNumbers++;
            }

            if (State.UserNumbers.Count == 5)
            {
                State.UserCredit += currentLuckyNumbers;
                if (currentLuckyNumbers == 5)
                {
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

        private static void GenerateRandomMatrix()
        {
            NumberGenerator numberGenerator = new NumberGenerator();

            for (var colNumber = 0; colNumber < Constants.MatrixNumberOfColumns; colNumber++)
            {
                var numbers = numberGenerator.GenerateNumbers(colNumber);
                for (int index = 0; index < Constants.MatrixNumberOfRows; index++)
                {
                    State.GeneratedNumbers[colNumber * 3 + index] = numbers[index];
                }
            }
        }

        private static void GenerateRandomWinningNumbers()
        {
            int currentNumber;
            var auxFlatMatrice = State.GeneratedNumbers.ToList();
            for (var index = 0; index < Constants.NumberOfWinningOptions; index++)
            {
                currentNumber = auxFlatMatrice[_rand.Next(0, auxFlatMatrice.Count)];
                State.WinningNumbers[index] = currentNumber;
                auxFlatMatrice.Remove(currentNumber);
            }
        }

        public void Read()
        {
            var input = Console.ReadLine()?.Trim();
            switch (input)
            {
                case Constants.NewGame:
                    if (Stage == GameStage.MainMenu) { NewGame(); }
                    else { Printer.PrintInvalidInput(); }

                    break;
                case Constants.ContinueGame:
                    if (Stage == GameStage.MainMenu) { ContinueGame(); }
                    else { Printer.PrintInvalidInput(); }
                    break;
                case Constants.NewSesion:
                    if (Stage != GameStage.MainMenu) { NewSession(); }
                    else { Printer.PrintInvalidInput(); }

                    break;
                case Constants.EndSession:
                    if (Stage == GameStage.InGame) { EndSession(); }
                    else { Printer.PrintInvalidInput(); }

                    break;
                case Constants.ExitGame:
                    ExitGame();
                    break;
                default:
                    if (Stage == GameStage.InGame)
                    {
                        int userNumber;
                        if (int.TryParse(input, out userNumber))
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
                    Read();

                    break;
            }
        }
    }
}