using System;

namespace System.Collections.Generic
{
	// Token: 0x020004BC RID: 1212
	[Serializable]
	internal class NullableComparer<T> : Comparer<T?> where T : struct, IComparable<T>
	{
		// Token: 0x06003A33 RID: 14899 RVA: 0x000DDBFA File Offset: 0x000DBDFA
		public override int Compare(T? x, T? y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.value.CompareTo(y.value);
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

		// Token: 0x06003A34 RID: 14900 RVA: 0x000DDC38 File Offset: 0x000DBE38
		public override bool Equals(object obj)
		{
			NullableComparer<T> nullableComparer = obj as NullableComparer<T>;
			return nullableComparer != null;
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000DDC50 File Offset: 0x000DBE50
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000DDC62 File Offset: 0x000DBE62
		public NullableComparer()
		{
		}
	}
}
