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

        public static string OptionNew = "a) New game";
        public static string OptionContinue = "b) Continue existing game";
        public static string OptionShow = "c) Display ticket";
        public static string OptionEnd = "d) End current game";
        public static string OptionExit = "e) Exit game";
        public static string OptionNumber = "0-89 Select a valid number that appears on the ticket except the ones already selected";
        public static string Options = "You have the following options:";

    }
}
