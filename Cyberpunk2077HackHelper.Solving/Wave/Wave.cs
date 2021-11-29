using System.Linq;
using System.Collections.Generic;

namespace Cyberpunk2077HackHelper.Solving.Wave
{
	/// <summary>
	/// A class used to describe wave algorithm in genegal regardless of items' type and expected result
	/// </summary>
	/// <typeparam name="TItem">type of item</typeparam>
	/// <typeparam name="TResult">type of result element</typeparam>
	public class Wave<TItem, TResult>
	{
		private readonly IWaveItemsEnumerator<TItem> _enumerator;
		private readonly IWaveItemProcessor<TItem, TResult> _processor;

		public Wave(IWaveItemsEnumerator<TItem> enumerator, IWaveItemProcessor<TItem, TResult> processor)
		{
			_enumerator = enumerator;
			_processor = processor;
		}

		/// <summary>
		/// Runs wave algirithm. 
		/// Initial items returned by the WaveItemsEnumerator are pushed to the queue. 
		/// Each item popped from the queue is processed by the WaveItemProcessor. The result successful processing is yield-returned.
		/// For each successfully processed item WaveItemsEnumerator returns next items to push into the queue.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<TResult> Run()
		{
			HashSet<TItem> visitedItems = new HashSet<TItem>();
			Queue<TItem> queue = new Queue<TItem>();

			// Enumerable.Exclude is not used because it works slower than HashSet
			foreach (TItem initialItem in _enumerator.GetInitialItems().Where(newItem => !visitedItems.Contains(newItem)))
			{
				visitedItems.Add(initialItem);
				queue.Enqueue(initialItem);
			}

			while (queue.Count > 0)
			{
				TItem item = queue.Dequeue();

				if (_processor.TryProcessItem(item, out TResult result))
				{
					yield return result;

					foreach (TItem newItem in _enumerator.GetNextItems(item).Where(newItem => !visitedItems.Contains(newItem)))
					{
						visitedItems.Add(newItem);
						queue.Enqueue(newItem);
					}
				}
			}
		}
	}
}
