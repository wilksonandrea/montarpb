namespace Plugin.Core.SharpDX
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack=4)]
    public struct Vector3 : IEquatable<Vector3>, IFormattable
    {
        public static readonly Vector3 Zero;
        public static readonly Vector3 UnitX;
        public static readonly Vector3 UnitY;
        public static readonly Vector3 UnitZ;
        public static readonly Vector3 One;
        public static readonly Vector3 Up;
        public static readonly Vector3 Down;
        public static readonly Vector3 Left;
        public static readonly Vector3 Right;
        public static readonly Vector3 ForwardRH;
        public static readonly Vector3 ForwardLH;
        public static readonly Vector3 BackwardRH;
        public static readonly Vector3 BackwardLH;
        public float X;
        public float Y;
        public float Z;
        public Vector3(float float_0, float float_1, float float_2)
        {
            this.X = float_0;
            this.Y = float_1;
            this.Z = float_2;
        }

        public static float Distance(Vector3 value1, Vector3 value2)
        {
            float num = value1.Y - value2.Y;
            float num2 = value1.Z - value2.Z;
            float single1 = value1.X - value2.X;
            return (float) Math.Sqrt((double) (((single1 * single1) + (num * num)) + (num2 * num2)));
        }

        public Vector3(float[] float_0)
        {
            if (float_0 == null)
            {
                throw new ArgumentNullException("values");
            }
            if (float_0.Length != 3)
            {
                throw new ArgumentOutOfRangeException("values", "There must be three and only three input values for Vector3.");
            }
            this.X = float_0[0];
            this.Y = float_0[1];
            this.Z = float_0[2];
        }

        public bool IsNormalized =>
            MathUtil.IsOne(((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
        public bool IsZero =>
            (this.X == 0f) && ((this.Y == 0f) && (this.Z == 0f));
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.X;

                    case 1:
                        return this.Y;

                    case 2:
                        return this.Z;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.X = value;
                        return;

                    case 1:
                        this.Y = value;
                        return;

                    case 2:
                        this.Z = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
            }
        }
        public static Vector3 operator +(Vector3 left, Vector3 right) => 
            new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector3 operator *(Vector3 left, Vector3 right) => 
            new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        public static Vector3 operator +(Vector3 value) => 
            value;

        public static Vector3 operator -(Vector3 left, Vector3 right) => 
            new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Vector3 operator -(Vector3 value) => 
            new Vector3(-value.X, -value.Y, -value.Z);

        public static Vector3 operator *(float scale, Vector3 value) => 
            new Vector3(value.X * scale, value.Y * scale, value.Z * scale);

        public static Vector3 operator *(Vector3 value, float scale) => 
            new Vector3(value.X * scale, value.Y * scale, value.Z * scale);

        public static Vector3 operator /(Vector3 value, float scale) => 
            new Vector3(value.X / scale, value.Y / scale, value.Z / scale);

        public static Vector3 operator /(float scale, Vector3 value) => 
            new Vector3(scale / value.X, scale / value.Y, scale / value.Z);

        public static Vector3 operator /(Vector3 value, Vector3 scale) => 
            new Vector3(value.X / scale.X, value.Y / scale.Y, value.Z / scale.Z);

        public static Vector3 operator +(Vector3 value, float scalar) => 
            new Vector3(value.X + scalar, value.Y + scalar, value.Z + scalar);

        public static Vector3 operator +(float scalar, Vector3 value) => 
            new Vector3(scalar + value.X, scalar + value.Y, scalar + value.Z);

        public static Vector3 operator -(Vector3 value, float scalar) => 
            new Vector3(value.X - scalar, value.Y - scalar, value.Z - scalar);

        public static Vector3 operator -(float scalar, Vector3 value) => 
            new Vector3(scalar - value.X, scalar - value.Y, scalar - value.Z);

        public override int GetHashCode() => 
            (((this.X.GetHashCode() * 0x18d) ^ this.Y.GetHashCode()) * 0x18d) ^ this.Z.GetHashCode();

        public override string ToString() => 
            string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", this.X, this.Y, this.Z);

        public string ToString(string format) => 
            (format != null) ? string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", this.X.ToString(format, CultureInfo.CurrentCulture), this.Y.ToString(format, CultureInfo.CurrentCulture), this.Z.ToString(format, CultureInfo.CurrentCulture)) : this.ToString();

        public string ToString(IFormatProvider formatProvider) => 
            string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", this.X, this.Y, this.Z);

        public string ToString(string format, IFormatProvider formatProvider) => 
            (format != null) ? string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", this.X.ToString(format, formatProvider), this.Y.ToString(format, formatProvider), this.Z.ToString(format, formatProvider)) : this.ToString(formatProvider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Vector3 other) => 
            MathUtil.NearEqual(other.X, this.X) && (MathUtil.NearEqual(other.Y, this.Y) && MathUtil.NearEqual(other.Z, this.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3 other) => 
            this.Equals(ref other);

        public override bool Equals(object value)
        {
            if (!(value is Vector3))
            {
                return false;
            }
            Vector3 other = (Vector3) value;
            return this.Equals(ref other);
        }

        public static implicit operator RawVector3(Vector3 value)
        {
            Vector3Union union = new Vector3Union {
                Vec3 = value
            };
            return union.RawVec3;
        }

        public static implicit operator Vector3(RawVector3 value)
        {
            Vector3Union union = new Vector3Union {
                RawVec3 = value
            };
            return union.Vec3;
        }

        static Vector3()
        {
            Zero = new Vector3();
            UnitX = new Vector3(1f, 0f, 0f);
            UnitY = new Vector3(0f, 1f, 0f);
            UnitZ = new Vector3(0f, 0f, 1f);
            One = new Vector3(1f, 1f, 1f);
            Up = new Vector3(0f, 1f, 0f);
            Down = new Vector3(0f, -1f, 0f);
            Left = new Vector3(-1f, 0f, 0f);
            Right = new Vector3(1f, 0f, 0f);
            ForwardRH = new Vector3(0f, 0f, -1f);
            ForwardLH = new Vector3(0f, 0f, 1f);
            BackwardRH = new Vector3(0f, 0f, 1f);
            BackwardLH = new Vector3(0f, 0f, -1f);
        }
    }
}

