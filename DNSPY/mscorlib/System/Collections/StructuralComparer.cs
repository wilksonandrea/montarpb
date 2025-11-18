using System;

namespace System.Collections
{
	// Token: 0x020004A9 RID: 1193
	[Serializable]
	internal class StructuralComparer : IComparer
	{
		// Token: 0x06003905 RID: 14597 RVA: 0x000DA358 File Offset: 0x000D8558
		public int Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				IStructuralComparable structuralComparable = x as IStructuralComparable;
				if (structuralComparable != null)
				{
					return structuralComparable.CompareTo(y, this);
				}
				return Comparer.Default.Compare(x, y);
			}
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000DA393 File Offset: 0x000D8593
		public StructuralComparer()
		{
		}
	}
}
