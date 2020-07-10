using System;
using System.Collections.Generic;
using System.Linq;

namespace Binottery.Util
{
	class NumberGenerator
	{
		private List<int> Numbers;

		public NumberGenerator()
		{
			Numbers = new List<int>();
			for(var i =0; i < 10; i++)
			{
				Numbers.Add(i);
			}
		}

        private Random  Rand = new Random((int)DateTime.Now.Ticks);

		int[] GenerateNumbers(int multiplier)
		{
			var result = new int[3];

			result[0] = Numbers[Rand.Next(0, Numbers.Count - 1)];

			for (int rowNumber = 0; rowNumber < Constants.MatrixNumberOfRows; rowNumber++)
			{
				result[rowNumber] = Numbers[Rand.Next(0, Numbers.Count)];
				Numbers.Remove(result[rowNumber]);				
			}

			Numbers.AddRange(result);
			return result.Select(r=> multiplier * 10 + r).ToArray();
		}
	}
}
