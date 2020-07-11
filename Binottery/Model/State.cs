using System;
using System.Collections.Generic;

namespace Binottery.Model
{
    [Serializable]
    public class State
    {
        public int[] GeneratedNumbers { get; set; } = new int[Constants.MatrixNumberOfColumns * Constants.MatrixNumberOfRows];
        public int[] WinningNumbers { get; set; } = new int[Constants.NumberOfWinningOptions];
        public List<int> UserNumbers { get; set; } = new List<int>();
        public int UserCredit { get; set; }
    }
}
