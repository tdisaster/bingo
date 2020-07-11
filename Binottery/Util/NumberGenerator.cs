using System;
using System.Collections.Generic;
using System.Linq;

namespace Binottery.Util
{
    class NumberGenerator
    {
        private readonly List<int> _numbersZeroToNine = new List<int>();
        private readonly List<int> _numbersOfMatrixIndexes = new List<int>();
        private readonly Random _rand = new Random((int)DateTime.Now.Ticks);

        public NumberGenerator()
        {
            for (var i = 0; i < 10; i++)
            {
                _numbersZeroToNine.Add(i);
            }
            for (var i = 0; i < Constants.MatrixNumberOfColumns * Constants.MatrixNumberOfRows; i++)
            {
                _numbersOfMatrixIndexes.Add(i);
            }
        }

        //Generates x (no of rows) random unique numbers for a specified column.
        public int[] GenerateXNumbers(int multiplier)
        {
            var result = new int[Constants.MatrixNumberOfRows];

            for (var rowNumber = 0; rowNumber < Constants.MatrixNumberOfRows; rowNumber++)
            {
                result[rowNumber] = _numbersZeroToNine[_rand.Next(0, _numbersZeroToNine.Count)];
                _numbersZeroToNine.Remove(result[rowNumber]);
            }

            _numbersZeroToNine.AddRange(result);
            return result.Select(r => multiplier * 10 + r).ToArray();
        }

        //Generates x (no of winning numbers) random indexes.
        public int[] GenerateWinningIndexes()
        {
            var result = new int[Constants.NumberOfWinningOptions];
            for (var index = 0; index < Constants.NumberOfWinningOptions; index++)
            {
                result[index] = _numbersOfMatrixIndexes[
	                _rand.Next(0, Constants.MatrixNumberOfRows * Constants.MatrixNumberOfColumns - index)
                ];
                _numbersOfMatrixIndexes.Remove(result[index]);
            }
            _numbersOfMatrixIndexes.AddRange(result);
            return result;
        }
    }
}
