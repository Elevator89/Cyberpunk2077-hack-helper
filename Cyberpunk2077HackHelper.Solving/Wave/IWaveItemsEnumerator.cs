using System.Collections.Generic;

namespace Cyberpunk2077HackHelper.Solving.Wave
{
	/// <summary>
	/// Enumerates items for wave algorithm
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	public interface IWaveItemsEnumerator<TItem>
	{
		/// <summary>
		/// Returns items from which wave algorithm should start its work
		/// </summary>
		/// <returns></returns>
		IEnumerable<TItem> GetInitialItems();

		/// <summary>
		/// Returns "neighbours" for the current item, which wave algorithm should process after the current item.
		/// It is safe to return items that have probably been processed by wave algorithm: 
		/// the wave algorithm guarantees that each item will be processed only once.
		/// </summary>
		/// <param name="currrentItem"></param>
		/// <returns></returns>
		IEnumerable<TItem> GetNextItems(TItem currrentItem);
	}
}
