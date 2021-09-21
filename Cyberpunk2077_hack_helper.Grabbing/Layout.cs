namespace Cyberpunk2077_hack_helper.Grabbing
{
	public class Layout
	{
		public LayoutTable Matrix { get; }
		public LayoutTable Sequences { get; }

		public Layout(LayoutTable matrix, LayoutTable sequences)
		{
			Matrix = matrix;
			Sequences = sequences;
		}
	}
}
