using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plugin.Core.SharpDX
{
	// Token: 0x02000040 RID: 64
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00002E7F File Offset: 0x0000107F
		public Vector3(float float_0, float float_1, float float_2)
		{
			this.X = float_0;
			this.Y = float_1;
			this.Z = float_2;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0001939C File Offset: 0x0001759C
		public static float Distance(Vector3 value1, Vector3 value2)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return (float)Math.Sqrt((double)(num * num + num2 * num2 + num3 * num3));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000193E4 File Offset: 0x000175E4
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00002E96 File Offset: 0x00001096
		public bool IsNormalized
		{
			get
			{
				return MathUtil.IsOne(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00002EC6 File Offset: 0x000010C6
		public bool IsZero
		{
			get
			{
				return this.X == 0f && this.Y == 0f && this.Z == 0f;
			}
		}

		// Token: 0x17000006 RID: 6
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
				default:
					throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
				}
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
				default:
					throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00002F64 File Offset: 0x00001164
		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00002F92 File Offset: 0x00001192
		public static Vector3 operator *(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00002FC0 File Offset: 0x000011C0
		public static Vector3 operator +(Vector3 value)
		{
			return value;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00002FC3 File Offset: 0x000011C3
		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00002FF1 File Offset: 0x000011F1
		public static Vector3 operator -(Vector3 value)
		{
			return new Vector3(-value.X, -value.Y, -value.Z);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000300D File Offset: 0x0000120D
		public static Vector3 operator *(float scale, Vector3 value)
		{
			return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000302C File Offset: 0x0000122C
		public static Vector3 operator *(Vector3 value, float scale)
		{
			return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000304B File Offset: 0x0000124B
		public static Vector3 operator /(Vector3 value, float scale)
		{
			return new Vector3(value.X / scale, value.Y / scale, value.Z / scale);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000306A File Offset: 0x0000126A
		public static Vector3 operator /(float scale, Vector3 value)
		{
			return new Vector3(scale / value.X, scale / value.Y, scale / value.Z);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00003089 File Offset: 0x00001289
		public static Vector3 operator /(Vector3 value, Vector3 scale)
		{
			return new Vector3(value.X / scale.X, value.Y / scale.Y, value.Z / scale.Z);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000030B7 File Offset: 0x000012B7
		public static Vector3 operator +(Vector3 value, float scalar)
		{
			return new Vector3(value.X + scalar, value.Y + scalar, value.Z + scalar);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000030D6 File Offset: 0x000012D6
		public static Vector3 operator +(float scalar, Vector3 value)
		{
			return new Vector3(scalar + value.X, scalar + value.Y, scalar + value.Z);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000030F5 File Offset: 0x000012F5
		public static Vector3 operator -(Vector3 value, float scalar)
		{
			return new Vector3(value.X - scalar, value.Y - scalar, value.Z - scalar);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00003114 File Offset: 0x00001314
		public static Vector3 operator -(float scalar, Vector3 value)
		{
			return new Vector3(scalar - value.X, scalar - value.Y, scalar - value.Z);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00003133 File Offset: 0x00001333
		public override int GetHashCode()
		{
			return (((this.X.GetHashCode() * 397) ^ this.Y.GetHashCode()) * 397) ^ this.Z.GetHashCode();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00003164 File Offset: 0x00001364
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", this.X, this.Y, this.Z);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00019430 File Offset: 0x00017630
		public string ToString(string format)
		{
			if (format == null)
			{
				return this.ToString();
			}
			return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", this.X.ToString(format, CultureInfo.CurrentCulture), this.Y.ToString(format, CultureInfo.CurrentCulture), this.Z.ToString(format, CultureInfo.CurrentCulture));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00003196 File Offset: 0x00001396
		public string ToString(IFormatProvider formatProvider)
		{
			return string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", this.X, this.Y, this.Z);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000031C4 File Offset: 0x000013C4
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (format == null)
			{
				return this.ToString(formatProvider);
			}
			return string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", this.X.ToString(format, formatProvider), this.Y.ToString(format, formatProvider), this.Z.ToString(format, formatProvider));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00003203 File Offset: 0x00001403
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(ref Vector3 other)
		{
			return MathUtil.NearEqual(other.X, this.X) && MathUtil.NearEqual(other.Y, this.Y) && MathUtil.NearEqual(other.Z, this.Z);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000323E File Offset: 0x0000143E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector3 other)
		{
			return this.Equals(ref other);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00019490 File Offset: 0x00017690
		public override bool Equals(object value)
		{
			if (!(value is Vector3))
			{
				return false;
			}
			Vector3 vector = (Vector3)value;
			return this.Equals(ref vector);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000194B8 File Offset: 0x000176B8
		public static implicit operator RawVector3(Vector3 value)
		{
			return new Vector3Union
			{
				Vec3 = value
			}.RawVec3;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000194DC File Offset: 0x000176DC
		public static implicit operator Vector3(RawVector3 value)
		{
			return new Vector3Union
			{
				RawVec3 = value
			}.Vec3;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00019500 File Offset: 0x00017700
		// Note: this type is marked as 'beforefieldinit'.
		static Vector3()
		{
		}

		// Token: 0x040000C3 RID: 195
		public static readonly Vector3 Zero = default(Vector3);

		// Token: 0x040000C4 RID: 196
		public static readonly Vector3 UnitX = new Vector3(1f, 0f, 0f);

		// Token: 0x040000C5 RID: 197
		public static readonly Vector3 UnitY = new Vector3(0f, 1f, 0f);

		// Token: 0x040000C6 RID: 198
		public static readonly Vector3 UnitZ = new Vector3(0f, 0f, 1f);

		// Token: 0x040000C7 RID: 199
		public static readonly Vector3 One = new Vector3(1f, 1f, 1f);

		// Token: 0x040000C8 RID: 200
		public static readonly Vector3 Up = new Vector3(0f, 1f, 0f);

		// Token: 0x040000C9 RID: 201
		public static readonly Vector3 Down = new Vector3(0f, -1f, 0f);

		// Token: 0x040000CA RID: 202
		public static readonly Vector3 Left = new Vector3(-1f, 0f, 0f);

		// Token: 0x040000CB RID: 203
		public static readonly Vector3 Right = new Vector3(1f, 0f, 0f);

		// Token: 0x040000CC RID: 204
		public static readonly Vector3 ForwardRH = new Vector3(0f, 0f, -1f);

		// Token: 0x040000CD RID: 205
		public static readonly Vector3 ForwardLH = new Vector3(0f, 0f, 1f);

		// Token: 0x040000CE RID: 206
		public static readonly Vector3 BackwardRH = new Vector3(0f, 0f, 1f);

		// Token: 0x040000CF RID: 207
		public static readonly Vector3 BackwardLH = new Vector3(0f, 0f, -1f);

		// Token: 0x040000D0 RID: 208
		public float X;

		// Token: 0x040000D1 RID: 209
		public float Y;

		// Token: 0x040000D2 RID: 210
		public float Z;
	}
}
