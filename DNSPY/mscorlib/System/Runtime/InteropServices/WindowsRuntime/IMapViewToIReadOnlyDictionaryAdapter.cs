using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E6 RID: 2534
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class IMapViewToIReadOnlyDictionaryAdapter
	{
		// Token: 0x0600648A RID: 25738 RVA: 0x001568EA File Offset: 0x00154AEA
		private IMapViewToIReadOnlyDictionaryAdapter()
		{
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x001568F4 File Offset: 0x00154AF4
		[SecurityCritical]
		internal V Indexer_Get<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			return IMapViewToIReadOnlyDictionaryAdapter.Lookup<K, V>(mapView, key);
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x00156924 File Offset: 0x00154B24
		[SecurityCritical]
		internal IEnumerable<K> Keys<K, V>()
		{
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			IReadOnlyDictionary<K, V> readOnlyDictionary = (IReadOnlyDictionary<K, V>)mapView;
			return new ReadOnlyDictionaryKeyCollection<K, V>(readOnlyDictionary);
		}

		// Token: 0x0600648D RID: 25741 RVA: 0x00156948 File Offset: 0x00154B48
		[SecurityCritical]
		internal IEnumerable<V> Values<K, V>()
		{
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			IReadOnlyDictionary<K, V> readOnlyDictionary = (IReadOnlyDictionary<K, V>)mapView;
			return new ReadOnlyDictionaryValueCollection<K, V>(readOnlyDictionary);
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x0015696C File Offset: 0x00154B6C
		[SecurityCritical]
		internal bool ContainsKey<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			return mapView.HasKey(key);
		}

		// Token: 0x0600648F RID: 25743 RVA: 0x0015699C File Offset: 0x00154B9C
		[SecurityCritical]
		internal bool TryGetValue<K, V>(K key, out V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			if (!mapView.HasKey(key))
			{
				value = default(V);
				return false;
			}
			bool flag;
			try
			{
				value = mapView.Lookup(key);
				flag = true;
			}
			catch (Exception ex)
			{
				if (-2147483637 != ex._HResult)
				{
					throw;
				}
				value = default(V);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x00156A14 File Offset: 0x00154C14
		private static V Lookup<K, V>(IMapView<K, V> _this, K key)
		{
			V v;
			try
			{
				v = _this.Lookup(key);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				}
				throw;
			}
			return v;
		}
	}
}
