using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.SharpDX
{
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Equals(ref Half3 value1, ref Half3 value2)
		{
			if (!(value1.X == value2.X) || !(value1.Y == value2.Y))
			{
				return false;
			}
			return value1.Z == value2.Z;
		}

		public bool Equals(Half3 other)
		{
			if (!(this.X == other.X) || !(this.Y == other.Y))
			{
				return false;
			}
			return this.Z == other.Z;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Half3))
			{
				return false;
			}
			return this.Equals((Half3)obj);
		}

		public override int GetHashCode()
		{
			return (this.X.GetHashCode() * 397 ^ this.Y.GetHashCode()) * 397 ^ this.Z.GetHashCode();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Half3 left, Half3 right)
		{
			return Half3.Equals(ref left, ref right);
		}

		public static implicit operator Half3(Vector3 value)
		{
			return new Half3(value.X, value.Y, value.Z);
		}

		public static implicit operator Vector3(Half3 value)
		{
			return new Vector3(value.X, value.Y, value.Z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Half3 left, Half3 right)
		{
			return !Half3.Equals(ref left, ref right);
		}
	}
}