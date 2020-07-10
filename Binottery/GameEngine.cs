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

		public GameEngine(IGamePersister persister)
		{
			Persister = persister;
		}

		public void Print()
		{
			Printer.PrintAvailableOptions(Stage);
		}

		private static List<int> _possibileNumbersForColumn;
		private static State State { get; set; }
		private static List<int> FlatListFromMatrice { get; set; }
		public bool InGame { get; private set; }

		#region Options

		public void NewGame()
		{
			Stage = GameStage.Started;
			//TODO generate new values
			Printer.ClearScreen();
			Printer.PrintGrid(State);
			Printer.PrintAvailableOptions(Stage);
		}

		public void ContinueGame()
		{
			LoadState();
			Stage = GameStage.Started;
			Printer.ClearScreen();
			Printer.PrintAvailableOptions(Stage);
		}

		public void NewSession()
		{
			GenerateRandomMatrix();
			GenerateRandomWinningNumbers();

			Stage = GameStage.InGame;
			Printer.ClearScreen();
			Printer.PrintGrid(State);
			Printer.PrintAvailableOptions(Stage);
		}

		public void EndSession()
		{
			Stage = GameStage.Started;
			Printer.ClearScreen();
			Printer.PrintScore();
			Printer.PrintAvailableOptions(Stage);
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
			if (FlatListFromMatrice.Contains(userNumber))
			{
				Printer.ClearScreen();
				State.UserNumbers.Add(userNumber);
				Printer.PrintGrid(State);
				Printer.PrintEmptyLines(2);
				//TODO set stage
				Printer.PrintAvailableOptions(Stage);
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

			for (var colNumber = 0; colNumber < Constants.MatrixNumberOfColumns; colNumber++)
			{
				_possibileNumbersForColumn = Helper.FillListFromRange(colNumber * 10, colNumber * 10 + 9);

				for (var rowNumber = 0; rowNumber < Constants.MatrixNumberOfRows; rowNumber++)
				{
					currentNumber = _possibileNumbersForColumn[_rand.Next(0, _possibileNumbersForColumn.Count)];
					_possibileNumbersForColumn.Remove(currentNumber);
					FlatListFromMatrice.Add(currentNumber);
					//TODO
					//State.GeneratedNumbers[colNumber, rowNumber] = currentNumber;
				}
			}
		}

		private static void GenerateRandomWinningNumbers()
		{
			int currentNumber;
			var auxFlatMatrice = FlatListFromMatrice.ToList();
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
			//TODO validate input based on current state
			switch (input)
			{
				case Constants.NewGame:
					NewGame();
					break;
				case Constants.ContinueGame:
					ContinueGame();
					break;
				case Constants.NewSession:
					NewSession();
					break;
				case Constants.EndSession:
					EndSession();
					break;
				case Constants.ExitGame:
					ExitGame();
					break;
				default:
					int userNumber;
					if (int.TryParse(input, out userNumber))
					{
						if (VerifyNumber(userNumber))
						{
							//TODO
						}
						else
						{
							//TODO
						}
					}
					else
					{
						Printer.PrintInvalidInput();
					}
					break;
			}
		}
	}
}