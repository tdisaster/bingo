using Binottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binottery
{
    public static class Printer
    {
        internal static void ClearScreen()
        {
            Console.Clear();
        }

		internal static void PrintEmptyLines(int numberOfLines)
        {
            for (int i = 0; i< numberOfLines; i++)
            {
                Console.WriteLine();
            }
        }
        internal static void PrintGrid(State state)
        {
            //TODO
            //Console.WriteLine(("The “Binottery” game").PadLeft(40));

            //for (int rowNumber = 0; rowNumber< Constants.MatrixNumberOfRows; rowNumber++)
            //{
            //    for (int colNumber = 0; colNumber < Constants.MatrixNumberOfColumns; colNumber++)
            //    {
            //        var currentNumber = state.GeneratedNumbers[colNumber, rowNumber];

            //        if (state.UserNumbers.Contains(currentNumber))
            //        {
            //            if (state.WinningNumbers.Contains(currentNumber))
            //            {
            //                Console.Write((currentNumber.ToString() + "*+*").PadRight(7));
            //            }
            //            else
            //            {
            //                Console.Write((currentNumber.ToString() + "*-*").PadRight(7));
            //            }
            //        }
            //        else
            //        {
            //            Console.Write(currentNumber.ToString().PadRight(7));
            //        }
            //    }

            //    PrintEmptyLines(2);
            //}
        }
        internal static void PrintInvalidInput()
        {
            Console.WriteLine($"Your input is invalid. Please check above the valid options!");
        }
        internal static void PrintInvalidNumber(int invalidNumber)
        {
            Console.WriteLine($"The number you have entered ({invalidNumber}) does not exist in on the ticket. Please type a number from the list!");
        }
        internal static void PrintAvailableOptions(GameStage stage)
        {
            Console.WriteLine(Constants.Options);
            switch (stage)
            {
	            case GameStage.MainMenu:
		            Console.WriteLine(Constants.NewSession);
		            Console.WriteLine(Constants.ContinueGame);
		            Console.WriteLine(Constants.ExitGame);
                    break;
	            case GameStage.Started:
		            Console.WriteLine(Constants.NewGame);
		            Console.WriteLine(Constants.ExitGame);
                    break;
	            case GameStage.InGame:
		            Console.WriteLine(Constants.EnterNumber);
		            Console.WriteLine(Constants.NewGame);
		            Console.WriteLine(Constants.EndSession);
		            Console.WriteLine(Constants.ExitGame);
                    break;
	            case GameStage.EndGame:
		            Console.WriteLine(Constants.NewGame);
		            Console.WriteLine(Constants.ExitGame);
                    break;
	            default:
		            throw new ArgumentOutOfRangeException(nameof(stage), stage, null);
            }
        }

        public static void PrintScore()
        {
	        throw new NotImplementedException();
        }
    }
}
