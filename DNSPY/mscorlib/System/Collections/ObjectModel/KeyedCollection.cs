using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
	// Token: 0x020004B9 RID: 1209
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_KeyedCollectionDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class KeyedCollection<TKey, TItem> : Collection<TItem>
	{
		// Token: 0x06003A16 RID: 14870 RVA: 0x000DD5B0 File Offset: 0x000DB7B0
		[__DynamicallyInvokable]
		protected KeyedCollection()
			: this(null, 0)
		{
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000DD5BA File Offset: 0x000DB7BA
		[__DynamicallyInvokable]
		protected KeyedCollection(IEqualityComparer<TKey> comparer)
			: this(comparer, 0)
		{
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000DD5C4 File Offset: 0x000DB7C4
		[__DynamicallyInvokable]
		protected KeyedCollection(IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			if (dictionaryCreationThreshold == -1)
			{
				dictionaryCreationThreshold = int.MaxValue;
			}
			if (dictionaryCreationThreshold < -1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.dictionaryCreationThreshold, ExceptionResource.ArgumentOutOfRange_InvalidThreshold);
			}
			this.comparer = comparer;
			this.threshold = dictionaryCreationThreshold;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x000DD5FB File Offset: 0x000DB7FB
		[__DynamicallyInvokable]
		public IEqualityComparer<TKey> Comparer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x170008CC RID: 2252
		[__DynamicallyInvokable]
		public TItem this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				if (this.dict != null)
				{
					return this.dict[key];
				}
				foreach (TItem titem in base.Items)
				{
					if (this.comparer.Equals(this.GetKeyForItem(titem), key))
					{
						return titem;
					}
				}
				ThrowHelper.ThrowKeyNotFoundException();
				return default(TItem);
			}
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000DD698 File Offset: 0x000DB898
		[__DynamicallyInvokable]
		public bool Contains(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key);
			}
			if (key != null)
			{
				foreach (TItem titem in base.Items)
				{
					if (this.comparer.Equals(this.GetKeyForItem(titem), key))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000DD728 File Offset: 0x000DB928
		private bool ContainsItem(TItem item)
		{
			TKey keyForItem;
			if (this.dict == null || (keyForItem = this.GetKeyForItem(item)) == null)
			{
				return base.Items.Contains(item);
			}
			TItem titem;
			bool flag = this.dict.TryGetValue(keyForItem, out titem);
			return flag && EqualityComparer<TItem>.Default.Equals(titem, item);
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000DD77C File Offset: 0x000DB97C
		[__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key) && base.Remove(this.dict[key]);
			}
			if (key != null)
			{
				for (int i = 0; i < base.Items.Count; i++)
				{
					if (this.comparer.Equals(this.GetKeyForItem(base.Items[i]), key))
					{
						this.RemoveItem(i);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000DD80A File Offset: 0x000DBA0A
		[__DynamicallyInvokable]
		protected IDictionary<TKey, TItem> Dictionary
		{
			[__DynamicallyInvokable]
			get
			{
				return this.dict;
			}
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000DD814 File Offset: 0x000DBA14
		[__DynamicallyInvokable]
		protected void ChangeItemKey(TItem item, TKey newKey)
		{
			if (!this.ContainsItem(item))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_ItemNotExist);
			}
			TKey keyForItem = this.GetKeyForItem(item);
			if (!this.comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000DD867 File Offset: 0x000DBA67
		[__DynamicallyInvokable]
		protected override void ClearItems()
		{
			base.ClearItems();
			if (this.dict != null)
			{
				this.dict.Clear();
			}
			this.keyCount = 0;
		}

		// Token: 0x06003A21 RID: 14881
		[__DynamicallyInvokable]
		protected abstract TKey GetKeyForItem(TItem item);

		// Token: 0x06003A22 RID: 14882 RVA: 0x000DD88C File Offset: 0x000DBA8C
		[__DynamicallyInvokable]
		protected override void InsertItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			if (keyForItem != null)
			{
				this.AddKey(keyForItem, item);
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000DD8BC File Offset: 0x000DBABC
		[__DynamicallyInvokable]
		protected override void RemoveItem(int index)
		{
			TKey keyForItem = this.GetKeyForItem(base.Items[index]);
			if (keyForItem != null)
			{
				this.RemoveKey(keyForItem);
			}
			base.RemoveItem(index);
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000DD8F4 File Offset: 0x000DBAF4
		[__DynamicallyInvokable]
		protected override void SetItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			TKey keyForItem2 = this.GetKeyForItem(base.Items[index]);
			if (this.comparer.Equals(keyForItem2, keyForItem))
			{
				if (keyForItem != null && this.dict != null)
				{
					this.dict[keyForItem] = item;
				}
			}
			else
			{
				if (keyForItem != null)
				{
					this.AddKey(keyForItem, item);
				}
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000DD974 File Offset: 0x000DBB74
		private void AddKey(TKey key, TItem item)
		{
			if (this.dict != null)
			{
				this.dict.Add(key, item);
				return;
			}
			if (this.keyCount == this.threshold)
			{
				this.CreateDictionary();
				this.dict.Add(key, item);
				return;
			}
			if (this.Contains(key))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
			}
			this.keyCount++;
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000DD9D8 File Offset: 0x000DBBD8
		private void CreateDictionary()
		{
			this.dict = new Dictionary<TKey, TItem>(this.comparer);
			foreach (TItem titem in base.Items)
			{
				TKey keyForItem = this.GetKeyForItem(titem);
				if (keyForItem != null)
				{
					this.dict.Add(keyForItem, titem);
				}
			}
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000DDA4C File Offset: 0x000DBC4C
		private void RemoveKey(TKey key)
		{
			if (this.dict != null)
			{
				this.dict.Remove(key);
				return;
			}
			this.keyCount--;
		}

		// Token: 0x0400193C RID: 6460
		private const int defaultThreshold = 0;

		// Token: 0x0400193D RID: 6461
		private IEqualityComparer<TKey> comparer;

		// Token: 0x0400193E RID: 6462
		private Dictionary<TKey, TItem> dict;

		// Token: 0x0400193F RID: 6463
		private int keyCount;

		// Token: 0x04001940 RID: 6464
		private int threshold;
	}
}
