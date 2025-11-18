namespace Plugin.Core.SharpDX
{
    using System;

    public static class MathUtil
    {
        public const float ZeroTolerance = 1E-06f;
        public const float Pi = 3.141593f;
        public const float TwoPi = 6.283185f;
        public const float PiOverTwo = 1.570796f;
        public const float PiOverFour = 0.7853982f;

        public static bool IsOne(float A) => 
            IsZero(A - 1f);

        public static bool IsZero(float A) => 
            Math.Abs(A) < 1E-06f;

        public static bool NearEqual(float A, float B)
        {
            if (IsZero(A - B))
            {
                return true;
            }
            int num = BitConverter.ToInt32(BitConverter.GetBytes(A), 0);
            int num2 = BitConverter.ToInt32(BitConverter.GetBytes(B), 0);
            return (((num < 0) == (num2 < 0)) ? (Math.Abs((int) (num - num2)) <= 4) : false);
        }
    }
}

