namespace Cyberpunk2077_hack_helper.LayoutMarker.Tools
{
	public interface IToolManager
	{
		ITool ActiveTool { get; }

		void ActivateTool(ITool tool);
		void DeactivateActiveTool();
	}
}
