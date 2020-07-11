using System;
using System.Collections.Generic;
using System.Linq;
using Binottery.Util;

namespace Binottery
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var gameEngine = new GameEngine(new BinaryGamePersister());
			gameEngine.PrintOptions();
			gameEngine.Read();
		}
	}
}