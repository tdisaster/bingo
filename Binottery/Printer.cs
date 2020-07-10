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
            Console.WriteLine(("The “Binottery” game").PadLeft(40));

            for (int rowNumber = 0; rowNumber< Constants.MatrixNumberOfRows; rowNumber++)
            {
                for (int colNumber = 0; colNumber < Constants.MatrixNumberOfColumns; colNumber++)
                {
                    var currentNumber = state.GeneratedNumbers[colNumber, rowNumber];

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
        internal static void PrintUserOptions(bool gameStarted, Helper.GameStage stage)
        {
            WriteInWhite(Constants.Options);

            if (!gameStarted)
            {
                WriteInWhite(Constants.OptionNew);          //either new or continue
                WriteInWhite(Constants.OptionContinue);     //either new or continue
                WriteInWhite(Constants.OptionShow);
                WriteInGrey(Constants.OptionEnd);
                WriteInWhite(Constants.OptionExit);
                WriteInGrey(Constants.OptionNumber);
            }
            else
            {
                WriteInGrey(Constants.OptionNew);
                WriteInGrey(Constants.OptionContinue);
                WriteInGrey(Constants.OptionShow);
                WriteInWhite(Constants.OptionEnd);
                WriteInWhite(Constants.OptionExit);
                if (stage == Helper.GameStage.EndOfGame)
                {
                    WriteInGrey(Constants.OptionNumber);
                }
                else
                {
                    WriteInWhite(Constants.OptionNumber);
                }
            }
        }
        private static void WriteInGrey(object str)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        private static void WriteInWhite(object str)
        {
            Console.WriteLine(str);
        }

    }
}
