using System;

namespace Plugin.Core.SharpDX;

public static class MathUtil
{
	public const float ZeroTolerance = 1E-06f;

	public const float Pi = (float)Math.PI;

	public const float TwoPi = (float)Math.PI * 2f;

	public const float PiOverTwo = (float)Math.PI / 2f;

	public const float PiOverFour = (float)Math.PI / 4f;

	public static bool NearEqual(float A, float B)
	{
		if (IsZero(A - B))
		{
			return true;
		}
		byte[] bytes = BitConverter.GetBytes(A);
		byte[] bytes2 = BitConverter.GetBytes(B);
		int num = BitConverter.ToInt32(bytes, 0);
		int num2 = BitConverter.ToInt32(bytes2, 0);
		if (num < 0 != num2 < 0)
		{
			return false;
		}
		return Math.Abs(num - num2) <= 4;
	}

	public static bool IsZero(float A)
	{
		return Math.Abs(A) < 1E-06f;
	}

	public static bool IsOne(float A)
	{
		return IsZero(A - 1f);
	}
}
