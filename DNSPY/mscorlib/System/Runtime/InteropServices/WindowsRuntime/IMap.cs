using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A20 RID: 2592
	[Guid("3c2925fe-8519-45c1-aa79-197b6718c1c1")]
	[ComImport]
	internal interface IMap<K, V> : IIterable<IKeyValuePair<K, V>>, IEnumerable<IKeyValuePair<K, V>>, IEnumerable
	{
		// Token: 0x060065F9 RID: 26105
		V Lookup(K key);

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x060065FA RID: 26106
		uint Size { get; }

		// Token: 0x060065FB RID: 26107
		bool HasKey(K key);

		// Token: 0x060065FC RID: 26108
		IReadOnlyDictionary<K, V> GetView();

		// Token: 0x060065FD RID: 26109
		bool Insert(K key, V value);

		// Token: 0x060065FE RID: 26110
		void Remove(K key);

		// Token: 0x060065FF RID: 26111
		void Clear();
	}
}
