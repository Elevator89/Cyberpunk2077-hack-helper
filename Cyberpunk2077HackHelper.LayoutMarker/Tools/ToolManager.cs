namespace Cyberpunk2077HackHelper.LayoutMarker.Tools
{
	public class ToolManager : IToolManager
	{
		public ITool ActiveTool { get; private set; }

		public void ActivateTool(ITool tool)
		{
			DeactivateActiveTool();

			if (tool != null)
				tool.Reset();

			ActiveTool = tool;
		}

		public void DeactivateActiveTool()
		{
			if (ActiveTool == null)
				return;

			ActiveTool.Reset();
			ActiveTool = null;

		}
	}
}
