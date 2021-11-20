namespace Cyberpunk2077HackHelper.Grabbing
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
