namespace Cyberpunk2077HackHelper.Solving.Wave
{
	/// <summary>
	/// Validates and processes items for wave algorithm
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	public interface IWaveItemProcessor<TItem, TResult>
	{
		bool TryProcessItem(TItem item, out TResult result);
	}
}
