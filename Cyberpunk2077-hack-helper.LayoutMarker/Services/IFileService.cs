using Cyberpunk2077_hack_helper.Grabbing;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public interface IFileService
	{
		Layout Open(string filename);
		void Save(string filename, Layout layout);
	}
}
