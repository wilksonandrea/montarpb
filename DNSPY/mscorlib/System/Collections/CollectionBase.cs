using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200048C RID: 1164
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class CollectionBase : IList, ICollection, IEnumerable
	{
		// Token: 0x0600377B RID: 14203 RVA: 0x000D545C File Offset: 0x000D365C
		protected CollectionBase()
		{
			this.list = new ArrayList();
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000D546F File Offset: 0x000D366F
		protected CollectionBase(int capacity)
		{
			this.list = new ArrayList(capacity);
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000D5483 File Offset: 0x000D3683
		protected ArrayList InnerList
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600377E RID: 14206 RVA: 0x000D549E File Offset: 0x000D369E
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600377F RID: 14207 RVA: 0x000D54A1 File Offset: 0x000D36A1
		// (set) Token: 0x06003780 RID: 14208 RVA: 0x000D54AE File Offset: 0x000D36AE
		[ComVisible(false)]
		public int Capacity
		{
			get
			{
				return this.InnerList.Capacity;
			}
			set
			{
				this.InnerList.Capacity = value;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x000D54BC File Offset: 0x000D36BC
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.list != null)
				{
					return this.list.Count;
				}
				return 0;
			}
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000D54D3 File Offset: 0x000D36D3
		[__DynamicallyInvokable]
		public void Clear()
		{
			this.OnClear();
			this.InnerList.Clear();
			this.OnClearComplete();
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000D54EC File Offset: 0x000D36EC
		[__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			object obj = this.InnerList[index];
			this.OnValidate(obj);
			this.OnRemove(index, obj);
			this.InnerList.RemoveAt(index);
			try
			{
				this.OnRemoveComplete(index, obj);
			}
			catch
			{
				this.InnerList.Insert(index, obj);
				throw;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003784 RID: 14212 RVA: 0x000D5570 File Offset: 0x000D3770
		bool IList.IsReadOnly
		{
			get
			{
				return this.InnerList.IsReadOnly;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x000D557D File Offset: 0x000D377D
		bool IList.IsFixedSize
		{
			get
			{
				return this.InnerList.IsFixedSize;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06003786 RID: 14214 RVA: 0x000D558A File Offset: 0x000D378A
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06003787 RID: 14215 RVA: 0x000D5597 File Offset: 0x000D3797
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000D55A4 File Offset: 0x000D37A4
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x1700082B RID: 2091
		object IList.this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return this.InnerList[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this.OnValidate(value);
				object obj = this.InnerList[index];
				this.OnSet(index, obj, value);
				this.InnerList[index] = value;
				try
				{
					this.OnSetComplete(index, obj, value);
				}
				catch
				{
					this.InnerList[index] = obj;
					throw;
				}
			}
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000D5668 File Offset: 0x000D3868
		bool IList.Contains(object value)
		{
			return this.InnerList.Contains(value);
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000D5678 File Offset: 0x000D3878
		int IList.Add(object value)
		{
			this.OnValidate(value);
			this.OnInsert(this.InnerList.Count, value);
			int num = this.InnerList.Add(value);
			try
			{
				this.OnInsertComplete(num, value);
			}
			catch
			{
				this.InnerList.RemoveAt(num);
				throw;
			}
			return num;
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000D56D8 File Offset: 0x000D38D8
		void IList.Remove(object value)
		{
			this.OnValidate(value);
			int num = this.InnerList.IndexOf(value);
			if (num < 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RemoveArgNotFound"));
			}
			this.OnRemove(num, value);
			this.InnerList.RemoveAt(num);
			try
			{
				this.OnRemoveComplete(num, value);
			}
			catch
			{
				this.InnerList.Insert(num, value);
				throw;
			}
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000D574C File Offset: 0x000D394C
		int IList.IndexOf(object value)
		{
			return this.InnerList.IndexOf(value);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000D575C File Offset: 0x000D395C
		void IList.Insert(int index, object value)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this.OnValidate(value);
			this.OnInsert(index, value);
			this.InnerList.Insert(index, value);
			try
			{
				this.OnInsertComplete(index, value);
			}
			catch
			{
				this.InnerList.RemoveAt(index);
				throw;
			}
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000D57D0 File Offset: 0x000D39D0
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000D57DD File Offset: 0x000D39DD
		protected virtual void OnSet(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000D57DF File Offset: 0x000D39DF
		protected virtual void OnInsert(int index, object value)
		{
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000D57E1 File Offset: 0x000D39E1
		protected virtual void OnClear()
		{
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000D57E3 File Offset: 0x000D39E3
		protected virtual void OnRemove(int index, object value)
		{
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000D57E5 File Offset: 0x000D39E5
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000D57F5 File Offset: 0x000D39F5
		protected virtual void OnSetComplete(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000D57F7 File Offset: 0x000D39F7
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000D57F9 File Offset: 0x000D39F9
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000D57FB File Offset: 0x000D39FB
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		// Token: 0x040018B9 RID: 6329
		private ArrayList list;
	}
}
