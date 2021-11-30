using System.Threading.Tasks;

namespace Cyberpunk2077HackHelper.Overlay
{
	class Program
	{
		static void Main(string[] args)
		{
			GameOverlay.TimerService.EnableHighPrecisionTimers();

			using (var example = new Example())
			{
				example.Run();
			}
		}
	}
}
