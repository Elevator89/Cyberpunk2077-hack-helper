namespace Cyberpunk2077HackHelper.LayoutMarker.Tools
{
	public interface IToolManager
	{
		ITool ActiveTool { get; }

		void ActivateTool(ITool tool);
		void DeactivateActiveTool();
	}
}
