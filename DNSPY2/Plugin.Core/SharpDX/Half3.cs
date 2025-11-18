using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plugin.Core.SharpDX
{
	// Token: 0x0200003B RID: 59
	public struct Half3 : IEquatable<Half3>
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00002D52 File Offset: 0x00000F52
		public Half3(float float_0, float float_1, float float_2)
		{
			this.X = new Half(float_0);
			this.Y = new Half(float_1);
			this.Z = new Half(float_2);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00002D78 File Offset: 0x00000F78
		public Half3(ushort ushort_0, ushort ushort_1, ushort ushort_2)
		{
			this.X = new Half(ushort_0);
			this.Y = new Half(ushort_1);
			this.Z = new Half(ushort_2);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00002D9E File Offset: 0x00000F9E
		public static implicit operator Half3(Vector3 value)
		{
			return new Half3(value.X, value.Y, value.Z);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00002DB7 File Offset: 0x00000FB7
		public static implicit operator Vector3(Half3 value)
		{
			return new Vector3(value.X, value.Y, value.Z);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00002DDF File Offset: 0x00000FDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Half3 left, Half3 right)
		{
			return Half3.Equals(ref left, ref right);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00002DEA File Offset: 0x00000FEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool operator !=(Half3 left, Half3 right)
		{
			return !Half3.Equals(ref left, ref right);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00018F34 File Offset: 0x00017134
		public override int GetHashCode()
		{
			return (((this.X.GetHashCode() * 397) ^ this.Y.GetHashCode()) * 397) ^ this.Z.GetHashCode();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Equals(ref Half3 value1, ref Half3 value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public bool Equals(Half3 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00002E33 File Offset: 0x00001033
		public override bool Equals(object obj)
		{
			return obj is Half3 && this.Equals((Half3)obj);
		}

		// Token: 0x040000B1 RID: 177
		public Half X;

		// Token: 0x040000B2 RID: 178
		public Half Y;

		// Token: 0x040000B3 RID: 179
		public Half Z;
	}
}
