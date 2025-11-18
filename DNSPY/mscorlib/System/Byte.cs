using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x020000B4 RID: 180
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Byte : IComparable, IFormattable, IConvertible, IComparable<byte>, IEquatable<byte>
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x0002167D File Offset: 0x0001F87D
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is byte))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeByte"));
			}
			return (int)(this - (byte)value);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000216A5 File Offset: 0x0001F8A5
		[__DynamicallyInvokable]
		public int CompareTo(byte value)
		{
			return (int)(this - value);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000216AB File Offset: 0x0001F8AB
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is byte && this == (byte)obj;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000216C1 File Offset: 0x0001F8C1
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(byte obj)
		{
			return this == obj;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000216C8 File Offset: 0x0001F8C8
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000216CC File Offset: 0x0001F8CC
		[__DynamicallyInvokable]
		public static byte Parse(string s)
		{
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000216DA File Offset: 0x0001F8DA
		[__DynamicallyInvokable]
		public static byte Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000216EE File Offset: 0x0001F8EE
		[__DynamicallyInvokable]
		public static byte Parse(string s, IFormatProvider provider)
		{
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000216FD File Offset: 0x0001F8FD
		[__DynamicallyInvokable]
		public static byte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00021714 File Offset: 0x0001F914
		private static byte Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"), ex);
			}
			if (num < 0 || num > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)num;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00021770 File Offset: 0x0001F970
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out byte result)
		{
			return byte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002177F File Offset: 0x0001F97F
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out byte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00021798 File Offset: 0x0001F998
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out byte result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if (num < 0 || num > 255)
			{
				return false;
			}
			result = (byte)num;
			return true;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000217C9 File Offset: 0x0001F9C9
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000217D8 File Offset: 0x0001F9D8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatInt32((int)this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000217E7 File Offset: 0x0001F9E7
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000217F7 File Offset: 0x0001F9F7
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00021807 File Offset: 0x0001FA07
		public TypeCode GetTypeCode()
		{
			return TypeCode.Byte;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002180A File Offset: 0x0001FA0A
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00021813 File Offset: 0x0001FA13
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002181C File Offset: 0x0001FA1C
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00021825 File Offset: 0x0001FA25
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00021829 File Offset: 0x0001FA29
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00021832 File Offset: 0x0001FA32
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002183B File Offset: 0x0001FA3B
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00021844 File Offset: 0x0001FA44
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002184D File Offset: 0x0001FA4D
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00021856 File Offset: 0x0001FA56
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002185F File Offset: 0x0001FA5F
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00021868 File Offset: 0x0001FA68
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00021871 File Offset: 0x0001FA71
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002187A File Offset: 0x0001FA7A
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Byte", "DateTime" }));
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000218A1 File Offset: 0x0001FAA1
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040003F4 RID: 1012
		private byte m_value;

		// Token: 0x040003F5 RID: 1013
		[__DynamicallyInvokable]
		public const byte MaxValue = 255;

		// Token: 0x040003F6 RID: 1014
		[__DynamicallyInvokable]
		public const byte MinValue = 0;
	}
}
