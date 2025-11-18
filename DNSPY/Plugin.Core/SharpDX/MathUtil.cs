using System;

namespace Plugin.Core.SharpDX
{
	// Token: 0x0200003E RID: 62
	public static class MathUtil
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0001934C File Offset: 0x0001754C
		public static bool NearEqual(float A, float B)
		{
			if (MathUtil.IsZero(A - B))
			{
				return true;
			}
			byte[] bytes = BitConverter.GetBytes(A);
			byte[] bytes2 = BitConverter.GetBytes(B);
			int num = BitConverter.ToInt32(bytes, 0);
			int num2 = BitConverter.ToInt32(bytes2, 0);
			return num < 0 == num2 < 0 && Math.Abs(num - num2) <= 4;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00002E4B File Offset: 0x0000104B
		public static bool IsZero(float A)
		{
			return Math.Abs(A) < 1E-06f;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00002E5A File Offset: 0x0000105A
		public static bool IsOne(float A)
		{
			return MathUtil.IsZero(A - 1f);
		}

		// Token: 0x040000BB RID: 187
		public const float ZeroTolerance = 1E-06f;

		// Token: 0x040000BC RID: 188
		public const float Pi = 3.14159274f;

		// Token: 0x040000BD RID: 189
		public const float TwoPi = 6.28318548f;

		// Token: 0x040000BE RID: 190
		public const float PiOverTwo = 1.57079637f;

		// Token: 0x040000BF RID: 191
		public const float PiOverFour = 0.7853982f;
	}
}
