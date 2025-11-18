using System;

namespace System.Collections.Generic
{
	// Token: 0x020004DD RID: 1245
	internal interface IArraySortHelper<TKey>
	{
		// Token: 0x06003B38 RID: 15160
		void Sort(TKey[] keys, int index, int length, IComparer<TKey> comparer);

		// Token: 0x06003B39 RID: 15161
		int BinarySearch(TKey[] keys, int index, int length, TKey value, IComparer<TKey> comparer);
	}
}
