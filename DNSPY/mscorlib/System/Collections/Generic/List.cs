using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x020004DC RID: 1244
	[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x06003AEF RID: 15087 RVA: 0x000DF919 File Offset: 0x000DDB19
		[__DynamicallyInvokable]
		public List()
		{
			this._items = List<T>._emptyArray;
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000DF92C File Offset: 0x000DDB2C
		[__DynamicallyInvokable]
		public List(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (capacity == 0)
			{
				this._items = List<T>._emptyArray;
				return;
			}
			this._items = new T[capacity];
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000DF95C File Offset: 0x000DDB5C
		[__DynamicallyInvokable]
		public List(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 == null)
			{
				this._size = 0;
				this._items = List<T>._emptyArray;
				foreach (T t in collection)
				{
					this.Add(t);
				}
				return;
			}
			int count = collection2.Count;
			if (count == 0)
			{
				this._items = List<T>._emptyArray;
				return;
			}
			this._items = new T[count];
			collection2.CopyTo(this._items, 0);
			this._size = count;
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x000DFA04 File Offset: 0x000DDC04
		// (set) Token: 0x06003AF3 RID: 15091 RVA: 0x000DFA10 File Offset: 0x000DDC10
		[__DynamicallyInvokable]
		public int Capacity
		{
			[__DynamicallyInvokable]
			get
			{
				return this._items.Length;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = List<T>._emptyArray;
				}
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000DFA75 File Offset: 0x000DDC75
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._size;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000DFA7D File Offset: 0x000DDC7D
		[__DynamicallyInvokable]
		bool IList.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000DFA80 File Offset: 0x000DDC80
		[__DynamicallyInvokable]
		bool ICollection<T>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000DFA83 File Offset: 0x000DDC83
		[__DynamicallyInvokable]
		bool IList.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000DFA86 File Offset: 0x000DDC86
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000DFA89 File Offset: 0x000DDC89
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170008FB RID: 2299
		[__DynamicallyInvokable]
		public T this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return this._items[index];
			}
			[__DynamicallyInvokable]
			set
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x000DFAF4 File Offset: 0x000DDCF4
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x170008FC RID: 2300
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this[index];
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
				}
			}
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x000DFB78 File Offset: 0x000DDD78
		[__DynamicallyInvokable]
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x000DFBD0 File Offset: 0x000DDDD0
		[__DynamicallyInvokable]
		int IList.Add(object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Add((T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
			}
			return this.Count - 1;
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000DFC20 File Offset: 0x000DDE20
		[__DynamicallyInvokable]
		public void AddRange(IEnumerable<T> collection)
		{
			this.InsertRange(this._size, collection);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000DFC2F File Offset: 0x000DDE2F
		[__DynamicallyInvokable]
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return new ReadOnlyCollection<T>(this);
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000DFC37 File Offset: 0x000DDE37
		[__DynamicallyInvokable]
		public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			return Array.BinarySearch<T>(this._items, index, count, item, comparer);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x000DFC73 File Offset: 0x000DDE73
		[__DynamicallyInvokable]
		public int BinarySearch(T item)
		{
			return this.BinarySearch(0, this.Count, item, null);
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x000DFC84 File Offset: 0x000DDE84
		[__DynamicallyInvokable]
		public int BinarySearch(T item, IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x000DFC95 File Offset: 0x000DDE95
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000DFCC8 File Offset: 0x000DDEC8
		[__DynamicallyInvokable]
		public bool Contains(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int j = 0; j < this._size; j++)
			{
				if (@default.Equals(this._items[j], item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000DFD34 File Offset: 0x000DDF34
		[__DynamicallyInvokable]
		bool IList.Contains(object item)
		{
			return List<T>.IsCompatibleObject(item) && this.Contains((T)((object)item));
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x000DFD4C File Offset: 0x000DDF4C
		public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			if (converter == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
			}
			List<TOutput> list = new List<TOutput>(this._size);
			for (int i = 0; i < this._size; i++)
			{
				list._items[i] = converter(this._items[i]);
			}
			list._size = this._size;
			return list;
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x000DFDAB File Offset: 0x000DDFAB
		[__DynamicallyInvokable]
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x000DFDB8 File Offset: 0x000DDFB8
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			try
			{
				Array.Copy(this._items, 0, array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x000DFE08 File Offset: 0x000DE008
		[__DynamicallyInvokable]
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x000DFE2D File Offset: 0x000DE02D
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x000DFE44 File Offset: 0x000DE044
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = ((this._items.Length == 0) ? 4 : (this._items.Length * 2));
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
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000DFE8E File Offset: 0x000DE08E
		[__DynamicallyInvokable]
		public bool Exists(Predicate<T> match)
		{
			return this.FindIndex(match) != -1;
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x000DFEA0 File Offset: 0x000DE0A0
		[__DynamicallyInvokable]
		public T Find(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x000DFEF4 File Offset: 0x000DE0F4
		[__DynamicallyInvokable]
		public List<T> FindAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			List<T> list = new List<T>();
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					list.Add(this._items[i]);
				}
			}
			return list;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x000DFF48 File Offset: 0x000DE148
		[__DynamicallyInvokable]
		public int FindIndex(Predicate<T> match)
		{
			return this.FindIndex(0, this._size, match);
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x000DFF58 File Offset: 0x000DE158
		[__DynamicallyInvokable]
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this.FindIndex(startIndex, this._size - startIndex, match);
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x000DFF6C File Offset: 0x000DE16C
		[__DynamicallyInvokable]
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			if (startIndex > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (count < 0 || startIndex > this._size - count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x000DFFD4 File Offset: 0x000DE1D4
		[__DynamicallyInvokable]
		public T FindLast(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = this._size - 1; i >= 0; i--)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x000E0027 File Offset: 0x000DE227
		[__DynamicallyInvokable]
		public int FindLastIndex(Predicate<T> match)
		{
			return this.FindLastIndex(this._size - 1, this._size, match);
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x000E003E File Offset: 0x000DE23E
		[__DynamicallyInvokable]
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this.FindLastIndex(startIndex, startIndex + 1, match);
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000E004C File Offset: 0x000DE24C
		[__DynamicallyInvokable]
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (this._size == 0)
			{
				if (startIndex != -1)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
				}
			}
			else if (startIndex >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x000E00C8 File Offset: 0x000DE2C8
		[__DynamicallyInvokable]
		public void ForEach(Action<T> action)
		{
			if (action == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int version = this._version;
			int num = 0;
			while (num < this._size && (version == this._version || !BinaryCompatibility.TargetsAtLeast_Desktop_V4_5))
			{
				action(this._items[num]);
				num++;
			}
			if (version != this._version && BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
			}
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x000E012F File Offset: 0x000DE32F
		[__DynamicallyInvokable]
		public List<T>.Enumerator GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000E0137 File Offset: 0x000DE337
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x000E0144 File Offset: 0x000DE344
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x000E0154 File Offset: 0x000DE354
		[__DynamicallyInvokable]
		public List<T> GetRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			List<T> list = new List<T>(count);
			Array.Copy(this._items, index, list._items, 0, count);
			list._size = count;
			return list;
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x000E01AE File Offset: 0x000DE3AE
		[__DynamicallyInvokable]
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x000E01C3 File Offset: 0x000DE3C3
		[__DynamicallyInvokable]
		int IList.IndexOf(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				return this.IndexOf((T)((object)item));
			}
			return -1;
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x000E01DB File Offset: 0x000DE3DB
		[__DynamicallyInvokable]
		public int IndexOf(T item, int index)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return Array.IndexOf<T>(this._items, item, index, this._size - index);
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000E0204 File Offset: 0x000DE404
		[__DynamicallyInvokable]
		public int IndexOf(T item, int index, int count)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (count < 0 || index > this._size - count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
			}
			return Array.IndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000E0240 File Offset: 0x000DE440
		[__DynamicallyInvokable]
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000E02CC File Offset: 0x000DE4CC
		[__DynamicallyInvokable]
		void IList.Insert(int index, object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Insert(index, (T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
			}
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000E0314 File Offset: 0x000DE514
		[__DynamicallyInvokable]
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > 0)
				{
					this.EnsureCapacity(this._size + count);
					if (index < this._size)
					{
						Array.Copy(this._items, index, this._items, index + count, this._size - index);
					}
					if (this == collection2)
					{
						Array.Copy(this._items, 0, this._items, index, index);
						Array.Copy(this._items, index + count, this._items, index * 2, this._size - index);
					}
					else
					{
						T[] array = new T[count];
						collection2.CopyTo(array, 0);
						array.CopyTo(this._items, index);
					}
					this._size += count;
				}
			}
			else
			{
				foreach (T t in collection)
				{
					this.Insert(index++, t);
				}
			}
			this._version++;
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x000E0440 File Offset: 0x000DE640
		[__DynamicallyInvokable]
		public int LastIndexOf(T item)
		{
			if (this._size == 0)
			{
				return -1;
			}
			return this.LastIndexOf(item, this._size - 1, this._size);
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x000E0461 File Offset: 0x000DE661
		[__DynamicallyInvokable]
		public int LastIndexOf(T item, int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return this.LastIndexOf(item, index, index + 1);
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x000E0480 File Offset: 0x000DE680
		[__DynamicallyInvokable]
		public int LastIndexOf(T item, int index, int count)
		{
			if (this.Count != 0 && index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this.Count != 0 && count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			if (count > index + 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			return Array.LastIndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x000E04F0 File Offset: 0x000DE6F0
		[__DynamicallyInvokable]
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000E0513 File Offset: 0x000DE713
		[__DynamicallyInvokable]
		void IList.Remove(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				this.Remove((T)((object)item));
			}
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000E052C File Offset: 0x000DE72C
		[__DynamicallyInvokable]
		public int RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = 0;
			while (num < this._size && !match(this._items[num]))
			{
				num++;
			}
			if (num >= this._size)
			{
				return 0;
			}
			int i = num + 1;
			while (i < this._size)
			{
				while (i < this._size && match(this._items[i]))
				{
					i++;
				}
				if (i < this._size)
				{
					this._items[num++] = this._items[i++];
				}
			}
			Array.Clear(this._items, num, this._size - num);
			int num2 = this._size - num;
			this._size = num;
			this._version++;
			return num2;
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000E0600 File Offset: 0x000DE800
		[__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000E0678 File Offset: 0x000DE878
		[__DynamicallyInvokable]
		public void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 0)
			{
				int size = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				Array.Clear(this._items, this._size, count);
				this._version++;
			}
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000E070E File Offset: 0x000DE90E
		[__DynamicallyInvokable]
		public void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000E0720 File Offset: 0x000DE920
		[__DynamicallyInvokable]
		public void Reverse(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Reverse(this._items, index, count);
			this._version++;
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000E0772 File Offset: 0x000DE972
		[__DynamicallyInvokable]
		public void Sort()
		{
			this.Sort(0, this.Count, null);
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000E0782 File Offset: 0x000DE982
		[__DynamicallyInvokable]
		public void Sort(IComparer<T> comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000E0794 File Offset: 0x000DE994
		[__DynamicallyInvokable]
		public void Sort(int index, int count, IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Sort<T>(this._items, index, count, comparer);
			this._version++;
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000E07E8 File Offset: 0x000DE9E8
		[__DynamicallyInvokable]
		public void Sort(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (this._size > 0)
			{
				IComparer<T> comparer = new Array.FunctorComparer<T>(comparison);
				Array.Sort<T>(this._items, 0, this._size, comparer);
			}
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000E0824 File Offset: 0x000DEA24
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000E0854 File Offset: 0x000DEA54
		[__DynamicallyInvokable]
		public void TrimExcess()
		{
			int num = (int)((double)this._items.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000E088C File Offset: 0x000DEA8C
		[__DynamicallyInvokable]
		public bool TrueForAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (!match(this._items[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x000E08CA File Offset: 0x000DEACA
		internal static IList<T> Synchronized(List<T> list)
		{
			return new List<T>.SynchronizedList(list);
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x000E08D2 File Offset: 0x000DEAD2
		// Note: this type is marked as 'beforefieldinit'.
		static List()
		{
		}

		// Token: 0x0400195B RID: 6491
		private const int _defaultCapacity = 4;

		// Token: 0x0400195C RID: 6492
		private T[] _items;

		// Token: 0x0400195D RID: 6493
		private int _size;

		// Token: 0x0400195E RID: 6494
		private int _version;

		// Token: 0x0400195F RID: 6495
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001960 RID: 6496
		private static readonly T[] _emptyArray = new T[0];

		// Token: 0x02000BE6 RID: 3046
		[Serializable]
		internal class SynchronizedList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x06006F2F RID: 28463 RVA: 0x0017EA4C File Offset: 0x0017CC4C
			internal SynchronizedList(List<T> list)
			{
				this._list = list;
				this._root = ((ICollection)list).SyncRoot;
			}

			// Token: 0x17001316 RID: 4886
			// (get) Token: 0x06006F30 RID: 28464 RVA: 0x0017EA68 File Offset: 0x0017CC68
			public int Count
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

			// Token: 0x17001317 RID: 4887
			// (get) Token: 0x06006F31 RID: 28465 RVA: 0x0017EAB0 File Offset: 0x0017CCB0
			public bool IsReadOnly
			{
				get
				{
					return ((ICollection<T>)this._list).IsReadOnly;
				}
			}

			// Token: 0x06006F32 RID: 28466 RVA: 0x0017EAC0 File Offset: 0x0017CCC0
			public void Add(T item)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(item);
				}
			}

			// Token: 0x06006F33 RID: 28467 RVA: 0x0017EB08 File Offset: 0x0017CD08
			public void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006F34 RID: 28468 RVA: 0x0017EB50 File Offset: 0x0017CD50
			public bool Contains(T item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(item);
				}
				return flag2;
			}

			// Token: 0x06006F35 RID: 28469 RVA: 0x0017EB98 File Offset: 0x0017CD98
			public void CopyTo(T[] array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006F36 RID: 28470 RVA: 0x0017EBE0 File Offset: 0x0017CDE0
			public bool Remove(T item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Remove(item);
				}
				return flag2;
			}

			// Token: 0x06006F37 RID: 28471 RVA: 0x0017EC28 File Offset: 0x0017CE28
			IEnumerator IEnumerable.GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006F38 RID: 28472 RVA: 0x0017EC74 File Offset: 0x0017CE74
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				object root = this._root;
				IEnumerator<T> enumerator;
				lock (root)
				{
					enumerator = ((IEnumerable<T>)this._list).GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x17001318 RID: 4888
			public T this[int index]
			{
				get
				{
					object root = this._root;
					T t;
					lock (root)
					{
						t = this._list[index];
					}
					return t;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x06006F3B RID: 28475 RVA: 0x0017ED4C File Offset: 0x0017CF4C
			public int IndexOf(T item)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(item);
				}
				return num;
			}

			// Token: 0x06006F3C RID: 28476 RVA: 0x0017ED94 File Offset: 0x0017CF94
			public void Insert(int index, T item)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, item);
				}
			}

			// Token: 0x06006F3D RID: 28477 RVA: 0x0017EDDC File Offset: 0x0017CFDC
			public void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x04003601 RID: 13825
			private List<T> _list;

			// Token: 0x04003602 RID: 13826
			private object _root;
		}

		// Token: 0x02000BE7 RID: 3047
		[__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006F3E RID: 28478 RVA: 0x0017EE24 File Offset: 0x0017D024
			internal Enumerator(List<T> list)
			{
				this.list = list;
				this.index = 0;
				this.version = list._version;
				this.current = default(T);
			}

			// Token: 0x06006F3F RID: 28479 RVA: 0x0017EE4C File Offset: 0x0017D04C
			[__DynamicallyInvokable]
			public void Dispose()
			{
			}

			// Token: 0x06006F40 RID: 28480 RVA: 0x0017EE50 File Offset: 0x0017D050
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				List<T> list = this.list;
				if (this.version == list._version && this.index < list._size)
				{
					this.current = list._items[this.index];
					this.index++;
					return true;
				}
				return this.MoveNextRare();
			}

			// Token: 0x06006F41 RID: 28481 RVA: 0x0017EEAD File Offset: 0x0017D0AD
			private bool MoveNextRare()
			{
				if (this.version != this.list._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = this.list._size + 1;
				this.current = default(T);
				return false;
			}

			// Token: 0x17001319 RID: 4889
			// (get) Token: 0x06006F42 RID: 28482 RVA: 0x0017EEE9 File Offset: 0x0017D0E9
			[__DynamicallyInvokable]
			public T Current
			{
				[__DynamicallyInvokable]
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700131A RID: 4890
			// (get) Token: 0x06006F43 RID: 28483 RVA: 0x0017EEF1 File Offset: 0x0017D0F1
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.list._size + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.Current;
				}
			}

			// Token: 0x06006F44 RID: 28484 RVA: 0x0017EF22 File Offset: 0x0017D122
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this.version != this.list._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.current = default(T);
			}

			// Token: 0x04003603 RID: 13827
			private List<T> list;

			// Token: 0x04003604 RID: 13828
			private int index;

			// Token: 0x04003605 RID: 13829
			private int version;

			// Token: 0x04003606 RID: 13830
			private T current;
		}
	}
}
