using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043C RID: 1084
	internal abstract class ConcurrentSetItem<KeyType, ItemType> where ItemType : ConcurrentSetItem<KeyType, ItemType>
	{
		// Token: 0x060035E3 RID: 13795
		public abstract int Compare(ItemType other);

		// Token: 0x060035E4 RID: 13796
		public abstract int Compare(KeyType key);

		// Token: 0x060035E5 RID: 13797 RVA: 0x000D1BAF File Offset: 0x000CFDAF
		protected ConcurrentSetItem()
		{
		}
	}
}
