using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Plugin.Core.SharpDX
{
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		public readonly static Vector3 Zero;

		public readonly static Vector3 UnitX;

		public readonly static Vector3 UnitY;

		public readonly static Vector3 UnitZ;

		public readonly static Vector3 One;

		public readonly static Vector3 Up;

		public readonly static Vector3 Down;

		public readonly static Vector3 Left;

		public readonly static Vector3 Right;

		public readonly static Vector3 ForwardRH;

		public readonly static Vector3 ForwardLH;

		public readonly static Vector3 BackwardRH;

		public readonly static Vector3 BackwardLH;

		public float X;

		public float Y;

		public float Z;

		public bool IsNormalized
		{
			get
			{
				return MathUtil.IsOne(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
			}
		}

		public bool IsZero
		{
			get
			{
				if (this.X != 0f || this.Y != 0f)
				{
					return false;
				}
				return this.Z == 0f;
			}
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
					{
						return this.X;
					}
					case 1:
					{
						return this.Y;
					}
					case 2:
					{
						return this.Z;
					}
					default:
					{
						throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
					}
				}
			}
			set
			{
				switch (index)
				{
					case 0:
					{
						this.X = value;
						return;
					}
					case 1:
					{
						this.Y = value;
						return;
					}
					case 2:
					{
						this.Z = value;
						return;
					}
					default:
					{
						throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
					}
				}
			}
		}

		static Vector3()
		{
			Vector3.Zero = new Vector3();
			Vector3.UnitX = new Vector3(1f, 0f, 0f);
			Vector3.UnitY = new Vector3(0f, 1f, 0f);
			Vector3.UnitZ = new Vector3(0f, 0f, 1f);
			Vector3.One = new Vector3(1f, 1f, 1f);
			Vector3.Up = new Vector3(0f, 1f, 0f);
			Vector3.Down = new Vector3(0f, -1f, 0f);
			Vector3.Left = new Vector3(-1f, 0f, 0f);
			Vector3.Right = new Vector3(1f, 0f, 0f);
			Vector3.ForwardRH = new Vector3(0f, 0f, -1f);
			Vector3.ForwardLH = new Vector3(0f, 0f, 1f);
			Vector3.BackwardRH = new Vector3(0f, 0f, 1f);
			Vector3.BackwardLH = new Vector3(0f, 0f, -1f);
		}

		public Vector3(float float_0, float float_1, float float_2)
		{
			this.X = float_0;
			this.Y = float_1;
			this.Z = float_2;
		}

		public Vector3(float[] float_0)
		{
			if (float_0 == null)
			{
				throw new ArgumentNullException("values");
			}
			if ((int)float_0.Length != 3)
			{
				throw new ArgumentOutOfRangeException("values", "There must be three and only three input values for Vector3.");
			}
			this.X = float_0[0];
			this.Y = float_0[1];
			this.Z = float_0[2];
		}

		public static float Distance(Vector3 value1, Vector3 value2)
		{
			float x = value1.X - value2.X;
			float y = value1.Y - value2.Y;
			float z = value1.Z - value2.Z;
			return (float)Math.Sqrt((double)(x * x + y * y + z * z));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(ref Vector3 other)
		{
			if (!MathUtil.NearEqual(other.X, this.X) || !MathUtil.NearEqual(other.Y, this.Y))
			{
				return false;
			}
			return MathUtil.NearEqual(other.Z, this.Z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector3 other)
		{
			return this.Equals(ref other);
		}

		public override bool Equals(object value)
		{
			if (!(value is Vector3))
			{
				return false;
			}
			Vector3 vector3 = (Vector3)value;
			return this.Equals(ref vector3);
		}

		public override int GetHashCode()
		{
			return (this.X.GetHashCode() * 397 ^ this.Y.GetHashCode()) * 397 ^ this.Z.GetHashCode();
		}

		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		public static Vector3 operator +(Vector3 value, float scalar)
		{
			return new Vector3(value.X + scalar, value.Y + scalar, value.Z + scalar);
		}

		public static Vector3 operator +(float scalar, Vector3 value)
		{
			return new Vector3(scalar + value.X, scalar + value.Y, scalar + value.Z);
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

		public static implicit operator RawVector3(Vector3 value)
		{
			return (new Vector3Union()
			{
				Vec3 = value
			}).RawVec3;
		}

		public static implicit operator Vector3(RawVector3 value)
		{
			return (new Vector3Union()
			{
				RawVec3 = value
			}).Vec3;
		}

		public static Vector3 operator *(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		public static Vector3 operator *(float scale, Vector3 value)
		{
			return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
		}

		public static Vector3 operator *(Vector3 value, float scale)
		{
			return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
		}

		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		public static Vector3 operator -(Vector3 value, float scalar)
		{
			return new Vector3(value.X - scalar, value.Y - scalar, value.Z - scalar);
		}

		public static Vector3 operator -(float scalar, Vector3 value)
		{
			return new Vector3(scalar - value.X, scalar - value.Y, scalar - value.Z);
		}

		public static Vector3 operator -(Vector3 value)
		{
			return new Vector3(-value.X, -value.Y, -value.Z);
		}

		public static Vector3 operator +(Vector3 value)
		{
			return value;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", this.X, this.Y, this.Z);
		}

		public string ToString(string format)
		{
			if (format == null)
			{
				return this.ToString();
			}
			return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", this.X.ToString(format, CultureInfo.CurrentCulture), this.Y.ToString(format, CultureInfo.CurrentCulture), this.Z.ToString(format, CultureInfo.CurrentCulture));
		}

		public string ToString(IFormatProvider formatProvider)
		{
			return string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", this.X, this.Y, this.Z);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (format == null)
			{
				return this.ToString(formatProvider);
			}
			return string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", this.X.ToString(format, formatProvider), this.Y.ToString(format, formatProvider), this.Z.ToString(format, formatProvider));
		}
	}
}