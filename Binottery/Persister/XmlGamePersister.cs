using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Binottery.Model;

namespace Binottery.Persister
{
	public class XmlGamePersister  :IGamePersister
	{
		private const string FileName = "save.xml";

		public void Save(State state)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(State));
			var fileStream = File.Open(FileName, FileMode.OpenOrCreate);
			serializer.Serialize(fileStream, state);
			fileStream.Close();
		}

		public State Load()
		{
			State state = null;
            var serializer = new XmlSerializer(typeof(State));
            try
			{
                var fileStream = File.Open(FileName, FileMode.Open);
                state = (State) serializer.Deserialize(fileStream);
				fileStream.Close();
			}
			catch (SerializationException)
			{
				//TODO show invalid file exception
			}
			return state;
		}
		
		public bool HasSaveState()
		{
			var fi = new FileInfo(FileName);
			return fi.Exists;
		}
	}
}
