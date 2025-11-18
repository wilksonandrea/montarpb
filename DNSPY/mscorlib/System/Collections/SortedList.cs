using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	// Token: 0x020004A4 RID: 1188
	[DebuggerTypeProxy(typeof(SortedList.SortedListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class SortedList : IDictionary, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x060038D3 RID: 14547 RVA: 0x000D9A8B File Offset: 0x000D7C8B
		public SortedList()
		{
			this.Init();
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000D9A99 File Offset: 0x000D7C99
		private void Init()
		{
			this.keys = SortedList.emptyArray;
			this.values = SortedList.emptyArray;
			this._size = 0;
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000D9AC8 File Offset: 0x000D7CC8
		public SortedList(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.keys = new object[initialCapacity];
			this.values = new object[initialCapacity];
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000D9B1C File Offset: 0x000D7D1C
		public SortedList(IComparer comparer)
			: this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000D9B2E File Offset: 0x000D7D2E
		public SortedList(IComparer comparer, int capacity)
			: this(comparer)
		{
			this.Capacity = capacity;
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000D9B3E File Offset: 0x000D7D3E
		public SortedList(IDictionary d)
			: this(d, null)
		{
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000D9B48 File Offset: 0x000D7D48
		public SortedList(IDictionary d, IComparer comparer)
			: this(comparer, (d != null) ? d.Count : 0)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", Environment.GetResourceString("ArgumentNull_Dictionary"));
			}
			d.Keys.CopyTo(this.keys, 0);
			d.Values.CopyTo(this.values, 0);
			Array.Sort(this.keys, this.values, comparer);
			this._size = d.Count;
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000D9BC4 File Offset: 0x000D7DC4
		public virtual void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", new object[]
				{
					this.GetKey(num),
					key
				}));
			}
			this.Insert(~num, key, value);
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000D9C35 File Offset: 0x000D7E35
		// (set) Token: 0x060038DC RID: 14556 RVA: 0x000D9C40 File Offset: 0x000D7E40
		public virtual int Capacity
		{
			get
			{
				return this.keys.Length;
			}
			set
			{
				if (value < this.Count)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
				}
				if (value != this.keys.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						object[] array2 = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, array, 0, this._size);
							Array.Copy(this.values, 0, array2, 0, this._size);
						}
						this.keys = array;
						this.values = array2;
						return;
					}
					this.keys = SortedList.emptyArray;
					this.values = SortedList.emptyArray;
				}
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000D9CDE File Offset: 0x000D7EDE
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000D9CE6 File Offset: 0x000D7EE6
		public virtual ICollection Keys
		{
			get
			{
				return this.GetKeyList();
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000D9CEE File Offset: 0x000D7EEE
		public virtual ICollection Values
		{
			get
			{
				return this.GetValueList();
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000D9CF6 File Offset: 0x000D7EF6
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000D9CF9 File Offset: 0x000D7EF9
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x000D9CFC File Offset: 0x000D7EFC
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000D9CFF File Offset: 0x000D7EFF
		public virtual object SyncRoot
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

		// Token: 0x060038E4 RID: 14564 RVA: 0x000D9D21 File Offset: 0x000D7F21
		public virtual void Clear()
		{
			this.version++;
			Array.Clear(this.keys, 0, this._size);
			Array.Clear(this.values, 0, this._size);
			this._size = 0;
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000D9D5C File Offset: 0x000D7F5C
		public virtual object Clone()
		{
			SortedList sortedList = new SortedList(this._size);
			Array.Copy(this.keys, 0, sortedList.keys, 0, this._size);
			Array.Copy(this.values, 0, sortedList.values, 0, this._size);
			sortedList._size = this._size;
			sortedList.version = this.version;
			sortedList.comparer = this.comparer;
			return sortedList;
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000D9DCC File Offset: 0x000D7FCC
		public virtual bool Contains(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000D9DDB File Offset: 0x000D7FDB
		public virtual bool ContainsKey(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000D9DEA File Offset: 0x000D7FEA
		public virtual bool ContainsValue(object value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000D9DFC File Offset: 0x000D7FFC
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[i], this.values[i]);
				array.SetValue(dictionaryEntry, i + arrayIndex);
			}
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000D9EAC File Offset: 0x000D80AC
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = new KeyValuePairs(this.keys[i], this.values[i]);
			}
			return array;
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000D9EF0 File Offset: 0x000D80F0
		private void EnsureCapacity(int min)
		{
			int num = ((this.keys.Length == 0) ? 16 : (this.keys.Length * 2));
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000D9F30 File Offset: 0x000D8130
		public virtual object GetByIndex(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return this.values[index];
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000D9F5C File Offset: 0x000D815C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000D9F6C File Offset: 0x000D816C
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000D9F7C File Offset: 0x000D817C
		public virtual object GetKey(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return this.keys[index];
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000D9FA8 File Offset: 0x000D81A8
		public virtual IList GetKeyList()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList.KeyList(this);
			}
			return this.keyList;
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000D9FC4 File Offset: 0x000D81C4
		public virtual IList GetValueList()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList.ValueList(this);
			}
			return this.valueList;
		}

		// Token: 0x17000886 RID: 2182
		public virtual object this[object key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000DA06C File Offset: 0x000D826C
		public virtual int IndexOfKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000DA0B2 File Offset: 0x000D82B2
		public virtual int IndexOfValue(object value)
		{
			return Array.IndexOf<object>(this.values, value, 0, this._size);
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000DA0C8 File Offset: 0x000D82C8
		private void Insert(int index, object key, object value)
		{
			if (this._size == this.keys.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
				Array.Copy(this.values, index, this.values, index + 1, this._size - index);
			}
			this.keys[index] = key;
			this.values[index] = value;
			this._size++;
			this.version++;
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x000DA164 File Offset: 0x000D8364
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			this.keys[this._size] = null;
			this.values[this._size] = null;
			this.version++;
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000DA210 File Offset: 0x000D8410
		public virtual void Remove(object key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000DA230 File Offset: 0x000D8430
		public virtual void SetByIndex(int index, object value)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this.values[index] = value;
			this.version++;
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000DA26B File Offset: 0x000D846B
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static SortedList Synchronized(SortedList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new SortedList.SyncSortedList(list);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000DA281 File Offset: 0x000D8481
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000DA28F File Offset: 0x000D848F
		// Note: this type is marked as 'beforefieldinit'.
		static SortedList()
		{
		}

		// Token: 0x04001905 RID: 6405
		private object[] keys;

		// Token: 0x04001906 RID: 6406
		private object[] values;

		// Token: 0x04001907 RID: 6407
		private int _size;

		// Token: 0x04001908 RID: 6408
		private int version;

		// Token: 0x04001909 RID: 6409
		private IComparer comparer;

		// Token: 0x0400190A RID: 6410
		private SortedList.KeyList keyList;

		// Token: 0x0400190B RID: 6411
		private SortedList.ValueList valueList;

		// Token: 0x0400190C RID: 6412
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x0400190D RID: 6413
		private const int _defaultCapacity = 16;

		// Token: 0x0400190E RID: 6414
		private static object[] emptyArray = EmptyArray<object>.Value;

		// Token: 0x02000BBF RID: 3007
		[Serializable]
		private class SyncSortedList : SortedList
		{
			// Token: 0x06006E29 RID: 28201 RVA: 0x0017C240 File Offset: 0x0017A440
			internal SyncSortedList(SortedList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x170012C3 RID: 4803
			// (get) Token: 0x06006E2A RID: 28202 RVA: 0x0017C25C File Offset: 0x0017A45C
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x170012C4 RID: 4804
			// (get) Token: 0x06006E2B RID: 28203 RVA: 0x0017C2A4 File Offset: 0x0017A4A4
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170012C5 RID: 4805
			// (get) Token: 0x06006E2C RID: 28204 RVA: 0x0017C2AC File Offset: 0x0017A4AC
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x170012C6 RID: 4806
			// (get) Token: 0x06006E2D RID: 28205 RVA: 0x0017C2B9 File Offset: 0x0017A4B9
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x170012C7 RID: 4807
			// (get) Token: 0x06006E2E RID: 28206 RVA: 0x0017C2C6 File Offset: 0x0017A4C6
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012C8 RID: 4808
			public override object this[object key]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[key];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[key] = value;
					}
				}
			}

			// Token: 0x06006E31 RID: 28209 RVA: 0x0017C35C File Offset: 0x0017A55C
			public override void Add(object key, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(key, value);
				}
			}

			// Token: 0x170012C9 RID: 4809
			// (get) Token: 0x06006E32 RID: 28210 RVA: 0x0017C3A4 File Offset: 0x0017A5A4
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
			}

			// Token: 0x06006E33 RID: 28211 RVA: 0x0017C3EC File Offset: 0x0017A5EC
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006E34 RID: 28212 RVA: 0x0017C434 File Offset: 0x0017A634
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._list.Clone();
				}
				return obj;
			}

			// Token: 0x06006E35 RID: 28213 RVA: 0x0017C47C File Offset: 0x0017A67C
			public override bool Contains(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(key);
				}
				return flag2;
			}

			// Token: 0x06006E36 RID: 28214 RVA: 0x0017C4C4 File Offset: 0x0017A6C4
			public override bool ContainsKey(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsKey(key);
				}
				return flag2;
			}

			// Token: 0x06006E37 RID: 28215 RVA: 0x0017C50C File Offset: 0x0017A70C
			public override bool ContainsValue(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsValue(key);
				}
				return flag2;
			}

			// Token: 0x06006E38 RID: 28216 RVA: 0x0017C554 File Offset: 0x0017A754
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06006E39 RID: 28217 RVA: 0x0017C59C File Offset: 0x0017A79C
			public override object GetByIndex(int index)
			{
				object root = this._root;
				object byIndex;
				lock (root)
				{
					byIndex = this._list.GetByIndex(index);
				}
				return byIndex;
			}

			// Token: 0x06006E3A RID: 28218 RVA: 0x0017C5E4 File Offset: 0x0017A7E4
			public override IDictionaryEnumerator GetEnumerator()
			{
				object root = this._root;
				IDictionaryEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006E3B RID: 28219 RVA: 0x0017C62C File Offset: 0x0017A82C
			public override object GetKey(int index)
			{
				object root = this._root;
				object key;
				lock (root)
				{
					key = this._list.GetKey(index);
				}
				return key;
			}

			// Token: 0x06006E3C RID: 28220 RVA: 0x0017C674 File Offset: 0x0017A874
			public override IList GetKeyList()
			{
				object root = this._root;
				IList keyList;
				lock (root)
				{
					keyList = this._list.GetKeyList();
				}
				return keyList;
			}

			// Token: 0x06006E3D RID: 28221 RVA: 0x0017C6BC File Offset: 0x0017A8BC
			public override IList GetValueList()
			{
				object root = this._root;
				IList valueList;
				lock (root)
				{
					valueList = this._list.GetValueList();
				}
				return valueList;
			}

			// Token: 0x06006E3E RID: 28222 RVA: 0x0017C704 File Offset: 0x0017A904
			public override int IndexOfKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfKey(key);
				}
				return num;
			}

			// Token: 0x06006E3F RID: 28223 RVA: 0x0017C764 File Offset: 0x0017A964
			public override int IndexOfValue(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfValue(value);
				}
				return num;
			}

			// Token: 0x06006E40 RID: 28224 RVA: 0x0017C7AC File Offset: 0x0017A9AC
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06006E41 RID: 28225 RVA: 0x0017C7F4 File Offset: 0x0017A9F4
			public override void Remove(object key)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(key);
				}
			}

			// Token: 0x06006E42 RID: 28226 RVA: 0x0017C83C File Offset: 0x0017AA3C
			public override void SetByIndex(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetByIndex(index, value);
				}
			}

			// Token: 0x06006E43 RID: 28227 RVA: 0x0017C884 File Offset: 0x0017AA84
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._list.ToKeyValuePairsArray();
			}

			// Token: 0x06006E44 RID: 28228 RVA: 0x0017C894 File Offset: 0x0017AA94
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x04003589 RID: 13705
			private SortedList _list;

			// Token: 0x0400358A RID: 13706
			private object _root;
		}

		// Token: 0x02000BC0 RID: 3008
		[Serializable]
		private class SortedListEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06006E45 RID: 28229 RVA: 0x0017C8DC File Offset: 0x0017AADC
			internal SortedListEnumerator(SortedList sortedList, int index, int count, int getObjRetType)
			{
				this.sortedList = sortedList;
				this.index = index;
				this.startIndex = index;
				this.endIndex = index + count;
				this.version = sortedList.version;
				this.getObjectRetType = getObjRetType;
				this.current = false;
			}

			// Token: 0x06006E46 RID: 28230 RVA: 0x0017C928 File Offset: 0x0017AB28
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x170012CA RID: 4810
			// (get) Token: 0x06006E47 RID: 28231 RVA: 0x0017C930 File Offset: 0x0017AB30
			public virtual object Key
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.key;
				}
			}

			// Token: 0x06006E48 RID: 28232 RVA: 0x0017C980 File Offset: 0x0017AB80
			public virtual bool MoveNext()
			{
				if (this.version != this.sortedList.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.index < this.endIndex)
				{
					this.key = this.sortedList.keys[this.index];
					this.value = this.sortedList.values[this.index];
					this.index++;
					this.current = true;
					return true;
				}
				this.key = null;
				this.value = null;
				this.current = false;
				return false;
			}

			// Token: 0x170012CB RID: 4811
			// (get) Token: 0x06006E49 RID: 28233 RVA: 0x0017CA1C File Offset: 0x0017AC1C
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x170012CC RID: 4812
			// (get) Token: 0x06006E4A RID: 28234 RVA: 0x0017CA78 File Offset: 0x0017AC78
			public virtual object Current
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					if (this.getObjectRetType == 1)
					{
						return this.key;
					}
					if (this.getObjectRetType == 2)
					{
						return this.value;
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x170012CD RID: 4813
			// (get) Token: 0x06006E4B RID: 28235 RVA: 0x0017CAD4 File Offset: 0x0017ACD4
			public virtual object Value
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.value;
				}
			}

			// Token: 0x06006E4C RID: 28236 RVA: 0x0017CB24 File Offset: 0x0017AD24
			public virtual void Reset()
			{
				if (this.version != this.sortedList.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.index = this.startIndex;
				this.current = false;
				this.key = null;
				this.value = null;
			}

			// Token: 0x0400358B RID: 13707
			private SortedList sortedList;

			// Token: 0x0400358C RID: 13708
			private object key;

			// Token: 0x0400358D RID: 13709
			private object value;

			// Token: 0x0400358E RID: 13710
			private int index;

			// Token: 0x0400358F RID: 13711
			private int startIndex;

			// Token: 0x04003590 RID: 13712
			private int endIndex;

			// Token: 0x04003591 RID: 13713
			private int version;

			// Token: 0x04003592 RID: 13714
			private bool current;

			// Token: 0x04003593 RID: 13715
			private int getObjectRetType;

			// Token: 0x04003594 RID: 13716
			internal const int Keys = 1;

			// Token: 0x04003595 RID: 13717
			internal const int Values = 2;

			// Token: 0x04003596 RID: 13718
			internal const int DictEntry = 3;
		}

		// Token: 0x02000BC1 RID: 3009
		[Serializable]
		private class KeyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006E4D RID: 28237 RVA: 0x0017CB75 File Offset: 0x0017AD75
			internal KeyList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170012CE RID: 4814
			// (get) Token: 0x06006E4E RID: 28238 RVA: 0x0017CB84 File Offset: 0x0017AD84
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170012CF RID: 4815
			// (get) Token: 0x06006E4F RID: 28239 RVA: 0x0017CB91 File Offset: 0x0017AD91
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012D0 RID: 4816
			// (get) Token: 0x06006E50 RID: 28240 RVA: 0x0017CB94 File Offset: 0x0017AD94
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012D1 RID: 4817
			// (get) Token: 0x06006E51 RID: 28241 RVA: 0x0017CB97 File Offset: 0x0017AD97
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170012D2 RID: 4818
			// (get) Token: 0x06006E52 RID: 28242 RVA: 0x0017CBA4 File Offset: 0x0017ADA4
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06006E53 RID: 28243 RVA: 0x0017CBB1 File Offset: 0x0017ADB1
			public virtual int Add(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E54 RID: 28244 RVA: 0x0017CBC2 File Offset: 0x0017ADC2
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E55 RID: 28245 RVA: 0x0017CBD3 File Offset: 0x0017ADD3
			public virtual bool Contains(object key)
			{
				return this.sortedList.Contains(key);
			}

			// Token: 0x06006E56 RID: 28246 RVA: 0x0017CBE1 File Offset: 0x0017ADE1
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				Array.Copy(this.sortedList.keys, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06006E57 RID: 28247 RVA: 0x0017CC1D File Offset: 0x0017AE1D
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x170012D3 RID: 4819
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetKey(index);
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
				}
			}

			// Token: 0x06006E5A RID: 28250 RVA: 0x0017CC4D File Offset: 0x0017AE4D
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 1);
			}

			// Token: 0x06006E5B RID: 28251 RVA: 0x0017CC68 File Offset: 0x0017AE68
			public virtual int IndexOf(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				int num = Array.BinarySearch(this.sortedList.keys, 0, this.sortedList.Count, key, this.sortedList.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06006E5C RID: 28252 RVA: 0x0017CCBD File Offset: 0x0017AEBD
			public virtual void Remove(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E5D RID: 28253 RVA: 0x0017CCCE File Offset: 0x0017AECE
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x04003597 RID: 13719
			private SortedList sortedList;
		}

		// Token: 0x02000BC2 RID: 3010
		[Serializable]
		private class ValueList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006E5E RID: 28254 RVA: 0x0017CCDF File Offset: 0x0017AEDF
			internal ValueList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170012D4 RID: 4820
			// (get) Token: 0x06006E5F RID: 28255 RVA: 0x0017CCEE File Offset: 0x0017AEEE
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170012D5 RID: 4821
			// (get) Token: 0x06006E60 RID: 28256 RVA: 0x0017CCFB File Offset: 0x0017AEFB
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012D6 RID: 4822
			// (get) Token: 0x06006E61 RID: 28257 RVA: 0x0017CCFE File Offset: 0x0017AEFE
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012D7 RID: 4823
			// (get) Token: 0x06006E62 RID: 28258 RVA: 0x0017CD01 File Offset: 0x0017AF01
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170012D8 RID: 4824
			// (get) Token: 0x06006E63 RID: 28259 RVA: 0x0017CD0E File Offset: 0x0017AF0E
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06006E64 RID: 28260 RVA: 0x0017CD1B File Offset: 0x0017AF1B
			public virtual int Add(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E65 RID: 28261 RVA: 0x0017CD2C File Offset: 0x0017AF2C
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E66 RID: 28262 RVA: 0x0017CD3D File Offset: 0x0017AF3D
			public virtual bool Contains(object value)
			{
				return this.sortedList.ContainsValue(value);
			}

			// Token: 0x06006E67 RID: 28263 RVA: 0x0017CD4B File Offset: 0x0017AF4B
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				Array.Copy(this.sortedList.values, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06006E68 RID: 28264 RVA: 0x0017CD87 File Offset: 0x0017AF87
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x170012D9 RID: 4825
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
				}
			}

			// Token: 0x06006E6B RID: 28267 RVA: 0x0017CDB7 File Offset: 0x0017AFB7
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 2);
			}

			// Token: 0x06006E6C RID: 28268 RVA: 0x0017CDD1 File Offset: 0x0017AFD1
			public virtual int IndexOf(object value)
			{
				return Array.IndexOf<object>(this.sortedList.values, value, 0, this.sortedList.Count);
			}

			// Token: 0x06006E6D RID: 28269 RVA: 0x0017CDF0 File Offset: 0x0017AFF0
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E6E RID: 28270 RVA: 0x0017CE01 File Offset: 0x0017B001
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x04003598 RID: 13720
			private SortedList sortedList;
		}

		// Token: 0x02000BC3 RID: 3011
		internal class SortedListDebugView
		{
			// Token: 0x06006E6F RID: 28271 RVA: 0x0017CE12 File Offset: 0x0017B012
			public SortedListDebugView(SortedList sortedList)
			{
				if (sortedList == null)
				{
					throw new ArgumentNullException("sortedList");
				}
				this.sortedList = sortedList;
			}

			// Token: 0x170012DA RID: 4826
			// (get) Token: 0x06006E70 RID: 28272 RVA: 0x0017CE2F File Offset: 0x0017B02F
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this.sortedList.ToKeyValuePairsArray();
				}
			}

			// Token: 0x04003599 RID: 13721
			private SortedList sortedList;
		}
	}
}
