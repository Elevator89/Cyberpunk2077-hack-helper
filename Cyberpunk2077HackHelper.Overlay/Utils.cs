using System;
using System.Diagnostics;

namespace Cyberpunk2077HackHelper.Overlay
{
	public static class Utils
	{
		public static IntPtr WinGetHandle(string wName)
		{
			foreach (Process pList in Process.GetProcesses())
				if (pList.MainWindowTitle.Contains(wName))
					return pList.MainWindowHandle;

			return IntPtr.Zero;
		}
	}
}
