using System.Collections.Generic;

namespace Cyberpunk2077HackHelper.Solving
{
	public class MatrixSequencePointComparer : IEqualityComparer<MatrixSequencePoint>
	{
		public bool Equals(MatrixSequencePoint x, MatrixSequencePoint y)
		{
			return x.Point == y.Point;
		}

		public int GetHashCode(MatrixSequencePoint obj)
		{
			return obj.Point.GetHashCode();
		}
	}
}
