using System;

namespace System.Collections.Generic
{
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	internal class GenericEqualityComparer<T> : EqualityComparer<T> where T : IEquatable<T>
	{
		// Token: 0x06003A7D RID: 14973 RVA: 0x000DEE44 File Offset: 0x000DD044
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000DEE72 File Offset: 0x000DD072
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000DEE8C File Offset: 0x000DD08C
		internal override int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000DEEF8 File Offset: 0x000DD0F8
		internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000DEF68 File Offset: 0x000DD168
		public override bool Equals(object obj)
		{
			GenericEqualityComparer<T> genericEqualityComparer = obj as GenericEqualityComparer<T>;
			return genericEqualityComparer != null;
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000DEF80 File Offset: 0x000DD180
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000DEF92 File Offset: 0x000DD192
		public GenericEqualityComparer()
		{
		}
	}
}
