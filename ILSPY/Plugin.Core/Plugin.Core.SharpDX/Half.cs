using System.Globalization;
using System.Runtime.CompilerServices;

namespace Plugin.Core.SharpDX;

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

	public static readonly float Epsilon;

	public static readonly float MaxValue;

	public static readonly float MinValue;

	public ushort RawValue
	{
		get
		{
			return ushort_0;
		}
		set
		{
			ushort_0 = value;
		}
	}

	public Half(ushort ushort_1)
	{
		ushort_0 = ushort_1;
	}

	public Half(float float_0)
	{
		ushort_0 = Class9.smethod_0(float_0);
	}

	public static float[] ConvertToFloat(Half[] values)
	{
		float[] array = new float[values.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = Class9.smethod_1(values[i].ushort_0);
		}
		return array;
	}

	public static Half[] ConvertToHalf(float[] values)
	{
		Half[] array = new Half[values.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new Half(values[i]);
		}
		return array;
	}

	public static implicit operator Half(float Value)
	{
		return new Half(Value);
	}

	public static implicit operator float(Half Value)
	{
		return Class9.smethod_1(Value.ushort_0);
	}

	public static bool operator ==(Half left, Half right)
	{
		return left.ushort_0 == right.ushort_0;
	}

	public static bool operator !=(Half left, Half right)
	{
		return left.ushort_0 != right.ushort_0;
	}

	public override string ToString()
	{
		return ((float)this).ToString(CultureInfo.CurrentCulture);
	}

	public override int GetHashCode()
	{
		ushort num = ushort_0;
		return (num * 3 / 2) ^ num;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Equals(ref Half value1, ref Half value2)
	{
		return value1.ushort_0 == value2.ushort_0;
	}

	public bool Equals(Half other)
	{
		return other.ushort_0 == ushort_0;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is Half))
		{
			return false;
		}
		return Equals((Half)obj);
	}

	static Half()
	{
		Epsilon = 0.0004887581f;
		MaxValue = 65504f;
		MinValue = 6.103516E-05f;
	}
}
