using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CE RID: 2510
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class ConstantSplittableMap<TKey, TValue> : IMapView<TKey, TValue>, IIterable<IKeyValuePair<TKey, TValue>>, IEnumerable<IKeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x060063D2 RID: 25554 RVA: 0x00154770 File Offset: 0x00152970
		internal ConstantSplittableMap(IReadOnlyDictionary<TKey, TValue> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.firstItemIndex = 0;
			this.lastItemIndex = data.Count - 1;
			this.items = this.CreateKeyValueArray(data.Count, data.GetEnumerator());
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x001547C0 File Offset: 0x001529C0
		internal ConstantSplittableMap(IMapView<TKey, TValue> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (2147483647U < data.Size)
			{
				Exception ex = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			int size = (int)data.Size;
			this.firstItemIndex = 0;
			this.lastItemIndex = size - 1;
			this.items = this.CreateKeyValueArray(size, data.GetEnumerator());
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x00154835 File Offset: 0x00152A35
		private ConstantSplittableMap(KeyValuePair<TKey, TValue>[] items, int firstItemIndex, int lastItemIndex)
		{
			this.items = items;
			this.firstItemIndex = firstItemIndex;
			this.lastItemIndex = lastItemIndex;
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x00154854 File Offset: 0x00152A54
		private KeyValuePair<TKey, TValue>[] CreateKeyValueArray(int count, IEnumerator<KeyValuePair<TKey, TValue>> data)
		{
			KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[count];
			int num = 0;
			while (data.MoveNext())
			{
				KeyValuePair<TKey, TValue> keyValuePair = data.Current;
				array[num++] = keyValuePair;
			}
			Array.Sort<KeyValuePair<TKey, TValue>>(array, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			return array;
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x00154894 File Offset: 0x00152A94
		private KeyValuePair<TKey, TValue>[] CreateKeyValueArray(int count, IEnumerator<IKeyValuePair<TKey, TValue>> data)
		{
			KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[count];
			int num = 0;
			while (data.MoveNext())
			{
				IKeyValuePair<TKey, TValue> keyValuePair = data.Current;
				array[num++] = new KeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value);
			}
			Array.Sort<KeyValuePair<TKey, TValue>>(array, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			return array;
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x060063D7 RID: 25559 RVA: 0x001548E3 File Offset: 0x00152AE3
		public int Count
		{
			get
			{
				return this.lastItemIndex - this.firstItemIndex + 1;
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x060063D8 RID: 25560 RVA: 0x001548F4 File Offset: 0x00152AF4
		public uint Size
		{
			get
			{
				return (uint)(this.lastItemIndex - this.firstItemIndex + 1);
			}
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x00154908 File Offset: 0x00152B08
		public TValue Lookup(TKey key)
		{
			TValue tvalue;
			if (!this.TryGetValue(key, out tvalue))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			return tvalue;
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x00154940 File Offset: 0x00152B40
		public bool HasKey(TKey key)
		{
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x00154958 File Offset: 0x00152B58
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<IKeyValuePair<TKey, TValue>>)this).GetEnumerator();
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x00154960 File Offset: 0x00152B60
		public IIterator<IKeyValuePair<TKey, TValue>> First()
		{
			return new EnumeratorToIteratorAdapter<IKeyValuePair<TKey, TValue>>(this.GetEnumerator());
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x0015496D File Offset: 0x00152B6D
		public IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new ConstantSplittableMap<TKey, TValue>.IKeyValuePairEnumerator(this.items, this.firstItemIndex, this.lastItemIndex);
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x0015498C File Offset: 0x00152B8C
		public void Split(out IMapView<TKey, TValue> firstPartition, out IMapView<TKey, TValue> secondPartition)
		{
			if (this.Count < 2)
			{
				firstPartition = null;
				secondPartition = null;
				return;
			}
			int num = (int)(((long)this.firstItemIndex + (long)this.lastItemIndex) / 2L);
			firstPartition = new ConstantSplittableMap<TKey, TValue>(this.items, this.firstItemIndex, num);
			secondPartition = new ConstantSplittableMap<TKey, TValue>(this.items, num + 1, this.lastItemIndex);
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x001549E8 File Offset: 0x00152BE8
		public bool ContainsKey(TKey key)
		{
			KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
			int num = Array.BinarySearch<KeyValuePair<TKey, TValue>>(this.items, this.firstItemIndex, this.Count, keyValuePair, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			return num >= 0;
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x00154A2C File Offset: 0x00152C2C
		public bool TryGetValue(TKey key, out TValue value)
		{
			KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
			int num = Array.BinarySearch<KeyValuePair<TKey, TValue>>(this.items, this.firstItemIndex, this.Count, keyValuePair, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			if (num < 0)
			{
				value = default(TValue);
				return false;
			}
			value = this.items[num].Value;
			return true;
		}

		// Token: 0x17001142 RID: 4418
		public TValue this[TKey key]
		{
			get
			{
				return this.Lookup(key);
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060063E2 RID: 25570 RVA: 0x00154A96 File Offset: 0x00152C96
		public IEnumerable<TKey> Keys
		{
			get
			{
				throw new NotImplementedException("NYI");
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060063E3 RID: 25571 RVA: 0x00154AA2 File Offset: 0x00152CA2
		public IEnumerable<TValue> Values
		{
			get
			{
				throw new NotImplementedException("NYI");
			}
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x00154AAE File Offset: 0x00152CAE
		// Note: this type is marked as 'beforefieldinit'.
		static ConstantSplittableMap()
		{
		}

		// Token: 0x04002CE9 RID: 11497
		private static readonly ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator keyValuePairComparator = new ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator();

		// Token: 0x04002CEA RID: 11498
		private readonly KeyValuePair<TKey, TValue>[] items;

		// Token: 0x04002CEB RID: 11499
		private readonly int firstItemIndex;

		// Token: 0x04002CEC RID: 11500
		private readonly int lastItemIndex;

		// Token: 0x02000CA3 RID: 3235
		private class KeyValuePairComparator : IComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600712F RID: 28975 RVA: 0x0018552D File Offset: 0x0018372D
			public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator.keyComparator.Compare(x.Key, y.Key);
			}

			// Token: 0x06007130 RID: 28976 RVA: 0x00185547 File Offset: 0x00183747
			public KeyValuePairComparator()
			{
			}

			// Token: 0x06007131 RID: 28977 RVA: 0x0018554F File Offset: 0x0018374F
			// Note: this type is marked as 'beforefieldinit'.
			static KeyValuePairComparator()
			{
			}

			// Token: 0x04003886 RID: 14470
			private static readonly IComparer<TKey> keyComparator = Comparer<TKey>.Default;
		}

		// Token: 0x02000CA4 RID: 3236
		[Serializable]
		internal struct IKeyValuePairEnumerator : IEnumerator<IKeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x06007132 RID: 28978 RVA: 0x0018555B File Offset: 0x0018375B
			internal IKeyValuePairEnumerator(KeyValuePair<TKey, TValue>[] items, int first, int end)
			{
				this._array = items;
				this._start = first;
				this._end = end;
				this._current = this._start - 1;
			}

			// Token: 0x06007133 RID: 28979 RVA: 0x00185580 File Offset: 0x00183780
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return true;
				}
				return false;
			}

			// Token: 0x17001367 RID: 4967
			// (get) Token: 0x06007134 RID: 28980 RVA: 0x001855A4 File Offset: 0x001837A4
			public IKeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (this._current < this._start)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._current > this._end)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return new CLRIKeyValuePairImpl<TKey, TValue>(ref this._array[this._current]);
				}
			}

			// Token: 0x17001368 RID: 4968
			// (get) Token: 0x06007135 RID: 28981 RVA: 0x00185603 File Offset: 0x00183803
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06007136 RID: 28982 RVA: 0x0018560B File Offset: 0x0018380B
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x06007137 RID: 28983 RVA: 0x0018561B File Offset: 0x0018381B
			public void Dispose()
			{
			}

			// Token: 0x04003887 RID: 14471
			private KeyValuePair<TKey, TValue>[] _array;

			// Token: 0x04003888 RID: 14472
			private int _start;

			// Token: 0x04003889 RID: 14473
			private int _end;

			// Token: 0x0400388A RID: 14474
			private int _current;
		}
	}
}
