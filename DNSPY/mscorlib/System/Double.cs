using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x020000D7 RID: 215
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Double : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002A57E File Offset: 0x0002877E
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsInfinity(double d)
		{
			return (*(long*)(&d) & long.MaxValue) == 9218868437227405312L;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002A599 File Offset: 0x00028799
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool IsPositiveInfinity(double d)
		{
			return d == double.PositiveInfinity;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002A5AA File Offset: 0x000287AA
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool IsNegativeInfinity(double d)
		{
			return d == double.NegativeInfinity;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002A5BB File Offset: 0x000287BB
		[SecuritySafeCritical]
		internal unsafe static bool IsNegative(double d)
		{
			return (*(long*)(&d) & long.MinValue) == long.MinValue;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002A5D6 File Offset: 0x000287D6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsNaN(double d)
		{
			return (*(long*)(&d) & long.MaxValue) > 9218868437227405312L;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002A5F4 File Offset: 0x000287F4
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is double))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDouble"));
			}
			double num = (double)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			if (this == num)
			{
				return 0;
			}
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0002A650 File Offset: 0x00028850
		[__DynamicallyInvokable]
		public int CompareTo(double value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			if (this == value)
			{
				return 0;
			}
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0002A680 File Offset: 0x00028880
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (!(obj is double))
			{
				return false;
			}
			double num = (double)obj;
			return num == this || (double.IsNaN(num) && double.IsNaN(this));
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0002A6B6 File Offset: 0x000288B6
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator ==(double left, double right)
		{
			return left == right;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002A6BC File Offset: 0x000288BC
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator !=(double left, double right)
		{
			return left != right;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0002A6C5 File Offset: 0x000288C5
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <(double left, double right)
		{
			return left < right;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0002A6CB File Offset: 0x000288CB
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >(double left, double right)
		{
			return left > right;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002A6D1 File Offset: 0x000288D1
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <=(double left, double right)
		{
			return left <= right;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002A6DA File Offset: 0x000288DA
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >=(double left, double right)
		{
			return left >= right;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002A6E3 File Offset: 0x000288E3
		[__DynamicallyInvokable]
		public bool Equals(double obj)
		{
			return obj == this || (double.IsNaN(obj) && double.IsNaN(this));
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002A700 File Offset: 0x00028900
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetHashCode()
		{
			double num = this;
			if (num == 0.0)
			{
				return 0;
			}
			long num2 = *(long*)(&num);
			return (int)num2 ^ (int)(num2 >> 32);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002A72B File Offset: 0x0002892B
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0002A73A File Offset: 0x0002893A
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002A749 File Offset: 0x00028949
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0002A759 File Offset: 0x00028959
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0002A769 File Offset: 0x00028969
		[__DynamicallyInvokable]
		public static double Parse(string s)
		{
			return double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002A77B File Offset: 0x0002897B
		[__DynamicallyInvokable]
		public static double Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0002A78F File Offset: 0x0002898F
		[__DynamicallyInvokable]
		public static double Parse(string s, IFormatProvider provider)
		{
			return double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0002A7A2 File Offset: 0x000289A2
		[__DynamicallyInvokable]
		public static double Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0002A7B7 File Offset: 0x000289B7
		private static double Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			return Number.ParseDouble(s, style, info);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0002A7C1 File Offset: 0x000289C1
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out double result)
		{
			return double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0002A7D4 File Offset: 0x000289D4
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0002A7EC File Offset: 0x000289EC
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out double result)
		{
			if (s == null)
			{
				result = 0.0;
				return false;
			}
			if (!Number.TryParseDouble(s, style, info, out result))
			{
				string text = s.Trim();
				if (text.Equals(info.PositiveInfinitySymbol))
				{
					result = double.PositiveInfinity;
				}
				else if (text.Equals(info.NegativeInfinitySymbol))
				{
					result = double.NegativeInfinity;
				}
				else
				{
					if (!text.Equals(info.NaNSymbol))
					{
						return false;
					}
					result = double.NaN;
				}
			}
			return true;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0002A871 File Offset: 0x00028A71
		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0002A875 File Offset: 0x00028A75
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0002A87E File Offset: 0x00028A7E
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Double", "Char" }));
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0002A8A5 File Offset: 0x00028AA5
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0002A8AE File Offset: 0x00028AAE
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002A8B7 File Offset: 0x00028AB7
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0002A8C0 File Offset: 0x00028AC0
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0002A8C9 File Offset: 0x00028AC9
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0002A8D2 File Offset: 0x00028AD2
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002A8DB File Offset: 0x00028ADB
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0002A8E4 File Offset: 0x00028AE4
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0002A8ED File Offset: 0x00028AED
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0002A8F6 File Offset: 0x00028AF6
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0002A8FA File Offset: 0x00028AFA
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0002A903 File Offset: 0x00028B03
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Double", "DateTime" }));
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0002A92A File Offset: 0x00028B2A
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0002A93A File Offset: 0x00028B3A
		// Note: this type is marked as 'beforefieldinit'.
		static Double()
		{
		}

		// Token: 0x0400055F RID: 1375
		internal double m_value;

		// Token: 0x04000560 RID: 1376
		[__DynamicallyInvokable]
		public const double MinValue = -1.7976931348623157E+308;

		// Token: 0x04000561 RID: 1377
		[__DynamicallyInvokable]
		public const double MaxValue = 1.7976931348623157E+308;

		// Token: 0x04000562 RID: 1378
		[__DynamicallyInvokable]
		public const double Epsilon = 4.94065645841247E-324;

		// Token: 0x04000563 RID: 1379
		[__DynamicallyInvokable]
		public const double NegativeInfinity = double.NegativeInfinity;

		// Token: 0x04000564 RID: 1380
		[__DynamicallyInvokable]
		public const double PositiveInfinity = double.PositiveInfinity;

		// Token: 0x04000565 RID: 1381
		[__DynamicallyInvokable]
		public const double NaN = double.NaN;

		// Token: 0x04000566 RID: 1382
		internal static double NegativeZero = BitConverter.Int64BitsToDouble(long.MinValue);
	}
}
