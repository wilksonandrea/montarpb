using System;

namespace System.Collections.Generic
{
	// Token: 0x020004BD RID: 1213
	[Serializable]
	internal class ObjectComparer<T> : Comparer<T>
	{
		// Token: 0x06003A37 RID: 14903 RVA: 0x000DDC6A File Offset: 0x000DBE6A
		public override int Compare(T x, T y)
		{
			return Comparer.Default.Compare(x, y);
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000DDC84 File Offset: 0x000DBE84
		public override bool Equals(object obj)
		{
			ObjectComparer<T> objectComparer = obj as ObjectComparer<T>;
			return objectComparer != null;
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000DDC9C File Offset: 0x000DBE9C
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000DDCAE File Offset: 0x000DBEAE
		public ObjectComparer()
		{
		}
	}
}
