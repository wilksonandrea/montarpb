using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000063 RID: 99
	[__DynamicallyInvokable]
	[Serializable]
	public class Tuple<T1, T2, T3, T4> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00008CEF File Offset: 0x00006EEF
		[__DynamicallyInvokable]
		public T1 Item1
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00008CF7 File Offset: 0x00006EF7
		[__DynamicallyInvokable]
		public T2 Item2
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00008CFF File Offset: 0x00006EFF
		[__DynamicallyInvokable]
		public T3 Item3
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00008D07 File Offset: 0x00006F07
		[__DynamicallyInvokable]
		public T4 Item4
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00008D0F File Offset: 0x00006F0F
		[__DynamicallyInvokable]
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00008D34 File Offset: 0x00006F34
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00008D44 File Offset: 0x00006F44
		[__DynamicallyInvokable]
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3)) && comparer.Equals(this.m_Item4, tuple.m_Item4);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00008DDA File Offset: 0x00006FDA
		[__DynamicallyInvokable]
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00008DE8 File Offset: 0x00006FE8
		[__DynamicallyInvokable]
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
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
			return comparer.Compare(this.m_Item4, tuple.m_Item4);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00008EB1 File Offset: 0x000070B1
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00008EC0 File Offset: 0x000070C0
		[__DynamicallyInvokable]
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4));
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00008F16 File Offset: 0x00007116
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00008F20 File Offset: 0x00007120
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00008F48 File Offset: 0x00007148
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00008FD3 File Offset: 0x000071D3
		int ITuple.Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x1700004F RID: 79
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
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x04000241 RID: 577
		private readonly T1 m_Item1;

		// Token: 0x04000242 RID: 578
		private readonly T2 m_Item2;

		// Token: 0x04000243 RID: 579
		private readonly T3 m_Item3;

		// Token: 0x04000244 RID: 580
		private readonly T4 m_Item4;
	}
}
