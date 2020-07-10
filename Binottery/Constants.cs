using System;
using System.Collections.Generic;
using System.Text;

namespace Binottery
{
    public static class Constants
    {
        public static readonly int MatrixNumberOfColumns = 9;
        public static readonly int MatrixNumberOfRows = 3;
        public static readonly int NumberOfWinningOptions = 6;
        public static readonly int NumberOfTries = 5; //if modified Constants.State should change also

        public const string NewGame = "new";
        public const string ContinueGame = "continue";
        public const string NewSesion = "show";
        public const string EndSession= "end";
        public const string ExitGame = "exit";
        public static string EnterNumber = "0-89 Select a valid number that appears on the ticket except the ones already selected";
        public static string Options = "You have the following options:";

    }
}
