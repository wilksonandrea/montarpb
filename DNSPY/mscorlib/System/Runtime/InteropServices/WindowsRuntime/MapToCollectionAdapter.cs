using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DA RID: 2522
	internal sealed class MapToCollectionAdapter
	{
		// Token: 0x0600642E RID: 25646 RVA: 0x00155613 File Offset: 0x00153813
		private MapToCollectionAdapter()
		{
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x0015561C File Offset: 0x0015381C
		[SecurityCritical]
		internal int Count<K, V>()
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IMap<K, V> map = obj as IMap<K, V>;
			if (map != null)
			{
				uint size = map.Size;
				if (2147483647U < size)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
				}
				return (int)size;
			}
			else
			{
				IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
				uint size2 = vector.Size;
				if (2147483647U < size2)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
				}
				return (int)size2;
			}
		}

		// Token: 0x06006430 RID: 25648 RVA: 0x00155685 File Offset: 0x00153885
		[SecurityCritical]
		internal bool IsReadOnly<K, V>()
		{
			return false;
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x00155688 File Offset: 0x00153888
		[SecurityCritical]
		internal void Add<K, V>(KeyValuePair<K, V> item)
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IDictionary<K, V> dictionary = obj as IDictionary<K, V>;
			if (dictionary != null)
			{
				dictionary.Add(item.Key, item.Value);
				return;
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			vector.Append(item);
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x001556CC File Offset: 0x001538CC
		[SecurityCritical]
		internal void Clear<K, V>()
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IMap<K, V> map = obj as IMap<K, V>;
			if (map != null)
			{
				map.Clear();
				return;
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			vector.Clear();
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x00155700 File Offset: 0x00153900
		[SecurityCritical]
		internal bool Contains<K, V>(KeyValuePair<K, V> item)
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IDictionary<K, V> dictionary = obj as IDictionary<K, V>;
			if (dictionary != null)
			{
				V v;
				return dictionary.TryGetValue(item.Key, out v) && EqualityComparer<V>.Default.Equals(v, item.Value);
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			uint num;
			return vector.IndexOf(item, out num);
		}

		// Token: 0x06006434 RID: 25652 RVA: 0x00155758 File Offset: 0x00153958
		[SecurityCritical]
		internal void CopyTo<K, V>(KeyValuePair<K, V>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length <= arrayIndex && this.Count<K, V>() > 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
			}
			if (array.Length - arrayIndex < this.Count<K, V>())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			IIterable<KeyValuePair<K, V>> iterable = JitHelpers.UnsafeCast<IIterable<KeyValuePair<K, V>>>(this);
			foreach (KeyValuePair<K, V> keyValuePair in iterable)
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x00155808 File Offset: 0x00153A08
		[SecurityCritical]
		internal bool Remove<K, V>(KeyValuePair<K, V> item)
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IDictionary<K, V> dictionary = obj as IDictionary<K, V>;
			if (dictionary != null)
			{
				return dictionary.Remove(item.Key);
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			uint num;
			if (!vector.IndexOf(item, out num))
			{
				return false;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			VectorToListAdapter.RemoveAtHelper<KeyValuePair<K, V>>(vector, num);
			return true;
		}
	}
}
