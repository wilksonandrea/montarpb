using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x020000FB RID: 251
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Int32 : IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int>
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x0002F5E0 File Offset: 0x0002D7E0
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is int))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt32"));
			}
			int num = (int)value;
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

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002F620 File Offset: 0x0002D820
		[__DynamicallyInvokable]
		public int CompareTo(int value)
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

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002F631 File Offset: 0x0002D831
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is int && this == (int)obj;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0002F647 File Offset: 0x0002D847
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(int obj)
		{
			return this == obj;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002F64E File Offset: 0x0002D84E
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0002F652 File Offset: 0x0002D852
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatInt32(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002F661 File Offset: 0x0002D861
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatInt32(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002F670 File Offset: 0x0002D870
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002F680 File Offset: 0x0002D880
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002F690 File Offset: 0x0002D890
		[__DynamicallyInvokable]
		public static int Parse(string s)
		{
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0002F69E File Offset: 0x0002D89E
		[__DynamicallyInvokable]
		public static int Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt32(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002F6B2 File Offset: 0x0002D8B2
		[__DynamicallyInvokable]
		public static int Parse(string s, IFormatProvider provider)
		{
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002F6C1 File Offset: 0x0002D8C1
		[__DynamicallyInvokable]
		public static int Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002F6D6 File Offset: 0x0002D8D6
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out int result)
		{
			return Number.TryParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002F6E5 File Offset: 0x0002D8E5
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out int result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002F6FB File Offset: 0x0002D8FB
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002F6FF File Offset: 0x0002D8FF
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002F708 File Offset: 0x0002D908
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002F711 File Offset: 0x0002D911
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002F71A File Offset: 0x0002D91A
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002F723 File Offset: 0x0002D923
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002F72C File Offset: 0x0002D92C
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002F735 File Offset: 0x0002D935
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002F739 File Offset: 0x0002D939
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002F742 File Offset: 0x0002D942
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002F74B File Offset: 0x0002D94B
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002F754 File Offset: 0x0002D954
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002F75D File Offset: 0x0002D95D
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002F766 File Offset: 0x0002D966
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002F76F File Offset: 0x0002D96F
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Int32", "DateTime" }));
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002F796 File Offset: 0x0002D996
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040005A2 RID: 1442
		internal int m_value;

		// Token: 0x040005A3 RID: 1443
		[__DynamicallyInvokable]
		public const int MaxValue = 2147483647;

		// Token: 0x040005A4 RID: 1444
		[__DynamicallyInvokable]
		public const int MinValue = -2147483648;
	}
}
