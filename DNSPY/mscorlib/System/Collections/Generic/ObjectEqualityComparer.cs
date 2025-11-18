using System;

namespace System.Collections.Generic
{
	// Token: 0x020004C3 RID: 1219
	[Serializable]
	internal class ObjectEqualityComparer<T> : EqualityComparer<T>
	{
		// Token: 0x06003A8B RID: 14987 RVA: 0x000DF106 File Offset: 0x000DD306
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000DF139 File Offset: 0x000DD339
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000DF154 File Offset: 0x000DD354
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

		// Token: 0x06003A8E RID: 14990 RVA: 0x000DF1C8 File Offset: 0x000DD3C8
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

		// Token: 0x06003A8F RID: 14991 RVA: 0x000DF23C File Offset: 0x000DD43C
		public override bool Equals(object obj)
		{
			ObjectEqualityComparer<T> objectEqualityComparer = obj as ObjectEqualityComparer<T>;
			return objectEqualityComparer != null;
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x000DF254 File Offset: 0x000DD454
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000DF266 File Offset: 0x000DD466
		public ObjectEqualityComparer()
		{
		}
	}
}
