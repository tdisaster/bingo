using System;
using System.Collections.Generic;
using System.Text;

namespace Binottery.Model
{
	public enum GameAction
	{
		Invalid,
		NewGame,
		Continue,
		EndGame,
		ExitGame,
		InputNumber,
		OpenOrNewSession
	}

	public class GameActionResult
	{
		public int Number { get; set; }
		public GameAction GameAction { get; set; }
	}
}
