using System;

namespace System.Collections.Generic
{
	// Token: 0x020004BB RID: 1211
	[Serializable]
	internal class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x06003A2F RID: 14895 RVA: 0x000DDB97 File Offset: 0x000DBD97
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000DDBC8 File Offset: 0x000DBDC8
		public override bool Equals(object obj)
		{
			GenericComparer<T> genericComparer = obj as GenericComparer<T>;
			return genericComparer != null;
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000DDBE0 File Offset: 0x000DBDE0
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000DDBF2 File Offset: 0x000DBDF2
		public GenericComparer()
		{
		}
	}
}
