namespace Cyberpunk2077HackHelper.Overlay
{
	class Program
	{
		static void Main(string[] args)
		{
			GameOverlay.TimerService.EnableHighPrecisionTimers();

			using (OverlayApplication overlayApp = new OverlayApplication())
			{
				overlayApp.Run();
			}
		}
	}
}
