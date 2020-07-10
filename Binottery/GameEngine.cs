using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Binottery
{
    public class GameEngine
    {
        private static Random  _rand = new Random();
        private static List<int> _possibileNumbersForColumn;
        private static State _state { get; set; }
        private static List<int> FlatListFromMatrice { get; set; }
        public bool GameStarted { get; private set; }
        public Helper.GameStage GetGameStage()
        {
            return (Helper.GameStage)_state.UserNumbers.Count;
        }

        public GameEngine()
        {
            _state = new State();
        }

        internal void NewGame()
        {
            GameStarted = true;

            GenerateRandomMatrix();
            GenerateRandomWinningNumbers();

            Printer.ClearScreen();
            Printer.PrintGrid(_state);
            Printer.PrintEmptyLines(2);
            Printer.PrintUserOptions(GameStarted, GetGameStage());
        }

        internal void ContinueGame()
        {
            Printer.ClearScreen();
            //Load state
            Helper.LoadState("Binottery.SavedState.txt", _state);

            Printer.PrintGrid(_state);
            Printer.PrintEmptyLines(2);
            Printer.PrintUserOptions(GameStarted, GetGameStage());
            throw new NotImplementedException();
        }


        public GameEngine(State state)
        {
            GameEngine._state = state;
            FlatListFromMatrice = new List<int>();
        }

        internal void ShowTicket()
        {
            Printer.ClearScreen();
            throw new NotImplementedException();
        }
        internal void EndCurrentGame()
        {
            Printer.ClearScreen();
            GameStarted = false;
            _state = new State();
            Printer.PrintUserOptions(GameStarted, GetGameStage());
        }
        internal void ExitGame()
        {
            //Save state
            SaveState();
            System.Environment.Exit(1);
        }
        private void SaveState()
        {
            //var x = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            Helper.WriteToXmlFile("Binottery.SavedState.txt", _state);
        }
        public void StartGame()
        {
            GenerateRandomMatrix();
            GenerateRandomWinningNumbers();
            Printer.PrintGrid(_state);
        }
        public bool VerifyNumber(int userNumber)
        {
            if (FlatListFromMatrice.Contains(userNumber))
            {
                Printer.ClearScreen();
                _state.UserNumbers.Add(userNumber);
                Printer.PrintGrid(_state);
                Printer.PrintEmptyLines(2);
                Printer.PrintUserOptions(GameStarted, GetGameStage());
                return true;
            }
            else
            {
                Printer.PrintInvalidNumber(userNumber);
                return false;
            }
        }

        private static void GenerateRandomMatrix()
        {
            FlatListFromMatrice = new List<int>();
            int currentNumber;

            for (int colNumber = 0; colNumber < Constants.MatrixNumberOfColumns; colNumber++)
            {
                _possibileNumbersForColumn = Helper.FillListFromRange(colNumber * 10, (colNumber * 10) + 9);

                for (int rowNumber = 0; rowNumber < Constants.MatrixNumberOfRows; rowNumber++)
                {
                    currentNumber = _possibileNumbersForColumn[_rand.Next(0, _possibileNumbersForColumn.Count)];
                    _possibileNumbersForColumn.Remove(currentNumber);
                    FlatListFromMatrice.Add(currentNumber);
                    _state.GeneratedNumbers[colNumber, rowNumber] = currentNumber;
                }
            }
        }
        private static void GenerateRandomWinningNumbers()
        {
            int currentNumber;
            var auxFlatMatrice = FlatListFromMatrice.ToList();
            for (int index = 0; index < Constants.NumberOfWinningOptions; index++)
            {
                currentNumber = auxFlatMatrice[_rand.Next(0, auxFlatMatrice.Count)];
                _state.WinningNumbers[index] = currentNumber;
                auxFlatMatrice.Remove(currentNumber);
            }
        }

        private void LoadUserNumbers()
        {
            throw new NotImplementedException();
        }
        private void LoadRandomWinningNumbers()
        {
            throw new NotImplementedException();
        }
        private void LoadRandomMatrix()
        {
            throw new NotImplementedException();
        }
    }
}
