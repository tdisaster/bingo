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
            for (int i = 0; i < numberOfLines; i++)
            {
                Console.WriteLine();
            }
        }
        internal static void PrintGrid(State state)
        {
            Console.WriteLine(("The “Binottery” game").PadLeft(40));
            int currentNumber;
            for (var rowNumber = 0; rowNumber < Constants.MatrixNumberOfRows; rowNumber++)
            {
                for (var index = 0; index < Constants.MatrixNumberOfColumns; index++)
                {
                    currentNumber = state.GeneratedNumbers[rowNumber + index * 3];

                    if (state.UserNumbers.Contains(currentNumber))
                    {
                        if (state.WinningNumbers.Contains(currentNumber))
                        {
                            Console.Write((currentNumber.ToString() + "*+*").PadRight(7));
                        }
                        else
                        {
                            Console.Write((currentNumber.ToString() + "*-*").PadRight(7));
                        }
                    }
                    else
                    {
                        Console.Write(currentNumber.ToString().PadRight(7));
                    }
                }

                PrintEmptyLines(2);
            }
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
                    Console.WriteLine(Constants.NewGame);
                    Console.WriteLine(Constants.ContinueGame);
                    Console.WriteLine(Constants.ExitGame);
                    break;
                case GameStage.Started:
                    Console.WriteLine(Constants.NewSesion);
                    Console.WriteLine(Constants.ExitGame);
                    break;
                case GameStage.InGame:
                    Console.WriteLine(Constants.EnterNumber);
                    Console.WriteLine(Constants.NewSesion);
                    Console.WriteLine(Constants.EndSession);
                    Console.WriteLine(Constants.ExitGame);
                    break;
                case GameStage.EndGame:
                    Console.WriteLine(Constants.NewSesion);
                    Console.WriteLine(Constants.ExitGame);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stage), stage, null);
            }
        }

        public static void PrintScore(int score)
        {
            Console.WriteLine($"Congratulations, you won {score}$!");
        }
    }
}
