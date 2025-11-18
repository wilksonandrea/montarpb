namespace Plugin.Core.SharpDX
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Half3 : IEquatable<Half3>
    {
        public Half X;
        public Half Y;
        public Half Z;
        public Half3(float float_0, float float_1, float float_2)
        {
            this.X = new Half(float_0);
            this.Y = new Half(float_1);
            this.Z = new Half(float_2);
        }

        public Half3(ushort ushort_0, ushort ushort_1, ushort ushort_2)
        {
            this.X = new Half(ushort_0);
            this.Y = new Half(ushort_1);
            this.Z = new Half(ushort_2);
        }

        public static implicit operator Half3(Vector3 value) => 
            new Half3(value.X, value.Y, value.Z);

        public static implicit operator Vector3(Half3 value) => 
            new Vector3((float) value.X, (float) value.Y, (float) value.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Half3 left, Half3 right) => 
            Equals(ref left, ref right);

        [return: MarshalAs(UnmanagedType.U1)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Half3 left, Half3 right) => 
            !Equals(ref left, ref right);

        public override int GetHashCode() => 
            (((this.X.GetHashCode() * 0x18d) ^ this.Y.GetHashCode()) * 0x18d) ^ this.Z.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(ref Half3 value1, ref Half3 value2) => 
            (value1.X == value2.X) && ((value1.Y == value2.Y) && (value1.Z == value2.Z));

        public bool Equals(Half3 other) => 
            (this.X == other.X) && ((this.Y == other.Y) && (this.Z == other.Z));

        public override bool Equals(object obj) => 
            (obj is Half3) ? this.Equals((Half3) obj) : false;
    }
}

