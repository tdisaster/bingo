using Binottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binottery
{
    public static class Printer
    {
        internal static void PrintAvailableOptions(GameStage stage)
        {
            Console.WriteLine(Constants.Options);
            PrintEmptyLines();
            switch (stage)
            {
                case GameStage.MainMenu:
                    Console.WriteLine(Constants.NewGame);
                    Console.WriteLine(Constants.ContinueGame);
                    Console.WriteLine(Constants.ExitGame);
                    break;
                case GameStage.Started:
                    Console.WriteLine(Constants.NewSession);
                    Console.WriteLine(Constants.ExitGame);
                    break;
                case GameStage.InGame:
                    Console.WriteLine(Constants.EnterNumber);
                    Console.WriteLine(Constants.NewSession);
                    Console.WriteLine(Constants.EndSession);
                    Console.WriteLine(Constants.ExitGame);
                    break;
                case GameStage.EndGame:
                    Console.WriteLine(Constants.NewSession);
                    Console.WriteLine(Constants.ExitGame);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stage), stage, null);
            }
        }

        internal static void PrintGrid(State state)
        {
            Console.WriteLine(Constants.TheBinotteryGame.PadLeft(40));

            int currentNumber;
            for (var rowNumber = 0; rowNumber < Constants.MatrixNumberOfRows; rowNumber++)
            {
                for (var index = 0; index < Constants.MatrixNumberOfColumns; index++)
                {
                    currentNumber = state.GeneratedNumbers[rowNumber + index * 3];

                    if (state.UserNumbers.Contains(currentNumber))
                    {
                        Console.Write(state.WinningNumbers.Contains(currentNumber)
                            ? (currentNumber.ToString() + "*+*").PadRight(7)
                            : (currentNumber.ToString() + "*-*").PadRight(7));
                    }
                    else
                    {
                        Console.Write(currentNumber.ToString().PadRight(7));
                    }
                }
                PrintEmptyLines();
            }
            PrintEmptyLines();
            PrintCurrentCredit(state.UserCredit);
            PrintEmptyLines();
        }

        internal static void ClearScreen()
        {
            Console.Clear();
        }

        internal static void PrintEmptyLines(int numberOfLines = 1)
        {
            for (var i = 0; i < numberOfLines; i++)
            {
                Console.WriteLine();
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

        public static void PrintScore(int score)
        {
            if (score == 0)
            {
                Console.WriteLine($"You are not the luckiest person... You have {score}$!");
            }
            else if (score < 5)
            {
                Console.WriteLine($"This is not... nothing, at least you have {score}$. Keep playing!");
            }
            else if (score < 10)
            {
                Console.WriteLine($"You are starting to get a hand of it, you have {score}$");
            }
            else if (score < 15)
            {
                Console.WriteLine($"Nice! you have {score}$!");
            }
            else if (score < 20)
            {
                Console.WriteLine($"I think you should take it easy, you have enough: {score}$");
            }

            Console.WriteLine($"You have more than enough: {score}$. You can type 'end' to start from scratch.");
        }

        public static void PrintCurrentCredit(int score)
        {
            Console.WriteLine($"Current credit is: {score}$");
        }
    }
}
