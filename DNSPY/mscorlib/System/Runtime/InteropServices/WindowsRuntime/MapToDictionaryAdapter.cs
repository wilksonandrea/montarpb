using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D9 RID: 2521
	internal sealed class MapToDictionaryAdapter
	{
		// Token: 0x06006423 RID: 25635 RVA: 0x001553BC File Offset: 0x001535BC
		private MapToDictionaryAdapter()
		{
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x001553C4 File Offset: 0x001535C4
		[SecurityCritical]
		internal V Indexer_Get<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			return MapToDictionaryAdapter.Lookup<K, V>(map, key);
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x001553F4 File Offset: 0x001535F4
		[SecurityCritical]
		internal void Indexer_Set<K, V>(K key, V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			MapToDictionaryAdapter.Insert<K, V>(map, key, value);
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x00155424 File Offset: 0x00153624
		[SecurityCritical]
		internal ICollection<K> Keys<K, V>()
		{
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			IDictionary<K, V> dictionary = (IDictionary<K, V>)map;
			return new DictionaryKeyCollection<K, V>(dictionary);
		}

		// Token: 0x06006427 RID: 25639 RVA: 0x00155448 File Offset: 0x00153648
		[SecurityCritical]
		internal ICollection<V> Values<K, V>()
		{
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			IDictionary<K, V> dictionary = (IDictionary<K, V>)map;
			return new DictionaryValueCollection<K, V>(dictionary);
		}

		// Token: 0x06006428 RID: 25640 RVA: 0x0015546C File Offset: 0x0015366C
		[SecurityCritical]
		internal bool ContainsKey<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			return map.HasKey(key);
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x0015549C File Offset: 0x0015369C
		[SecurityCritical]
		internal void Add<K, V>(K key, V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.ContainsKey<K, V>(key))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate"));
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			MapToDictionaryAdapter.Insert<K, V>(map, key, value);
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x001554E8 File Offset: 0x001536E8
		[SecurityCritical]
		internal bool Remove<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			if (!map.HasKey(key))
			{
				return false;
			}
			bool flag;
			try
			{
				map.Remove(key);
				flag = true;
			}
			catch (Exception ex)
			{
				if (-2147483637 != ex._HResult)
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x0015554C File Offset: 0x0015374C
		[SecurityCritical]
		internal bool TryGetValue<K, V>(K key, out V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			if (!map.HasKey(key))
			{
				value = default(V);
				return false;
			}
			bool flag;
			try
			{
				value = MapToDictionaryAdapter.Lookup<K, V>(map, key);
				flag = true;
			}
			catch (KeyNotFoundException)
			{
				value = default(V);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x001555B4 File Offset: 0x001537B4
		private static V Lookup<K, V>(IMap<K, V> _this, K key)
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

		// Token: 0x0600642D RID: 25645 RVA: 0x001555FC File Offset: 0x001537FC
		private static bool Insert<K, V>(IMap<K, V> _this, K key, V value)
		{
			return _this.Insert(key, value);
		}
	}
}
