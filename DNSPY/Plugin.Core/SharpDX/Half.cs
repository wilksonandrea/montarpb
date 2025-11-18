using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Plugin.Core.SharpDX
{
	// Token: 0x0200003A RID: 58
	public struct Half
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00002CB3 File Offset: 0x00000EB3
		public Half(ushort ushort_1)
		{
			this.ushort_0 = ushort_1;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00002CBC File Offset: 0x00000EBC
		public Half(float float_0)
		{
			this.ushort_0 = Class9.smethod_0(float_0);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00002CCA File Offset: 0x00000ECA
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00002CB3 File Offset: 0x00000EB3
		public ushort RawValue
		{
			get
			{
				return this.ushort_0;
			}
			set
			{
				this.ushort_0 = value;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00018E80 File Offset: 0x00017080
		public static float[] ConvertToFloat(Half[] values)
		{
			float[] array = new float[values.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Class9.smethod_1(values[i].ushort_0);
			}
			return array;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00018EBC File Offset: 0x000170BC
		public static Half[] ConvertToHalf(float[] values)
		{
			Half[] array = new Half[values.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Half(values[i]);
			}
			return array;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00002CD2 File Offset: 0x00000ED2
		public static implicit operator Half(float Value)
		{
			return new Half(Value);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00002CDA File Offset: 0x00000EDA
		public static implicit operator float(Half Value)
		{
			return Class9.smethod_1(Value.ushort_0);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00002CE7 File Offset: 0x00000EE7
		public static bool operator ==(Half left, Half right)
		{
			return left.ushort_0 == right.ushort_0;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public static bool operator !=(Half left, Half right)
		{
			return left.ushort_0 != right.ushort_0;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00018EF0 File Offset: 0x000170F0
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00018F18 File Offset: 0x00017118
		public override int GetHashCode()
		{
			ushort num = this.ushort_0;
			return (int)((num * 3 / 2) ^ num);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00002CE7 File Offset: 0x00000EE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Equals(ref Half value1, ref Half value2)
		{
			return value1.ushort_0 == value2.ushort_0;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00002D0A File Offset: 0x00000F0A
		public bool Equals(Half other)
		{
			return other.ushort_0 == this.ushort_0;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00002D1A File Offset: 0x00000F1A
		public override bool Equals(object obj)
		{
			return obj is Half && this.Equals((Half)obj);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00002D32 File Offset: 0x00000F32
		static Half()
		{
		}

		// Token: 0x040000A5 RID: 165
		private ushort ushort_0;

		// Token: 0x040000A6 RID: 166
		public const int PrecisionDigits = 3;

		// Token: 0x040000A7 RID: 167
		public const int MantissaBits = 11;

		// Token: 0x040000A8 RID: 168
		public const int MaximumDecimalExponent = 4;

		// Token: 0x040000A9 RID: 169
		public const int MaximumBinaryExponent = 15;

		// Token: 0x040000AA RID: 170
		public const int MinimumDecimalExponent = -4;

		// Token: 0x040000AB RID: 171
		public const int MinimumBinaryExponent = -14;

		// Token: 0x040000AC RID: 172
		public const int ExponentRadix = 2;

		// Token: 0x040000AD RID: 173
		public const int AdditionRounding = 1;

		// Token: 0x040000AE RID: 174
		public static readonly float Epsilon = 0.0004887581f;

		// Token: 0x040000AF RID: 175
		public static readonly float MaxValue = 65504f;

		// Token: 0x040000B0 RID: 176
		public static readonly float MinValue = 6.103516E-05f;
	}
}
