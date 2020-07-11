using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Binottery.Model;

namespace Binottery.Util
{
	public class BinaryGamePersister  :IGamePersister
	{
		private const string FileName = "save.dat";

		public void Save(State state)
		{
			var formatter = new BinaryFormatter();
			var fileStream = File.Open(FileName, FileMode.OpenOrCreate);
			formatter.Serialize(fileStream, state);
			fileStream.Close();
		}

		public State Load()
		{
			State state = null;
            var formatter = new BinaryFormatter();
            try
			{
                var fileStream = File.Open(FileName, FileMode.Open);
				state = (State) formatter.Deserialize(fileStream);
				fileStream.Close();
			}
			catch (SerializationException)
			{
				//TODO show invalid file exception
			}
			return state;
		}
	}
}
