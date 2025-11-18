using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DB RID: 2523
	internal sealed class MapViewToReadOnlyCollectionAdapter
	{
		// Token: 0x06006436 RID: 25654 RVA: 0x0015586C File Offset: 0x00153A6C
		private MapViewToReadOnlyCollectionAdapter()
		{
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x00155874 File Offset: 0x00153A74
		[SecurityCritical]
		internal int Count<K, V>()
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IMapView<K, V> mapView = obj as IMapView<K, V>;
			if (mapView != null)
			{
				uint size = mapView.Size;
				if (2147483647U < size)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
				}
				return (int)size;
			}
			else
			{
				IVectorView<KeyValuePair<K, V>> vectorView = JitHelpers.UnsafeCast<IVectorView<KeyValuePair<K, V>>>(this);
				uint size2 = vectorView.Size;
				if (2147483647U < size2)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
				}
				return (int)size2;
			}
		}
	}
}
