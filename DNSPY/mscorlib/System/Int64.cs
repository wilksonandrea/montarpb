using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x020000FC RID: 252
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Int64 : IComparable, IFormattable, IConvertible, IComparable<long>, IEquatable<long>
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x0002F7A8 File Offset: 0x0002D9A8
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is long))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt64"));
			}
			long num = (long)value;
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

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002F7E8 File Offset: 0x0002D9E8
		[__DynamicallyInvokable]
		public int CompareTo(long value)
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

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002F7F9 File Offset: 0x0002D9F9
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is long && this == (long)obj;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0002F80F File Offset: 0x0002DA0F
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(long obj)
		{
			return this == obj;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002F816 File Offset: 0x0002DA16
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0002F822 File Offset: 0x0002DA22
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatInt64(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0002F831 File Offset: 0x0002DA31
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt64(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0002F841 File Offset: 0x0002DA41
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatInt64(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0002F850 File Offset: 0x0002DA50
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt64(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0002F860 File Offset: 0x0002DA60
		[__DynamicallyInvokable]
		public static long Parse(string s)
		{
			return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0002F86E File Offset: 0x0002DA6E
		[__DynamicallyInvokable]
		public static long Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0002F882 File Offset: 0x0002DA82
		[__DynamicallyInvokable]
		public static long Parse(string s, IFormatProvider provider)
		{
			return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0002F891 File Offset: 0x0002DA91
		[__DynamicallyInvokable]
		public static long Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0002F8A6 File Offset: 0x0002DAA6
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out long result)
		{
			return Number.TryParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0002F8B5 File Offset: 0x0002DAB5
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out long result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0002F8CB File Offset: 0x0002DACB
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int64;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0002F8CF File Offset: 0x0002DACF
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0002F8D8 File Offset: 0x0002DAD8
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0002F8E1 File Offset: 0x0002DAE1
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0002F8EA File Offset: 0x0002DAEA
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0002F8F3 File Offset: 0x0002DAF3
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0002F8FC File Offset: 0x0002DAFC
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0002F905 File Offset: 0x0002DB05
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0002F90E File Offset: 0x0002DB0E
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0002F917 File Offset: 0x0002DB17
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0002F91B File Offset: 0x0002DB1B
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0002F924 File Offset: 0x0002DB24
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0002F92D File Offset: 0x0002DB2D
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0002F936 File Offset: 0x0002DB36
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0002F93F File Offset: 0x0002DB3F
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Int64", "DateTime" }));
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0002F966 File Offset: 0x0002DB66
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040005A5 RID: 1445
		internal long m_value;

		// Token: 0x040005A6 RID: 1446
		[__DynamicallyInvokable]
		public const long MaxValue = 9223372036854775807L;

		// Token: 0x040005A7 RID: 1447
		[__DynamicallyInvokable]
		public const long MinValue = -9223372036854775808L;
	}
}
