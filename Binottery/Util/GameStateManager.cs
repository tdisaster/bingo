using System;
using Binottery.Model;

namespace Binottery.Util
{
	public class GameStateManager
	{
		public GameActionResult GetNextAction(GameStage currentStage)
		{
			var input = Console.ReadLine()?.Trim();
			var result = new GameActionResult {GameAction = GameAction.Invalid};
			switch (input)
			{
				case Constants.NewGame:
					if (currentStage == GameStage.MainMenu) result.GameAction = GameAction.NewGame;
					break;
				case Constants.ContinueGame:
					if (currentStage == GameStage.MainMenu) result.GameAction = GameAction.Continue;
					break;
				case Constants.NewSession:
					if (currentStage != GameStage.MainMenu) result.GameAction = GameAction.OpenOrNewSession;
					break;
				case Constants.EndSession:
					if (currentStage == GameStage.InGame) result.GameAction = GameAction.EndGame;
					break;
				case Constants.ExitGame:
					result.GameAction = GameAction.ExitGame;
					break;
				default:
					if (currentStage == GameStage.InGame && int.TryParse(input, out var userNumber))
					{
						result.GameAction = GameAction.InputNumber;
						result.Number = userNumber;
					}
					break;
			}
			return result;
		}
	}
}