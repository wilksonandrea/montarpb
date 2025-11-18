using System;

namespace Plugin.Core.SharpDX
{
	public static class MathUtil
	{
		public const float ZeroTolerance = 1E-06f;

		public const float Pi = 3.14159274f;

		public const float TwoPi = 6.28318548f;

		public const float PiOverTwo = 1.57079637f;

		public const float PiOverFour = 0.7853982f;

		public static bool IsOne(float A)
		{
			return MathUtil.IsZero(A - 1f);
		}

		public static bool IsZero(float A)
		{
			return Math.Abs(A) < 1E-06f;
		}

		public static bool NearEqual(float A, float B)
		{
			if (MathUtil.IsZero(A - B))
			{
				return true;
			}
			byte[] bytes = BitConverter.GetBytes(A);
			byte[] numArray = BitConverter.GetBytes(B);
			int ınt32 = BitConverter.ToInt32(bytes, 0);
			int ınt321 = BitConverter.ToInt32(numArray, 0);
			if (ınt32 < 0 != ınt321 < 0)
			{
				return false;
			}
			return Math.Abs(ınt32 - ınt321) <= 4;
		}
	}
}