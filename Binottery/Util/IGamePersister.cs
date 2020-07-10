using System;
using System.Collections.Generic;
using System.Text;
using Binottery.Model;

namespace Binottery.Util
{
	public interface IGamePersister
	{
		void Save(State state);
		State Load();
	}
}
