using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
	// Token: 0x020004B6 RID: 1206
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x060039CE RID: 14798 RVA: 0x000DCE05 File Offset: 0x000DB005
		[__DynamicallyInvokable]
		public ReadOnlyCollection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.list = list;
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000DCE1D File Offset: 0x000DB01D
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170008B0 RID: 2224
		[__DynamicallyInvokable]
		public T this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000DCE38 File Offset: 0x000DB038
		[__DynamicallyInvokable]
		public bool Contains(T value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000DCE46 File Offset: 0x000DB046
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000DCE55 File Offset: 0x000DB055
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000DCE62 File Offset: 0x000DB062
		[__DynamicallyInvokable]
		public int IndexOf(T value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000DCE70 File Offset: 0x000DB070
		[__DynamicallyInvokable]
		protected IList<T> Items
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000DCE78 File Offset: 0x000DB078
		[__DynamicallyInvokable]
		bool ICollection<T>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170008B3 RID: 2227
		[__DynamicallyInvokable]
		T IList<T>.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list[index];
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000DCE92 File Offset: 0x000DB092
		[__DynamicallyInvokable]
		void ICollection<T>.Add(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000DCE9B File Offset: 0x000DB09B
		[__DynamicallyInvokable]
		void ICollection<T>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000DCEA4 File Offset: 0x000DB0A4
		[__DynamicallyInvokable]
		void IList<T>.Insert(int index, T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000DCEAD File Offset: 0x000DB0AD
		[__DynamicallyInvokable]
		bool ICollection<T>.Remove(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000DCEB7 File Offset: 0x000DB0B7
		[__DynamicallyInvokable]
		void IList<T>.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000DCEC0 File Offset: 0x000DB0C0
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x000DCECD File Offset: 0x000DB0CD
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060039E0 RID: 14816 RVA: 0x000DCED0 File Offset: 0x000DB0D0
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					ICollection collection = this.list as ICollection;
					if (collection != null)
					{
						this._syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000DCF1C File Offset: 0x000DB11C
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.list.CopyTo(array2, index);
				return;
			}
			Type elementType = array.GetType().GetElementType();
			Type typeFromHandle = typeof(T);
			if (!elementType.IsAssignableFrom(typeFromHandle) && !typeFromHandle.IsAssignableFrom(elementType))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			int count = this.list.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.list[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000DD020 File Offset: 0x000DB220
		[__DynamicallyInvokable]
		bool IList.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000DD023 File Offset: 0x000DB223
		[__DynamicallyInvokable]
		bool IList.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170008B8 RID: 2232
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list[index];
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000DD042 File Offset: 0x000DB242
		[__DynamicallyInvokable]
		int IList.Add(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return -1;
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000DD04C File Offset: 0x000DB24C
		[__DynamicallyInvokable]
		void IList.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000DD058 File Offset: 0x000DB258
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000DD085 File Offset: 0x000DB285
		[__DynamicallyInvokable]
		bool IList.Contains(object value)
		{
			return ReadOnlyCollection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000DD09D File Offset: 0x000DB29D
		[__DynamicallyInvokable]
		int IList.IndexOf(object value)
		{
			if (ReadOnlyCollection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000DD0B5 File Offset: 0x000DB2B5
		[__DynamicallyInvokable]
		void IList.Insert(int index, object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x000DD0BE File Offset: 0x000DB2BE
		[__DynamicallyInvokable]
		void IList.Remove(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000DD0C7 File Offset: 0x000DB2C7
		[__DynamicallyInvokable]
		void IList.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x04001936 RID: 6454
		private IList<T> list;

		// Token: 0x04001937 RID: 6455
		[NonSerialized]
		private object _syncRoot;
	}
}
