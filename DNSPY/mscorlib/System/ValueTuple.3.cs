using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<T1, T2> : IEquatable<ValueTuple<T1, T2>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2>>, IValueTupleInternal, ITuple
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0000A959 File Offset: 0x00008B59
		public ValueTuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000A969 File Offset: 0x00008B69
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2> && this.Equals((ValueTuple<T1, T2>)obj);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000A981 File Offset: 0x00008B81
		public bool Equals(ValueTuple<T1, T2> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2>))
			{
				return false;
			}
			ValueTuple<T1, T2> valueTuple = (ValueTuple<T1, T2>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000AA14 File Offset: 0x00008C14
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2>))
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", new object[] { base.GetType().ToString() }), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2>)other);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000AA70 File Offset: 0x00008C70
		public int CompareTo(ValueTuple<T1, T2> other)
		{
			int num = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T2>.Default.Compare(this.Item2, other.Item2);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2>))
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", new object[] { base.GetType().ToString() }), "other");
			}
			ValueTuple<T1, T2> valueTuple = (ValueTuple<T1, T2>)other;
			int num = comparer.Compare(this.Item1, valueTuple.Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item2, valueTuple.Item2);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000AB42 File Offset: 0x00008D42
		public override int GetHashCode()
		{
			return ValueTuple.CombineHashCodes(EqualityComparer<T1>.Default.GetHashCode(this.Item1), EqualityComparer<T2>.Default.GetHashCode(this.Item2));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000AB69 File Offset: 0x00008D69
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000AB72 File Offset: 0x00008D72
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000AB9B File Offset: 0x00008D9B
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "(";
			int num = 1;
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text = null;
					goto IL_45;
				}
			}
			text = ptr.ToString();
			IL_45:
			array[num] = text;
			array[2] = ", ";
			int num2 = 3;
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			string text2;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					text2 = null;
					goto IL_85;
				}
			}
			text2 = ptr2.ToString();
			IL_85:
			array[num2] = text2;
			array[4] = ")";
			return string.Concat(array);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000AC44 File Offset: 0x00008E44
		string IValueTupleInternal.ToStringEnd()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text = null;
					goto IL_35;
				}
			}
			text = ptr.ToString();
			IL_35:
			string text2 = ", ";
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			string text3;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					text3 = null;
					goto IL_6F;
				}
			}
			text3 = ptr2.ToString();
			IL_6F:
			return text + text2 + text3 + ")";
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000ACCA File Offset: 0x00008ECA
		int ITuple.Length
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000077 RID: 119
		object ITuple.this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.Item1;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.Item2;
			}
		}

		// Token: 0x04000260 RID: 608
		public T1 Item1;

		// Token: 0x04000261 RID: 609
		public T2 Item2;
	}
}
