using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009ED RID: 2541
	[DebuggerDisplay("Size = {Size}")]
	internal sealed class IReadOnlyDictionaryToIMapViewAdapter
	{
		// Token: 0x060064AA RID: 25770 RVA: 0x00156C84 File Offset: 0x00154E84
		private IReadOnlyDictionaryToIMapViewAdapter()
		{
		}

		// Token: 0x060064AB RID: 25771 RVA: 0x00156C8C File Offset: 0x00154E8C
		[SecurityCritical]
		internal V Lookup<K, V>(K key)
		{
			IReadOnlyDictionary<K, V> readOnlyDictionary = JitHelpers.UnsafeCast<IReadOnlyDictionary<K, V>>(this);
			V v;
			if (!readOnlyDictionary.TryGetValue(key, out v))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			return v;
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x00156CCC File Offset: 0x00154ECC
		[SecurityCritical]
		internal uint Size<K, V>()
		{
			IReadOnlyDictionary<K, V> readOnlyDictionary = JitHelpers.UnsafeCast<IReadOnlyDictionary<K, V>>(this);
			return (uint)readOnlyDictionary.Count;
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x00156CE8 File Offset: 0x00154EE8
		[SecurityCritical]
		internal bool HasKey<K, V>(K key)
		{
			IReadOnlyDictionary<K, V> readOnlyDictionary = JitHelpers.UnsafeCast<IReadOnlyDictionary<K, V>>(this);
			return readOnlyDictionary.ContainsKey(key);
		}

		// Token: 0x060064AE RID: 25774 RVA: 0x00156D04 File Offset: 0x00154F04
		[SecurityCritical]
		internal void Split<K, V>(out IMapView<K, V> first, out IMapView<K, V> second)
		{
			IReadOnlyDictionary<K, V> readOnlyDictionary = JitHelpers.UnsafeCast<IReadOnlyDictionary<K, V>>(this);
			if (readOnlyDictionary.Count < 2)
			{
				first = null;
				second = null;
				return;
			}
			ConstantSplittableMap<K, V> constantSplittableMap = readOnlyDictionary as ConstantSplittableMap<K, V>;
			if (constantSplittableMap == null)
			{
				constantSplittableMap = new ConstantSplittableMap<K, V>(readOnlyDictionary);
			}
			constantSplittableMap.Split(out first, out second);
		}
	}
}
