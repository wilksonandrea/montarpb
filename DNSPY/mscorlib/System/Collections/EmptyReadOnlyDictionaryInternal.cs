using System;

namespace System.Collections
{
	// Token: 0x02000496 RID: 1174
	[Serializable]
	internal sealed class EmptyReadOnlyDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003852 RID: 14418 RVA: 0x000D8055 File Offset: 0x000D6255
		public EmptyReadOnlyDictionaryInternal()
		{
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000D805D File Offset: 0x000D625D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000D8064 File Offset: 0x000D6264
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"), "index");
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x000D80D6 File Offset: 0x000D62D6
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000D80D9 File Offset: 0x000D62D9
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06003857 RID: 14423 RVA: 0x000D80DC File Offset: 0x000D62DC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085A RID: 2138
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				if (!key.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "key");
				}
				if (value != null && !value.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "value");
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000D8177 File Offset: 0x000D6377
		public ICollection Keys
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x000D817E File Offset: 0x000D637E
		public ICollection Values
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000D8185 File Offset: 0x000D6385
		public bool Contains(object key)
		{
			return false;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000D8188 File Offset: 0x000D6388
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			if (!key.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "key");
			}
			if (value != null && !value.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "value");
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000D8203 File Offset: 0x000D6403
		public void Clear()
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000D8214 File Offset: 0x000D6414
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000D8217 File Offset: 0x000D6417
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000D821A File Offset: 0x000D641A
		public IDictionaryEnumerator GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000D8221 File Offset: 0x000D6421
		public void Remove(object key)
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
		}

		// Token: 0x02000BB8 RID: 3000
		private sealed class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006DF4 RID: 28148 RVA: 0x0017B9FF File Offset: 0x00179BFF
			public NodeEnumerator()
			{
			}

			// Token: 0x06006DF5 RID: 28149 RVA: 0x0017BA07 File Offset: 0x00179C07
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x170012AC RID: 4780
			// (get) Token: 0x06006DF6 RID: 28150 RVA: 0x0017BA0A File Offset: 0x00179C0A
			public object Current
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x06006DF7 RID: 28151 RVA: 0x0017BA1B File Offset: 0x00179C1B
			public void Reset()
			{
			}

			// Token: 0x170012AD RID: 4781
			// (get) Token: 0x06006DF8 RID: 28152 RVA: 0x0017BA1D File Offset: 0x00179C1D
			public object Key
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x170012AE RID: 4782
			// (get) Token: 0x06006DF9 RID: 28153 RVA: 0x0017BA2E File Offset: 0x00179C2E
			public object Value
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x170012AF RID: 4783
			// (get) Token: 0x06006DFA RID: 28154 RVA: 0x0017BA3F File Offset: 0x00179C3F
			public DictionaryEntry Entry
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}
		}
	}
}
