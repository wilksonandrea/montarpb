using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DD RID: 2525
	internal sealed class DictionaryToMapAdapter
	{
		// Token: 0x06006447 RID: 25671 RVA: 0x00155C33 File Offset: 0x00153E33
		private DictionaryToMapAdapter()
		{
		}

		// Token: 0x06006448 RID: 25672 RVA: 0x00155C3C File Offset: 0x00153E3C
		[SecurityCritical]
		internal V Lookup<K, V>(K key)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			V v;
			if (!dictionary.TryGetValue(key, out v))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			return v;
		}

		// Token: 0x06006449 RID: 25673 RVA: 0x00155C7C File Offset: 0x00153E7C
		[SecurityCritical]
		internal uint Size<K, V>()
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			return (uint)dictionary.Count;
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x00155C98 File Offset: 0x00153E98
		[SecurityCritical]
		internal bool HasKey<K, V>(K key)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			return dictionary.ContainsKey(key);
		}

		// Token: 0x0600644B RID: 25675 RVA: 0x00155CB4 File Offset: 0x00153EB4
		[SecurityCritical]
		internal IReadOnlyDictionary<K, V> GetView<K, V>()
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			IReadOnlyDictionary<K, V> readOnlyDictionary = dictionary as IReadOnlyDictionary<K, V>;
			if (readOnlyDictionary == null)
			{
				readOnlyDictionary = new ReadOnlyDictionary<K, V>(dictionary);
			}
			return readOnlyDictionary;
		}

		// Token: 0x0600644C RID: 25676 RVA: 0x00155CDC File Offset: 0x00153EDC
		[SecurityCritical]
		internal bool Insert<K, V>(K key, V value)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			bool flag = dictionary.ContainsKey(key);
			dictionary[key] = value;
			return flag;
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x00155D04 File Offset: 0x00153F04
		[SecurityCritical]
		internal void Remove<K, V>(K key)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			if (!dictionary.Remove(key))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x00155D40 File Offset: 0x00153F40
		[SecurityCritical]
		internal void Clear<K, V>()
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			dictionary.Clear();
		}
	}
}
