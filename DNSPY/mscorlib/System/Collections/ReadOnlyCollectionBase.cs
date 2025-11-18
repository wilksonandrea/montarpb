using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200048E RID: 1166
	[ComVisible(true)]
	[Serializable]
	public abstract class ReadOnlyCollectionBase : ICollection, IEnumerable
	{
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x000D5A63 File Offset: 0x000D3C63
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

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000D5A7E File Offset: 0x000D3C7E
		public virtual int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x000D5A8B File Offset: 0x000D3C8B
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000D5A98 File Offset: 0x000D3C98
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000D5AA5 File Offset: 0x000D3CA5
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000D5AB4 File Offset: 0x000D3CB4
		public virtual IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000D5AC1 File Offset: 0x000D3CC1
		protected ReadOnlyCollectionBase()
		{
		}

		// Token: 0x040018BB RID: 6331
		private ArrayList list;
	}
}
