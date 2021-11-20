using Cyberpunk2077HackHelper.Grabbing;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	public interface IFileService
	{
		Layout Open(string filename);
		void Save(string filename, Layout layout);
	}
}
