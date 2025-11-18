using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plugin.Core.SharpDX;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Vector3 : IEquatable<Vector3>, IFormattable
{
	public static readonly Vector3 Zero = default(Vector3);

	public static readonly Vector3 UnitX = new Vector3(1f, 0f, 0f);

	public static readonly Vector3 UnitY = new Vector3(0f, 1f, 0f);

	public static readonly Vector3 UnitZ = new Vector3(0f, 0f, 1f);

	public static readonly Vector3 One = new Vector3(1f, 1f, 1f);

	public static readonly Vector3 Up = new Vector3(0f, 1f, 0f);

	public static readonly Vector3 Down = new Vector3(0f, -1f, 0f);

	public static readonly Vector3 Left = new Vector3(-1f, 0f, 0f);

	public static readonly Vector3 Right = new Vector3(1f, 0f, 0f);

	public static readonly Vector3 ForwardRH = new Vector3(0f, 0f, -1f);

	public static readonly Vector3 ForwardLH = new Vector3(0f, 0f, 1f);

	public static readonly Vector3 BackwardRH = new Vector3(0f, 0f, 1f);

	public static readonly Vector3 BackwardLH = new Vector3(0f, 0f, -1f);

	public float X;

	public float Y;

	public float Z;

	public bool IsNormalized => MathUtil.IsOne(X * X + Y * Y + Z * Z);

	public bool IsZero
	{
		get
		{
			if (X == 0f && Y == 0f)
			{
				return Z == 0f;
			}
			return false;
		}
	}

	public float this[int index]
	{
		get
		{
			return index switch
			{
				0 => X, 
				1 => Y, 
				2 => Z, 
				_ => throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive."), 
			};
		}
		set
		{
			switch (index)
			{
			default:
				throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
			case 0:
				X = value;
				break;
			case 1:
				Y = value;
				break;
			case 2:
				Z = value;
				break;
			}
		}
	}

	public Vector3(float float_0, float float_1, float float_2)
	{
		X = float_0;
		Y = float_1;
		Z = float_2;
	}

	public static float Distance(Vector3 value1, Vector3 value2)
	{
		float num = value1.X - value2.X;
		float num2 = value1.Y - value2.Y;
		float num3 = value1.Z - value2.Z;
		return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
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
		X = float_0[0];
		Y = float_0[1];
		Z = float_0[2];
	}

	public static Vector3 operator +(Vector3 left, Vector3 right)
	{
		return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	}

	public static Vector3 operator *(Vector3 left, Vector3 right)
	{
		return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
	}

	public static Vector3 operator +(Vector3 value)
	{
		return value;
	}

	public static Vector3 operator -(Vector3 left, Vector3 right)
	{
		return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
	}

	public static Vector3 operator -(Vector3 value)
	{
		return new Vector3(0f - value.X, 0f - value.Y, 0f - value.Z);
	}

	public static Vector3 operator *(float scale, Vector3 value)
	{
		return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
	}

	public static Vector3 operator *(Vector3 value, float scale)
	{
		return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
	}

	public static Vector3 operator /(Vector3 value, float scale)
	{
		return new Vector3(value.X / scale, value.Y / scale, value.Z / scale);
	}

	public static Vector3 operator /(float scale, Vector3 value)
	{
		return new Vector3(scale / value.X, scale / value.Y, scale / value.Z);
	}

	public static Vector3 operator /(Vector3 value, Vector3 scale)
	{
		return new Vector3(value.X / scale.X, value.Y / scale.Y, value.Z / scale.Z);
	}

	public static Vector3 operator +(Vector3 value, float scalar)
	{
		return new Vector3(value.X + scalar, value.Y + scalar, value.Z + scalar);
	}

	public static Vector3 operator +(float scalar, Vector3 value)
	{
		return new Vector3(scalar + value.X, scalar + value.Y, scalar + value.Z);
	}

	public static Vector3 operator -(Vector3 value, float scalar)
	{
		return new Vector3(value.X - scalar, value.Y - scalar, value.Z - scalar);
	}

	public static Vector3 operator -(float scalar, Vector3 value)
	{
		return new Vector3(scalar - value.X, scalar - value.Y, scalar - value.Z);
	}

	public override int GetHashCode()
	{
		return (((X.GetHashCode() * 397) ^ Y.GetHashCode()) * 397) ^ Z.GetHashCode();
	}

	public override string ToString()
	{
		return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", X, Y, Z);
	}

	public string ToString(string format)
	{
		if (format == null)
		{
			return ToString();
		}
		return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", X.ToString(format, CultureInfo.CurrentCulture), Y.ToString(format, CultureInfo.CurrentCulture), Z.ToString(format, CultureInfo.CurrentCulture));
	}

	public string ToString(IFormatProvider formatProvider)
	{
		return string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", X, Y, Z);
	}

	public string ToString(string format, IFormatProvider formatProvider)
	{
		if (format == null)
		{
			return ToString(formatProvider);
		}
		return string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", X.ToString(format, formatProvider), Y.ToString(format, formatProvider), Z.ToString(format, formatProvider));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(ref Vector3 other)
	{
		if (MathUtil.NearEqual(other.X, X) && MathUtil.NearEqual(other.Y, Y))
		{
			return MathUtil.NearEqual(other.Z, Z);
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Vector3 other)
	{
		return Equals(ref other);
	}

	public override bool Equals(object value)
	{
		if (!(value is Vector3 other))
		{
			return false;
		}
		return Equals(ref other);
	}

	public static implicit operator RawVector3(Vector3 value)
	{
		Vector3Union vector3Union = default(Vector3Union);
		vector3Union.Vec3 = value;
		return vector3Union.RawVec3;
	}

	public static implicit operator Vector3(RawVector3 value)
	{
		Vector3Union vector3Union = default(Vector3Union);
		vector3Union.RawVec3 = value;
		return vector3Union.Vec3;
	}
}
