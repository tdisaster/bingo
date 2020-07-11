using Binottery.Model;

namespace Binottery.Persister
{
	public interface IGamePersister
	{
		void Save(State state);
		State Load();
		bool HasSaveState();
	}
}
