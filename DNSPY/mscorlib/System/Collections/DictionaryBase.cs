using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200048D RID: 1165
	[ComVisible(true)]
	[Serializable]
	public abstract class DictionaryBase : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000D57FD File Offset: 0x000D39FD
		protected Hashtable InnerHashtable
		{
			get
			{
				if (this.hashtable == null)
				{
					this.hashtable = new Hashtable();
				}
				return this.hashtable;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000D5818 File Offset: 0x000D3A18
		protected IDictionary Dictionary
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x000D581B File Offset: 0x000D3A1B
		public int Count
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Count;
				}
				return 0;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000D5832 File Offset: 0x000D3A32
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.InnerHashtable.IsReadOnly;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000D583F File Offset: 0x000D3A3F
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.InnerHashtable.IsFixedSize;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000D584C File Offset: 0x000D3A4C
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerHashtable.IsSynchronized;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000D5859 File Offset: 0x000D3A59
		ICollection IDictionary.Keys
		{
			get
			{
				return this.InnerHashtable.Keys;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x000D5866 File Offset: 0x000D3A66
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerHashtable.SyncRoot;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000D5873 File Offset: 0x000D3A73
		ICollection IDictionary.Values
		{
			get
			{
				return this.InnerHashtable.Values;
			}
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000D5880 File Offset: 0x000D3A80
		public void CopyTo(Array array, int index)
		{
			this.InnerHashtable.CopyTo(array, index);
		}

		// Token: 0x17000835 RID: 2101
		object IDictionary.this[object key]
		{
			get
			{
				object obj = this.InnerHashtable[key];
				this.OnGet(key, obj);
				return obj;
			}
			set
			{
				this.OnValidate(key, value);
				bool flag = true;
				object obj = this.InnerHashtable[key];
				if (obj == null)
				{
					flag = this.InnerHashtable.Contains(key);
				}
				this.OnSet(key, obj, value);
				this.InnerHashtable[key] = value;
				try
				{
					this.OnSetComplete(key, obj, value);
				}
				catch
				{
					if (flag)
					{
						this.InnerHashtable[key] = obj;
					}
					else
					{
						this.InnerHashtable.Remove(key);
					}
					throw;
				}
			}
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000D593C File Offset: 0x000D3B3C
		bool IDictionary.Contains(object key)
		{
			return this.InnerHashtable.Contains(key);
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000D594C File Offset: 0x000D3B4C
		void IDictionary.Add(object key, object value)
		{
			this.OnValidate(key, value);
			this.OnInsert(key, value);
			this.InnerHashtable.Add(key, value);
			try
			{
				this.OnInsertComplete(key, value);
			}
			catch
			{
				this.InnerHashtable.Remove(key);
				throw;
			}
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000D59A0 File Offset: 0x000D3BA0
		public void Clear()
		{
			this.OnClear();
			this.InnerHashtable.Clear();
			this.OnClearComplete();
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000D59BC File Offset: 0x000D3BBC
		void IDictionary.Remove(object key)
		{
			if (this.InnerHashtable.Contains(key))
			{
				object obj = this.InnerHashtable[key];
				this.OnValidate(key, obj);
				this.OnRemove(key, obj);
				this.InnerHashtable.Remove(key);
				try
				{
					this.OnRemoveComplete(key, obj);
				}
				catch
				{
					this.InnerHashtable.Add(key, obj);
					throw;
				}
			}
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000D5A2C File Offset: 0x000D3C2C
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000D5A39 File Offset: 0x000D3C39
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000D5A46 File Offset: 0x000D3C46
		protected virtual object OnGet(object key, object currentValue)
		{
			return currentValue;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000D5A49 File Offset: 0x000D3C49
		protected virtual void OnSet(object key, object oldValue, object newValue)
		{
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000D5A4B File Offset: 0x000D3C4B
		protected virtual void OnInsert(object key, object value)
		{
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000D5A4D File Offset: 0x000D3C4D
		protected virtual void OnClear()
		{
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000D5A4F File Offset: 0x000D3C4F
		protected virtual void OnRemove(object key, object value)
		{
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000D5A51 File Offset: 0x000D3C51
		protected virtual void OnValidate(object key, object value)
		{
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000D5A53 File Offset: 0x000D3C53
		protected virtual void OnSetComplete(object key, object oldValue, object newValue)
		{
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000D5A55 File Offset: 0x000D3C55
		protected virtual void OnInsertComplete(object key, object value)
		{
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000D5A57 File Offset: 0x000D3C57
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000D5A59 File Offset: 0x000D3C59
		protected virtual void OnRemoveComplete(object key, object value)
		{
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000D5A5B File Offset: 0x000D3C5B
		protected DictionaryBase()
		{
		}

		// Token: 0x040018BA RID: 6330
		private Hashtable hashtable;
	}
}
