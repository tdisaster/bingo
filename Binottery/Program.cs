using System;
using System.Collections.Generic;
using System.Linq;

namespace Binottery
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameEngine = new GameEngine();
            Printer.PrintUserOptions(gameEngine.GameStarted, gameEngine.GetGameStage());

            while (true)
            {
                ReadUserInput(Console.ReadLine().Trim(), gameEngine);
            }
        }

        static bool ReadUserInput(string userInput, GameEngine gameEngine)
        {
            switch (userInput)
            {
                case "a":

                    gameEngine.NewGame();
                    break;
                case "b":

                    gameEngine.ContinueGame();
                    break;
                case "c":

                    gameEngine.ShowTicket();
                    break;
                case "d":

                    gameEngine.EndCurrentGame();
                    break;
                case "e":

                    gameEngine.ExitGame();
                    break;
                default:
                    {

                        int userNumber;
                        if (int.TryParse(userInput, out userNumber))
                        {
                            return gameEngine.VerifyNumber(userNumber);
                        }
                        else
                        {
                            Printer.PrintInvalidInput();
                            return false;
                        }
                    }
            }
            return true;
        }

    }
}
