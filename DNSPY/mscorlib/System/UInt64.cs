using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x02000152 RID: 338
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct UInt64 : IComparable, IFormattable, IConvertible, IComparable<ulong>, IEquatable<ulong>
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x0003E334 File Offset: 0x0003C534
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is ulong))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeUInt64"));
			}
			ulong num = (ulong)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0003E374 File Offset: 0x0003C574
		[__DynamicallyInvokable]
		public int CompareTo(ulong value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0003E385 File Offset: 0x0003C585
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ulong && this == (ulong)obj;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0003E39B File Offset: 0x0003C59B
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(ulong obj)
		{
			return this == obj;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0003E3A2 File Offset: 0x0003C5A2
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0003E3AE File Offset: 0x0003C5AE
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatUInt64(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0003E3BD File Offset: 0x0003C5BD
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt64(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0003E3CD File Offset: 0x0003C5CD
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatUInt64(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0003E3DC File Offset: 0x0003C5DC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt64(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0003E3EC File Offset: 0x0003C5EC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s)
		{
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0003E3FA File Offset: 0x0003C5FA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0003E40E File Offset: 0x0003C60E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s, IFormatProvider provider)
		{
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0003E41D File Offset: 0x0003C61D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0003E432 File Offset: 0x0003C632
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out ulong result)
		{
			return Number.TryParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0003E441 File Offset: 0x0003C641
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ulong result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseUInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0003E457 File Offset: 0x0003C657
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt64;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0003E45B File Offset: 0x0003C65B
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0003E464 File Offset: 0x0003C664
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0003E46D File Offset: 0x0003C66D
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0003E476 File Offset: 0x0003C676
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0003E47F File Offset: 0x0003C67F
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0003E488 File Offset: 0x0003C688
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0003E491 File Offset: 0x0003C691
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0003E49A File Offset: 0x0003C69A
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0003E4A3 File Offset: 0x0003C6A3
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0003E4AC File Offset: 0x0003C6AC
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0003E4B0 File Offset: 0x0003C6B0
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0003E4B9 File Offset: 0x0003C6B9
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0003E4C2 File Offset: 0x0003C6C2
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0003E4CB File Offset: 0x0003C6CB
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "UInt64", "DateTime" }));
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0003E4F2 File Offset: 0x0003C6F2
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040006F7 RID: 1783
		private ulong m_value;

		// Token: 0x040006F8 RID: 1784
		[__DynamicallyInvokable]
		public const ulong MaxValue = 18446744073709551615UL;

		// Token: 0x040006F9 RID: 1785
		[__DynamicallyInvokable]
		public const ulong MinValue = 0UL;
	}
}
