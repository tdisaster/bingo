using System;
using System.Collections.Generic;
using System.Linq;

namespace Binottery.Util
{
    class NumberGenerator
    {
        private readonly List<int> NumbersZeroToNine;
        private List<int> NumbersOfMatrixIndexes;

        private Random Rand = new Random((int)DateTime.Now.Ticks);

        public NumberGenerator()
        {
            NumbersZeroToNine = new List<int>();
            for (var i = 0; i < 10; i++)
            {
                NumbersZeroToNine.Add(i);
            }

            NumbersOfMatrixIndexes = new List<int>();
            for (var i = 0; i < Constants.MatrixNumberOfColumns * Constants.MatrixNumberOfRows; i++)
            {
                NumbersOfMatrixIndexes.Add(i);
            }
        }

        //Generates x (no of rows) random unique numbers for a specified column.
        public int[] GenerateXNumbers(int multiplier)
        {
            var result = new int[Constants.MatrixNumberOfRows];

            for (int rowNumber = 0; rowNumber < Constants.MatrixNumberOfRows; rowNumber++)
            {
                result[rowNumber] = NumbersZeroToNine[Rand.Next(0, NumbersZeroToNine.Count)];
                NumbersZeroToNine.Remove(result[rowNumber]);
            }

            NumbersZeroToNine.AddRange(result);
            return result.Select(r => multiplier * 10 + r).ToArray();
        }

        //Generates x (no of winning numbers) random indexes.
        public int[] GenerateWinningIndexes(int[] generatedNumbers)
        {
            var result = new int[Constants.NumberOfWinningOptions];

            for (int index = 0; index < Constants.NumberOfWinningOptions; index++)
            {
                result[index] = NumbersOfMatrixIndexes[Rand.Next(0, (Constants.MatrixNumberOfRows * Constants.MatrixNumberOfColumns - index))];
                NumbersOfMatrixIndexes.Remove(result[index]);
            }

            NumbersOfMatrixIndexes.AddRange(result);
            return result;
        }
    }
}
