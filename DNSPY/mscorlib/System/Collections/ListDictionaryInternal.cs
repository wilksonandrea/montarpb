using System;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000495 RID: 1173
	[Serializable]
	internal class ListDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003841 RID: 14401 RVA: 0x000D7C28 File Offset: 0x000D5E28
		public ListDictionaryInternal()
		{
		}

		// Token: 0x1700084F RID: 2127
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
				{
					if (next.key.Equals(key))
					{
						return next.value;
					}
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
				this.version++;
				ListDictionaryInternal.DictionaryNode dictionaryNode = null;
				ListDictionaryInternal.DictionaryNode next = this.head;
				while (next != null && !next.key.Equals(key))
				{
					dictionaryNode = next;
					next = next.next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000D7D5F File Offset: 0x000D5F5F
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x000D7D67 File Offset: 0x000D5F67
		public ICollection Keys
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, true);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x000D7D70 File Offset: 0x000D5F70
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06003847 RID: 14407 RVA: 0x000D7D73 File Offset: 0x000D5F73
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000D7D76 File Offset: 0x000D5F76
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06003849 RID: 14409 RVA: 0x000D7D79 File Offset: 0x000D5F79
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000D7D9B File Offset: 0x000D5F9B
		public ICollection Values
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, false);
			}
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000D7DA4 File Offset: 0x000D5FA4
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
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", new object[] { next.key, key }));
				}
				dictionaryNode = next;
			}
			if (next != null)
			{
				next.value = value;
				return;
			}
			ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000D7EA6 File Offset: 0x000D60A6
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000D7EC4 File Offset: 0x000D60C4
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000D7F10 File Offset: 0x000D6110
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
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000D7FB7 File Offset: 0x000D61B7
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000D7FBF File Offset: 0x000D61BF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000D7FC8 File Offset: 0x000D61C8
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next = this.head;
			while (next != null && !next.key.Equals(key))
			{
				dictionaryNode = next;
				next = next.next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x040018DE RID: 6366
		private ListDictionaryInternal.DictionaryNode head;

		// Token: 0x040018DF RID: 6367
		private int version;

		// Token: 0x040018E0 RID: 6368
		private int count;

		// Token: 0x040018E1 RID: 6369
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000BB5 RID: 2997
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006DE6 RID: 28134 RVA: 0x0017B77C File Offset: 0x0017997C
			public NodeEnumerator(ListDictionaryInternal list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x170012A5 RID: 4773
			// (get) Token: 0x06006DE7 RID: 28135 RVA: 0x0017B7A5 File Offset: 0x001799A5
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x170012A6 RID: 4774
			// (get) Token: 0x06006DE8 RID: 28136 RVA: 0x0017B7B2 File Offset: 0x001799B2
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x170012A7 RID: 4775
			// (get) Token: 0x06006DE9 RID: 28137 RVA: 0x0017B7E7 File Offset: 0x001799E7
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.key;
				}
			}

			// Token: 0x170012A8 RID: 4776
			// (get) Token: 0x06006DEA RID: 28138 RVA: 0x0017B80C File Offset: 0x00179A0C
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.value;
				}
			}

			// Token: 0x06006DEB RID: 28139 RVA: 0x0017B834 File Offset: 0x00179A34
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x06006DEC RID: 28140 RVA: 0x0017B8A8 File Offset: 0x00179AA8
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x0400356F RID: 13679
			private ListDictionaryInternal list;

			// Token: 0x04003570 RID: 13680
			private ListDictionaryInternal.DictionaryNode current;

			// Token: 0x04003571 RID: 13681
			private int version;

			// Token: 0x04003572 RID: 13682
			private bool start;
		}

		// Token: 0x02000BB6 RID: 2998
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06006DED RID: 28141 RVA: 0x0017B8DB File Offset: 0x00179ADB
			public NodeKeyValueCollection(ListDictionaryInternal list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x06006DEE RID: 28142 RVA: 0x0017B8F4 File Offset: 0x00179AF4
			void ICollection.CopyTo(Array array, int index)
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
				if (array.Length - index < this.list.Count)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"), "index");
				}
				for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x170012A9 RID: 4777
			// (get) Token: 0x06006DEF RID: 28143 RVA: 0x0017B9A8 File Offset: 0x00179BA8
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170012AA RID: 4778
			// (get) Token: 0x06006DF0 RID: 28144 RVA: 0x0017B9D4 File Offset: 0x00179BD4
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170012AB RID: 4779
			// (get) Token: 0x06006DF1 RID: 28145 RVA: 0x0017B9D7 File Offset: 0x00179BD7
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x06006DF2 RID: 28146 RVA: 0x0017B9E4 File Offset: 0x00179BE4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionaryInternal.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x04003573 RID: 13683
			private ListDictionaryInternal list;

			// Token: 0x04003574 RID: 13684
			private bool isKeys;

			// Token: 0x02000D08 RID: 3336
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x06007200 RID: 29184 RVA: 0x00188A0B File Offset: 0x00186C0B
				public NodeKeyValueEnumerator(ListDictionaryInternal list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x17001385 RID: 4997
				// (get) Token: 0x06007201 RID: 29185 RVA: 0x00188A3B File Offset: 0x00186C3B
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x06007202 RID: 29186 RVA: 0x00188A74 File Offset: 0x00186C74
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x06007203 RID: 29187 RVA: 0x00188AE8 File Offset: 0x00186CE8
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04003945 RID: 14661
				private ListDictionaryInternal list;

				// Token: 0x04003946 RID: 14662
				private ListDictionaryInternal.DictionaryNode current;

				// Token: 0x04003947 RID: 14663
				private int version;

				// Token: 0x04003948 RID: 14664
				private bool isKeys;

				// Token: 0x04003949 RID: 14665
				private bool start;
			}
		}

		// Token: 0x02000BB7 RID: 2999
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x06006DF3 RID: 28147 RVA: 0x0017B9F7 File Offset: 0x00179BF7
			public DictionaryNode()
			{
			}

			// Token: 0x04003575 RID: 13685
			public object key;

			// Token: 0x04003576 RID: 13686
			public object value;

			// Token: 0x04003577 RID: 13687
			public ListDictionaryInternal.DictionaryNode next;
		}
	}
}
