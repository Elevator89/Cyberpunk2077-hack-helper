using Cyberpunk2077HackHelper.Grabbing;
using Newtonsoft.Json;
using System.IO;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	public class JsonFileService : IFileService
	{
		public Layout Open(string filename)
		{
			string contents = File.ReadAllText(filename);
			return JsonConvert.DeserializeObject<Layout>(contents);
		}

		public void Save(string filename, Layout layout)
		{
			string contents = JsonConvert.SerializeObject(layout);
			File.WriteAllText(filename, contents);
		}
	}
}
