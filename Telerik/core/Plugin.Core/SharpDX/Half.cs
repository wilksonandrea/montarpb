using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Plugin.Core.SharpDX
{
	public struct Half
	{
		private ushort ushort_0;

		public const int PrecisionDigits = 3;

		public const int MantissaBits = 11;

		public const int MaximumDecimalExponent = 4;

		public const int MaximumBinaryExponent = 15;

		public const int MinimumDecimalExponent = -4;

		public const int MinimumBinaryExponent = -14;

		public const int ExponentRadix = 2;

		public const int AdditionRounding = 1;

		public readonly static float Epsilon;

		public readonly static float MaxValue;

		public readonly static float MinValue;

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

		static Half()
		{
			Half.Epsilon = 0.0004887581f;
			Half.MaxValue = 65504f;
			Half.MinValue = 6.103516E-05f;
		}

		public Half(ushort ushort_1)
		{
			this.ushort_0 = ushort_1;
		}

		public Half(float float_0)
		{
			this.ushort_0 = Class9.smethod_0(float_0);
		}

		public static float[] ConvertToFloat(Half[] values)
		{
			float[] singleArray = new float[(int)values.Length];
			for (int i = 0; i < (int)singleArray.Length; i++)
			{
				singleArray[i] = Class9.smethod_1(values[i].ushort_0);
			}
			return singleArray;
		}

		public static Half[] ConvertToHalf(float[] values)
		{
			Half[] half = new Half[(int)values.Length];
			for (int i = 0; i < (int)half.Length; i++)
			{
				half[i] = new Half(values[i]);
			}
			return half;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Equals(ref Half value1, ref Half value2)
		{
			return value1.ushort_0 == value2.ushort_0;
		}

		public bool Equals(Half other)
		{
			return other.ushort_0 == this.ushort_0;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Half))
			{
				return false;
			}
			return this.Equals((Half)obj);
		}

		public override int GetHashCode()
		{
			ushort ushort0 = this.ushort_0;
			return ushort0 * 3 / 2 ^ ushort0;
		}

		public static bool operator ==(Half left, Half right)
		{
			return left.ushort_0 == right.ushort_0;
		}

		public static implicit operator Half(float Value)
		{
			return new Half(Value);
		}

		public static implicit operator Single(Half Value)
		{
			return Class9.smethod_1(Value.ushort_0);
		}

		public static bool operator !=(Half left, Half right)
		{
			return left.ushort_0 != right.ushort_0;
		}

		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}
	}
}