using System;
using System.Collections.Generic;
using System.Linq;

namespace Binottery.Util
{
    public class NumberGenerator
    {
        private List<int> Digits { get; } = new List<int>();
        private List<int> MatrixIndexes { get; }= new List<int>();
        private readonly Random _rand = new Random((int)DateTime.Now.Ticks);

        public NumberGenerator()
        {
            for (var i = 0; i < 10; i++)
            {
                Digits.Add(i);
            }
            for (var i = 0; i < Constants.MatrixNumberOfColumns * Constants.MatrixNumberOfRows; i++)
            {
                MatrixIndexes.Add(i);
            }
        }

        //Generates x (no of rows) random unique numbers for a specified column.
        public int[] GenerateXNumbers(int multiplier)
        {
            var result = new int[Constants.MatrixNumberOfRows];

            for (var rowNumber = 0; rowNumber < Constants.MatrixNumberOfRows; rowNumber++)
            {
                result[rowNumber] = Digits[_rand.Next(0, Digits.Count)];
                Digits.Remove(result[rowNumber]);
            }

            Digits.AddRange(result);
            return result.Select(r => multiplier * 10 + r).ToArray();
        }

        //Generates x (no of winning numbers) random indexes.
        public int[] GenerateWinningIndexes()
        {
            var result = new int[Constants.NumberOfWinningOptions];
            for (var index = 0; index < Constants.NumberOfWinningOptions; index++)
            {
                result[index] = MatrixIndexes[
	                _rand.Next(0, Constants.MatrixNumberOfRows * Constants.MatrixNumberOfColumns - index)
                ];
                MatrixIndexes.Remove(result[index]);
            }
            MatrixIndexes.AddRange(result);
            return result;
        }
    }
}
