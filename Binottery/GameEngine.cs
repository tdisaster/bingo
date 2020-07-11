using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Binottery.Model;
using Binottery.Persister;
using Binottery.Util;

namespace Binottery
{
    public class GameEngine
    {
	    #region Properties

	    private GameStage Stage { get; set; } = GameStage.MainMenu;
	    private IGamePersister Persister { get; }
	    private static State State { get; set; }
	    private GameStateManager StateManager { get; } = new GameStateManager();

	    #endregion

	    #region Constructor

	    public GameEngine(IGamePersister persister)
	    {
		    Persister = persister;
	    }

	    #endregion

        #region Options
        //new
        public void NewGame()
        {
            State = new State();
            Stage = GameStage.Started;
            InitGame();
            Printer.ClearScreen();
            Printer.PrintAvailableOptions(Stage);
        }
        //continue
        public void ContinueGame()
        {
            if (Persister.HasSaveState())
            {
                State = Persister.Load();
                Stage = GameStage.Started;
            }
            else
            {
                Printer.PrintSaveNotExist();

            }
            Printer.ClearScreen();
            Printer.PrintAvailableOptions(Stage);
        }
        //show
        public void OpenOrNewSession()
        {
            if (Stage == GameStage.InGame || Stage == GameStage.EndGame)
            {
	            InitGame();
            }

            Stage = State.UserNumbers.Count == Constants.NumberOfTries ? GameStage.EndGame : GameStage.InGame;
            Printer.ClearScreen();

            Printer.PrintGrid(State);
            if (Stage == GameStage.EndGame)
            {
                Printer.PrintScore(State.UserCredit);
            }

            Printer.PrintAvailableOptions(Stage);
        }
        //end
        public void EndSession()
        {
            Stage = GameStage.Started;
            Printer.ClearScreen();
            Printer.PrintScore(State.UserCredit);
            Printer.PrintAvailableOptions(Stage);
        }
        //exit
        public void ExitGame()
        {
	        Persister.Save(State);
            Environment.Exit(1);
        }
        //0-89
        public void GoToNextStep(int newNumber)
        {
            Printer.ClearScreen();
            State.UserNumbers.Add(newNumber);

            if (State.WinningNumbers.Contains(newNumber))
            {
                State.UserCredit++;
            }

            if (State.UserNumbers.Count == Constants.NumberOfTries)
            {
                var currentLuckyNumbers = State.UserNumbers.Count(userNr => State.WinningNumbers.Contains(userNr));

                if (currentLuckyNumbers == Constants.NumberOfTries)
                {
                    State.UserCredit *= 2;
                }
                Stage = GameStage.EndGame;
            }
            Printer.PrintGrid(State);

            if (State.UserNumbers.Count == Constants.NumberOfTries)
            {
                Printer.PrintScore(State.UserCredit);
            }
            Printer.PrintAvailableOptions(Stage);
        }

        #endregion

        public void Start()
        {
	        Printer.PrintAvailableOptions(Stage);
            Run();
        }

        #region Utils

        private void Run()
        {
	        var action = StateManager.GetNextAction(Stage);
	        switch (action.GameAction)
	        {
		        case GameAction.Invalid:
			        Printer.PrintInvalidInput();
			        Run();
			        break;
		        case GameAction.NewGame:
			        NewGame();
			        Run();
			        break;
		        case GameAction.Continue:
			        ContinueGame();
			        Run();
			        break;
		        case GameAction.OpenOrNewSession:
			        OpenOrNewSession();
			        Run();
			        break;
		        case GameAction.EndGame:
			        EndSession();
			        Run();
			        break;
		        case GameAction.ExitGame:
			        ExitGame();
			        break;
		        case GameAction.InputNumber:
			        if (VerifyNumber(action.Number))
			        {
				        GoToNextStep(action.Number);
			        }
			        else
			        {
				        Printer.PrintInvalidNumber(action.Number);
			        }
			        Run();
			        break;
		        default:
			        throw new ArgumentOutOfRangeException($"Unknown paramater {action.GameAction}");
	        }
        }

        private bool VerifyNumber(int newNumber)
        {
	        return State.GeneratedNumbers.Contains(newNumber) && 
	               !State.UserNumbers.Contains(newNumber);
        }

        private void InitGame()
        {
	        State.UserNumbers.Clear();
	        var generator = new NumberGenerator();
	        GenerateRandomMatrix(generator);
	        GenerateRandomWinningNumbers(generator);
        }

        private static void GenerateRandomMatrix(NumberGenerator generator)
        {
	        for (var colNumber = 0; colNumber < Constants.MatrixNumberOfColumns; colNumber++)
	        {
		        var numbers = generator.GenerateXNumbers(colNumber);
		        for (var index = 0; index < Constants.MatrixNumberOfRows; index++)
		        {
			        State.GeneratedNumbers[colNumber * 3 + index] = numbers[index];
		        }
	        }
        }
        private static void GenerateRandomWinningNumbers(NumberGenerator generator)
        {
	        var winningNumbersIndexes = generator.GenerateWinningIndexes();

	        for (var i = 0; i < Constants.NumberOfWinningOptions; i++)
	        {
		        State.WinningNumbers[i] = State.GeneratedNumbers[winningNumbersIndexes[i]];
	        }
        }

        #endregion
    }
}