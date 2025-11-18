using System;

namespace System.Collections.Generic
{
	// Token: 0x020004C2 RID: 1218
	[Serializable]
	internal class NullableEqualityComparer<T> : EqualityComparer<T?> where T : struct, IEquatable<T>
	{
		// Token: 0x06003A84 RID: 14980 RVA: 0x000DEF9A File Offset: 0x000DD19A
		public override bool Equals(T? x, T? y)
		{
			if (x != null)
			{
				return y != null && x.value.Equals(y.value);
			}
			return y == null;
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000DEFD5 File Offset: 0x000DD1D5
		public override int GetHashCode(T? obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000DEFE4 File Offset: 0x000DD1E4
		internal override int IndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x000DF05C File Offset: 0x000DD25C
		internal override int LastIndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000DF0D4 File Offset: 0x000DD2D4
		public override bool Equals(object obj)
		{
			NullableEqualityComparer<T> nullableEqualityComparer = obj as NullableEqualityComparer<T>;
			return nullableEqualityComparer != null;
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000DF0EC File Offset: 0x000DD2EC
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000DF0FE File Offset: 0x000DD2FE
		public NullableEqualityComparer()
		{
		}
	}
}
