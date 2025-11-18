using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000064 RID: 100
	[__DynamicallyInvokable]
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00009032 File Offset: 0x00007232
		[__DynamicallyInvokable]
		public T1 Item1
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000903A File Offset: 0x0000723A
		[__DynamicallyInvokable]
		public T2 Item2
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00009042 File Offset: 0x00007242
		[__DynamicallyInvokable]
		public T3 Item3
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000904A File Offset: 0x0000724A
		[__DynamicallyInvokable]
		public T4 Item4
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00009052 File Offset: 0x00007252
		[__DynamicallyInvokable]
		public T5 Item5
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000905A File Offset: 0x0000725A
		[__DynamicallyInvokable]
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00009087 File Offset: 0x00007287
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00009098 File Offset: 0x00007298
		[__DynamicallyInvokable]
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4)) && comparer.Equals(this.m_Item5, tuple.m_Item5);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000914C File Offset: 0x0000734C
		[__DynamicallyInvokable]
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000915C File Offset: 0x0000735C
		[__DynamicallyInvokable]
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
			if (tuple == null)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_TupleIncorrectType", new object[] { base.GetType().ToString() }), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item2, tuple.m_Item2);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item3, tuple.m_Item3);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item4, tuple.m_Item4);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item5, tuple.m_Item5);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00009247 File Offset: 0x00007447
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00009254 File Offset: 0x00007454
		[__DynamicallyInvokable]
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000092BB File Offset: 0x000074BB
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000092C4 File Offset: 0x000074C4
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000092EC File Offset: 0x000074EC
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(", ");
			sb.Append(this.m_Item5);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00009395 File Offset: 0x00007595
		int ITuple.Length
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000056 RID: 86
		object ITuple.this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.Item1;
				case 1:
					return this.Item2;
				case 2:
					return this.Item3;
				case 3:
					return this.Item4;
				case 4:
					return this.Item5;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x04000245 RID: 581
		private readonly T1 m_Item1;

		// Token: 0x04000246 RID: 582
		private readonly T2 m_Item2;

		// Token: 0x04000247 RID: 583
		private readonly T3 m_Item3;

		// Token: 0x04000248 RID: 584
		private readonly T4 m_Item4;

		// Token: 0x04000249 RID: 585
		private readonly T5 m_Item5;
	}
}
