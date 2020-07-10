using System;
using System.Collections.Generic;
using System.Text;

namespace Binottery
{
    [Serializable]
    public class State
    {
        private int[,] _generatedNumbers = new int[Constants.MatrixNumberOfColumns, Constants.MatrixNumberOfRows];
        private int[] _winningNumbers = new int[Constants.NumberOfWinningOptions];
        private List<int> _userNumbers = new List<int>();
        private int _userCredit;

        public int[,] GeneratedNumbers {
            get { return _generatedNumbers; }
            set { _generatedNumbers = value; }
        }
        public int[] WinningNumbers
        {
            get { return _winningNumbers; }
            set { _winningNumbers = value; }
        }
        public List<int> UserNumbers
        {
            get { return _userNumbers; }
            set { _userNumbers = value; }
        }
        public int UserCredit
        {
            get { return _userCredit; }
            set { _userCredit = value; }
        }
    }
}
