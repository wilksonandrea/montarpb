namespace Plugin.Core.SharpDX
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
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
        public Half(ushort ushort_1)
        {
            this.ushort_0 = ushort_1;
        }

        public Half(float float_0)
        {
            this.ushort_0 = Class9.smethod_0(float_0);
        }

        public ushort RawValue
        {
            get => 
                this.ushort_0;
            set => 
                this.ushort_0 = value;
        }
        public static float[] ConvertToFloat(Half[] values)
        {
            float[] numArray = new float[values.Length];
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = Class9.smethod_1(values[i].ushort_0);
            }
            return numArray;
        }

        public static Half[] ConvertToHalf(float[] values)
        {
            Half[] halfArray = new Half[values.Length];
            for (int i = 0; i < halfArray.Length; i++)
            {
                halfArray[i] = new Half(values[i]);
            }
            return halfArray;
        }

        public static implicit operator Half(float Value) => 
            new Half(Value);

        public static implicit operator float(Half Value) => 
            Class9.smethod_1(Value.ushort_0);

        public static bool operator ==(Half left, Half right) => 
            left.ushort_0 == right.ushort_0;

        public static bool operator !=(Half left, Half right) => 
            left.ushort_0 != right.ushort_0;

        public override string ToString() => 
            ((float) this).ToString(CultureInfo.CurrentCulture);

        public override int GetHashCode()
        {
            ushort num = this.ushort_0;
            return (((num * 3) / 2) ^ num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(ref Half value1, ref Half value2) => 
            value1.ushort_0 == value2.ushort_0;

        public bool Equals(Half other) => 
            other.ushort_0 == this.ushort_0;

        public override bool Equals(object obj) => 
            (obj is Half) ? this.Equals((Half) obj) : false;

        static Half()
        {
            Epsilon = 0.0004887581f;
            MaxValue = 65504f;
            MinValue = 6.103516E-05f;
        }
    }
}

