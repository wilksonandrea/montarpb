using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A21 RID: 2593
	[Guid("e480ce40-a338-4ada-adcf-272272e48cb9")]
	[ComImport]
	internal interface IMapView<K, V> : IIterable<IKeyValuePair<K, V>>, IEnumerable<IKeyValuePair<K, V>>, IEnumerable
	{
		// Token: 0x06006600 RID: 26112
		V Lookup(K key);

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06006601 RID: 26113
		uint Size { get; }

		// Token: 0x06006602 RID: 26114
		bool HasKey(K key);

		// Token: 0x06006603 RID: 26115
		void Split(out IMapView<K, V> first, out IMapView<K, V> second);
	}
}
