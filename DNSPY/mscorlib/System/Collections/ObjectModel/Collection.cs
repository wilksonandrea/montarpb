using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
	// Token: 0x020004B5 RID: 1205
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x060039AC RID: 14764 RVA: 0x000DC8C0 File Offset: 0x000DAAC0
		[__DynamicallyInvokable]
		public Collection()
		{
			this.items = new List<T>();
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x000DC8D3 File Offset: 0x000DAAD3
		[__DynamicallyInvokable]
		public Collection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.items = list;
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x000DC8EB File Offset: 0x000DAAEB
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000DC8F8 File Offset: 0x000DAAF8
		[__DynamicallyInvokable]
		protected IList<T> Items
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items;
			}
		}

		// Token: 0x170008A8 RID: 2216
		[__DynamicallyInvokable]
		public T this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items[index];
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.items.IsReadOnly)
				{
					ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				}
				if (index < 0 || index >= this.items.Count)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this.SetItem(index, value);
			}
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000DC944 File Offset: 0x000DAB44
		[__DynamicallyInvokable]
		public void Add(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int count = this.items.Count;
			this.InsertItem(count, item);
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000DC979 File Offset: 0x000DAB79
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			this.ClearItems();
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000DC995 File Offset: 0x000DAB95
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000DC9A4 File Offset: 0x000DABA4
		[__DynamicallyInvokable]
		public bool Contains(T item)
		{
			return this.items.Contains(item);
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000DC9B2 File Offset: 0x000DABB2
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000DC9BF File Offset: 0x000DABBF
		[__DynamicallyInvokable]
		public int IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000DC9CD File Offset: 0x000DABCD
		[__DynamicallyInvokable]
		public void Insert(int index, T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index < 0 || index > this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
			}
			this.InsertItem(index, item);
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000DCA08 File Offset: 0x000DAC08
		[__DynamicallyInvokable]
		public bool Remove(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int num = this.items.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this.RemoveItem(num);
			return true;
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000DCA44 File Offset: 0x000DAC44
		[__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index < 0 || index >= this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this.RemoveItem(index);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000DCA78 File Offset: 0x000DAC78
		[__DynamicallyInvokable]
		protected virtual void ClearItems()
		{
			this.items.Clear();
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000DCA85 File Offset: 0x000DAC85
		[__DynamicallyInvokable]
		protected virtual void InsertItem(int index, T item)
		{
			this.items.Insert(index, item);
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000DCA94 File Offset: 0x000DAC94
		[__DynamicallyInvokable]
		protected virtual void RemoveItem(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000DCAA2 File Offset: 0x000DACA2
		[__DynamicallyInvokable]
		protected virtual void SetItem(int index, T item)
		{
			this.items[index] = item;
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000DCAB1 File Offset: 0x000DACB1
		[__DynamicallyInvokable]
		bool ICollection<T>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000DCABE File Offset: 0x000DACBE
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000DCACB File Offset: 0x000DACCB
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060039C2 RID: 14786 RVA: 0x000DCAD0 File Offset: 0x000DACD0
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					ICollection collection = this.items as ICollection;
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

		// Token: 0x060039C3 RID: 14787 RVA: 0x000DCB1C File Offset: 0x000DAD1C
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
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.items.CopyTo(array2, index);
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
			int count = this.items.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.items[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x170008AC RID: 2220
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items[index];
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

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060039C6 RID: 14790 RVA: 0x000DCC7C File Offset: 0x000DAE7C
		[__DynamicallyInvokable]
		bool IList.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x000DCC8C File Offset: 0x000DAE8C
		[__DynamicallyInvokable]
		bool IList.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				IList list = this.items as IList;
				if (list != null)
				{
					return list.IsFixedSize;
				}
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000DCCBC File Offset: 0x000DAEBC
		[__DynamicallyInvokable]
		int IList.Add(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			try
			{
				this.Add((T)((object)value));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
			}
			return this.Count - 1;
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000DCD20 File Offset: 0x000DAF20
		[__DynamicallyInvokable]
		bool IList.Contains(object value)
		{
			return Collection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000DCD38 File Offset: 0x000DAF38
		[__DynamicallyInvokable]
		int IList.IndexOf(object value)
		{
			if (Collection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000DCD50 File Offset: 0x000DAF50
		[__DynamicallyInvokable]
		void IList.Insert(int index, object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			try
			{
				this.Insert(index, (T)((object)value));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
			}
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000DCDAC File Offset: 0x000DAFAC
		[__DynamicallyInvokable]
		void IList.Remove(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (Collection<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000DCDD8 File Offset: 0x000DAFD8
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x04001934 RID: 6452
		private IList<T> items;

		// Token: 0x04001935 RID: 6453
		[NonSerialized]
		private object _syncRoot;
	}
}
