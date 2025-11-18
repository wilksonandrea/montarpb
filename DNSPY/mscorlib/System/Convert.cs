using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x020000CC RID: 204
	[__DynamicallyInvokable]
	public static class Convert
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x000251AC File Offset: 0x000233AC
		[__DynamicallyInvokable]
		public static TypeCode GetTypeCode(object value)
		{
			if (value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000251D0 File Offset: 0x000233D0
		public static bool IsDBNull(object value)
		{
			if (value == System.DBNull.Value)
			{
				return true;
			}
			IConvertible convertible = value as IConvertible;
			return convertible != null && convertible.GetTypeCode() == TypeCode.DBNull;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000251FC File Offset: 0x000233FC
		public static object ChangeType(object value, TypeCode typeCode)
		{
			return Convert.ChangeType(value, typeCode, Thread.CurrentThread.CurrentCulture);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00025210 File Offset: 0x00023410
		[__DynamicallyInvokable]
		public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider)
		{
			if (value == null && (typeCode == TypeCode.Empty || typeCode == TypeCode.String || typeCode == TypeCode.Object))
			{
				return null;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible == null)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_IConvertible"));
			}
			switch (typeCode)
			{
			case TypeCode.Empty:
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_Empty"));
			case TypeCode.Object:
				return value;
			case TypeCode.DBNull:
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_DBNull"));
			case TypeCode.Boolean:
				return convertible.ToBoolean(provider);
			case TypeCode.Char:
				return convertible.ToChar(provider);
			case TypeCode.SByte:
				return convertible.ToSByte(provider);
			case TypeCode.Byte:
				return convertible.ToByte(provider);
			case TypeCode.Int16:
				return convertible.ToInt16(provider);
			case TypeCode.UInt16:
				return convertible.ToUInt16(provider);
			case TypeCode.Int32:
				return convertible.ToInt32(provider);
			case TypeCode.UInt32:
				return convertible.ToUInt32(provider);
			case TypeCode.Int64:
				return convertible.ToInt64(provider);
			case TypeCode.UInt64:
				return convertible.ToUInt64(provider);
			case TypeCode.Single:
				return convertible.ToSingle(provider);
			case TypeCode.Double:
				return convertible.ToDouble(provider);
			case TypeCode.Decimal:
				return convertible.ToDecimal(provider);
			case TypeCode.DateTime:
				return convertible.ToDateTime(provider);
			case TypeCode.String:
				return convertible.ToString(provider);
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_UnknownTypeCode"));
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00025390 File Offset: 0x00023590
		internal static object DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			RuntimeType runtimeType = targetType as RuntimeType;
			if (runtimeType != null)
			{
				if (value.GetType() == targetType)
				{
					return value;
				}
				if (runtimeType == Convert.ConvertTypes[3])
				{
					return value.ToBoolean(provider);
				}
				if (runtimeType == Convert.ConvertTypes[4])
				{
					return value.ToChar(provider);
				}
				if (runtimeType == Convert.ConvertTypes[5])
				{
					return value.ToSByte(provider);
				}
				if (runtimeType == Convert.ConvertTypes[6])
				{
					return value.ToByte(provider);
				}
				if (runtimeType == Convert.ConvertTypes[7])
				{
					return value.ToInt16(provider);
				}
				if (runtimeType == Convert.ConvertTypes[8])
				{
					return value.ToUInt16(provider);
				}
				if (runtimeType == Convert.ConvertTypes[9])
				{
					return value.ToInt32(provider);
				}
				if (runtimeType == Convert.ConvertTypes[10])
				{
					return value.ToUInt32(provider);
				}
				if (runtimeType == Convert.ConvertTypes[11])
				{
					return value.ToInt64(provider);
				}
				if (runtimeType == Convert.ConvertTypes[12])
				{
					return value.ToUInt64(provider);
				}
				if (runtimeType == Convert.ConvertTypes[13])
				{
					return value.ToSingle(provider);
				}
				if (runtimeType == Convert.ConvertTypes[14])
				{
					return value.ToDouble(provider);
				}
				if (runtimeType == Convert.ConvertTypes[15])
				{
					return value.ToDecimal(provider);
				}
				if (runtimeType == Convert.ConvertTypes[16])
				{
					return value.ToDateTime(provider);
				}
				if (runtimeType == Convert.ConvertTypes[18])
				{
					return value.ToString(provider);
				}
				if (runtimeType == Convert.ConvertTypes[1])
				{
					return value;
				}
				if (runtimeType == Convert.EnumType)
				{
					return (Enum)value;
				}
				if (runtimeType == Convert.ConvertTypes[2])
				{
					throw new InvalidCastException(Environment.GetResourceString("InvalidCast_DBNull"));
				}
				if (runtimeType == Convert.ConvertTypes[0])
				{
					throw new InvalidCastException(Environment.GetResourceString("InvalidCast_Empty"));
				}
			}
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				value.GetType().FullName,
				targetType.FullName
			}));
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002560B File Offset: 0x0002380B
		[__DynamicallyInvokable]
		public static object ChangeType(object value, Type conversionType)
		{
			return Convert.ChangeType(value, conversionType, Thread.CurrentThread.CurrentCulture);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00025620 File Offset: 0x00023820
		[__DynamicallyInvokable]
		public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
		{
			if (conversionType == null)
			{
				throw new ArgumentNullException("conversionType");
			}
			if (value == null)
			{
				if (conversionType.IsValueType)
				{
					throw new InvalidCastException(Environment.GetResourceString("InvalidCast_CannotCastNullToValueType"));
				}
				return null;
			}
			else
			{
				IConvertible convertible = value as IConvertible;
				if (convertible == null)
				{
					if (value.GetType() == conversionType)
					{
						return value;
					}
					throw new InvalidCastException(Environment.GetResourceString("InvalidCast_IConvertible"));
				}
				else
				{
					RuntimeType runtimeType = conversionType as RuntimeType;
					if (runtimeType == Convert.ConvertTypes[3])
					{
						return convertible.ToBoolean(provider);
					}
					if (runtimeType == Convert.ConvertTypes[4])
					{
						return convertible.ToChar(provider);
					}
					if (runtimeType == Convert.ConvertTypes[5])
					{
						return convertible.ToSByte(provider);
					}
					if (runtimeType == Convert.ConvertTypes[6])
					{
						return convertible.ToByte(provider);
					}
					if (runtimeType == Convert.ConvertTypes[7])
					{
						return convertible.ToInt16(provider);
					}
					if (runtimeType == Convert.ConvertTypes[8])
					{
						return convertible.ToUInt16(provider);
					}
					if (runtimeType == Convert.ConvertTypes[9])
					{
						return convertible.ToInt32(provider);
					}
					if (runtimeType == Convert.ConvertTypes[10])
					{
						return convertible.ToUInt32(provider);
					}
					if (runtimeType == Convert.ConvertTypes[11])
					{
						return convertible.ToInt64(provider);
					}
					if (runtimeType == Convert.ConvertTypes[12])
					{
						return convertible.ToUInt64(provider);
					}
					if (runtimeType == Convert.ConvertTypes[13])
					{
						return convertible.ToSingle(provider);
					}
					if (runtimeType == Convert.ConvertTypes[14])
					{
						return convertible.ToDouble(provider);
					}
					if (runtimeType == Convert.ConvertTypes[15])
					{
						return convertible.ToDecimal(provider);
					}
					if (runtimeType == Convert.ConvertTypes[16])
					{
						return convertible.ToDateTime(provider);
					}
					if (runtimeType == Convert.ConvertTypes[18])
					{
						return convertible.ToString(provider);
					}
					if (runtimeType == Convert.ConvertTypes[1])
					{
						return value;
					}
					return convertible.ToType(conversionType, provider);
				}
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00025850 File Offset: 0x00023A50
		[__DynamicallyInvokable]
		public static bool ToBoolean(object value)
		{
			return value != null && ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00025863 File Offset: 0x00023A63
		[__DynamicallyInvokable]
		public static bool ToBoolean(object value, IFormatProvider provider)
		{
			return value != null && ((IConvertible)value).ToBoolean(provider);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00025876 File Offset: 0x00023A76
		[__DynamicallyInvokable]
		public static bool ToBoolean(bool value)
		{
			return value;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00025879 File Offset: 0x00023A79
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool ToBoolean(sbyte value)
		{
			return value != 0;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002587F File Offset: 0x00023A7F
		public static bool ToBoolean(char value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002588D File Offset: 0x00023A8D
		[__DynamicallyInvokable]
		public static bool ToBoolean(byte value)
		{
			return value > 0;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00025893 File Offset: 0x00023A93
		[__DynamicallyInvokable]
		public static bool ToBoolean(short value)
		{
			return value != 0;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00025899 File Offset: 0x00023A99
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool ToBoolean(ushort value)
		{
			return value > 0;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002589F File Offset: 0x00023A9F
		[__DynamicallyInvokable]
		public static bool ToBoolean(int value)
		{
			return value != 0;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000258A5 File Offset: 0x00023AA5
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool ToBoolean(uint value)
		{
			return value > 0U;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000258AB File Offset: 0x00023AAB
		[__DynamicallyInvokable]
		public static bool ToBoolean(long value)
		{
			return value != 0L;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000258B2 File Offset: 0x00023AB2
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool ToBoolean(ulong value)
		{
			return value > 0UL;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000258B9 File Offset: 0x00023AB9
		[__DynamicallyInvokable]
		public static bool ToBoolean(string value)
		{
			return value != null && bool.Parse(value);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000258C6 File Offset: 0x00023AC6
		[__DynamicallyInvokable]
		public static bool ToBoolean(string value, IFormatProvider provider)
		{
			return value != null && bool.Parse(value);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000258D3 File Offset: 0x00023AD3
		[__DynamicallyInvokable]
		public static bool ToBoolean(float value)
		{
			return value != 0f;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000258E0 File Offset: 0x00023AE0
		[__DynamicallyInvokable]
		public static bool ToBoolean(double value)
		{
			return value != 0.0;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000258F1 File Offset: 0x00023AF1
		[__DynamicallyInvokable]
		public static bool ToBoolean(decimal value)
		{
			return value != 0m;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000258FE File Offset: 0x00023AFE
		public static bool ToBoolean(DateTime value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002590C File Offset: 0x00023B0C
		[__DynamicallyInvokable]
		public static char ToChar(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(null);
			}
			return '\0';
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002591F File Offset: 0x00023B1F
		[__DynamicallyInvokable]
		public static char ToChar(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(provider);
			}
			return '\0';
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00025932 File Offset: 0x00023B32
		public static char ToChar(bool value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00025940 File Offset: 0x00023B40
		public static char ToChar(char value)
		{
			return value;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00025943 File Offset: 0x00023B43
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static char ToChar(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002595B File Offset: 0x00023B5B
		[__DynamicallyInvokable]
		public static char ToChar(byte value)
		{
			return (char)value;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002595E File Offset: 0x00023B5E
		[__DynamicallyInvokable]
		public static char ToChar(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00025976 File Offset: 0x00023B76
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static char ToChar(ushort value)
		{
			return (char)value;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00025979 File Offset: 0x00023B79
		[__DynamicallyInvokable]
		public static char ToChar(int value)
		{
			if (value < 0 || value > 65535)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00025999 File Offset: 0x00023B99
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static char ToChar(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000259B5 File Offset: 0x00023BB5
		[__DynamicallyInvokable]
		public static char ToChar(long value)
		{
			if (value < 0L || value > 65535L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000259D7 File Offset: 0x00023BD7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static char ToChar(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000259F4 File Offset: 0x00023BF4
		[__DynamicallyInvokable]
		public static char ToChar(string value)
		{
			return Convert.ToChar(value, null);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000259FD File Offset: 0x00023BFD
		[__DynamicallyInvokable]
		public static char ToChar(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
			}
			return value[0];
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00025A2D File Offset: 0x00023C2D
		public static char ToChar(float value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00025A3B File Offset: 0x00023C3B
		public static char ToChar(double value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00025A49 File Offset: 0x00023C49
		public static char ToChar(decimal value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00025A57 File Offset: 0x00023C57
		public static char ToChar(DateTime value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00025A65 File Offset: 0x00023C65
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(null);
			}
			return 0;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00025A78 File Offset: 0x00023C78
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(provider);
			}
			return 0;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00025A8B File Offset: 0x00023C8B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00025A93 File Offset: 0x00023C93
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(sbyte value)
		{
			return value;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00025A96 File Offset: 0x00023C96
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(char value)
		{
			if (value > '\u007f')
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00025AAF File Offset: 0x00023CAF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(byte value)
		{
			if (value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00025AC8 File Offset: 0x00023CC8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(short value)
		{
			if (value < -128 || value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00025AE6 File Offset: 0x00023CE6
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(ushort value)
		{
			if (value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00025AFF File Offset: 0x00023CFF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00025B1D File Offset: 0x00023D1D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(uint value)
		{
			if ((ulong)value > 127UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00025B38 File Offset: 0x00023D38
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(long value)
		{
			if (value < -128L || value > 127L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00025B58 File Offset: 0x00023D58
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(ulong value)
		{
			if (value > 127UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00025B72 File Offset: 0x00023D72
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(float value)
		{
			return Convert.ToSByte((double)value);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00025B7B File Offset: 0x00023D7B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(double value)
		{
			return Convert.ToSByte(Convert.ToInt32(value));
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00025B88 File Offset: 0x00023D88
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(decimal value)
		{
			return decimal.ToSByte(decimal.Round(value, 0));
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00025B96 File Offset: 0x00023D96
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return sbyte.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00025BA8 File Offset: 0x00023DA8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(string value, IFormatProvider provider)
		{
			return sbyte.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00025BB2 File Offset: 0x00023DB2
		[CLSCompliant(false)]
		public static sbyte ToSByte(DateTime value)
		{
			return ((IConvertible)value).ToSByte(null);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00025BC0 File Offset: 0x00023DC0
		[__DynamicallyInvokable]
		public static byte ToByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(null);
			}
			return 0;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00025BD3 File Offset: 0x00023DD3
		[__DynamicallyInvokable]
		public static byte ToByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(provider);
			}
			return 0;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00025BE6 File Offset: 0x00023DE6
		[__DynamicallyInvokable]
		public static byte ToByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00025BEE File Offset: 0x00023DEE
		[__DynamicallyInvokable]
		public static byte ToByte(byte value)
		{
			return value;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00025BF1 File Offset: 0x00023DF1
		[__DynamicallyInvokable]
		public static byte ToByte(char value)
		{
			if (value > 'ÿ')
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00025C0D File Offset: 0x00023E0D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static byte ToByte(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00025C25 File Offset: 0x00023E25
		[__DynamicallyInvokable]
		public static byte ToByte(short value)
		{
			if (value < 0 || value > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00025C45 File Offset: 0x00023E45
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static byte ToByte(ushort value)
		{
			if (value > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00025C61 File Offset: 0x00023E61
		[__DynamicallyInvokable]
		public static byte ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00025C81 File Offset: 0x00023E81
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static byte ToByte(uint value)
		{
			if (value > 255U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00025C9D File Offset: 0x00023E9D
		[__DynamicallyInvokable]
		public static byte ToByte(long value)
		{
			if (value < 0L || value > 255L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00025CBF File Offset: 0x00023EBF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static byte ToByte(ulong value)
		{
			if (value > 255UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00025CDC File Offset: 0x00023EDC
		[__DynamicallyInvokable]
		public static byte ToByte(float value)
		{
			return Convert.ToByte((double)value);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00025CE5 File Offset: 0x00023EE5
		[__DynamicallyInvokable]
		public static byte ToByte(double value)
		{
			return Convert.ToByte(Convert.ToInt32(value));
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00025CF2 File Offset: 0x00023EF2
		[__DynamicallyInvokable]
		public static byte ToByte(decimal value)
		{
			return decimal.ToByte(decimal.Round(value, 0));
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00025D00 File Offset: 0x00023F00
		[__DynamicallyInvokable]
		public static byte ToByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00025D12 File Offset: 0x00023F12
		[__DynamicallyInvokable]
		public static byte ToByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00025D21 File Offset: 0x00023F21
		public static byte ToByte(DateTime value)
		{
			return ((IConvertible)value).ToByte(null);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00025D2F File Offset: 0x00023F2F
		[__DynamicallyInvokable]
		public static short ToInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(null);
			}
			return 0;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00025D42 File Offset: 0x00023F42
		[__DynamicallyInvokable]
		public static short ToInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(provider);
			}
			return 0;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00025D55 File Offset: 0x00023F55
		[__DynamicallyInvokable]
		public static short ToInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00025D5D File Offset: 0x00023F5D
		[__DynamicallyInvokable]
		public static short ToInt16(char value)
		{
			if (value > '翿')
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00025D79 File Offset: 0x00023F79
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static short ToInt16(sbyte value)
		{
			return (short)value;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00025D7C File Offset: 0x00023F7C
		[__DynamicallyInvokable]
		public static short ToInt16(byte value)
		{
			return (short)value;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00025D7F File Offset: 0x00023F7F
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static short ToInt16(ushort value)
		{
			if (value > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00025D9B File Offset: 0x00023F9B
		[__DynamicallyInvokable]
		public static short ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00025DBF File Offset: 0x00023FBF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static short ToInt16(uint value)
		{
			if ((ulong)value > 32767UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00025DDD File Offset: 0x00023FDD
		[__DynamicallyInvokable]
		public static short ToInt16(short value)
		{
			return value;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00025DE0 File Offset: 0x00023FE0
		[__DynamicallyInvokable]
		public static short ToInt16(long value)
		{
			if (value < -32768L || value > 32767L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00025E06 File Offset: 0x00024006
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static short ToInt16(ulong value)
		{
			if (value > 32767UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00025E23 File Offset: 0x00024023
		[__DynamicallyInvokable]
		public static short ToInt16(float value)
		{
			return Convert.ToInt16((double)value);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00025E2C File Offset: 0x0002402C
		[__DynamicallyInvokable]
		public static short ToInt16(double value)
		{
			return Convert.ToInt16(Convert.ToInt32(value));
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00025E39 File Offset: 0x00024039
		[__DynamicallyInvokable]
		public static short ToInt16(decimal value)
		{
			return decimal.ToInt16(decimal.Round(value, 0));
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00025E47 File Offset: 0x00024047
		[__DynamicallyInvokable]
		public static short ToInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00025E59 File Offset: 0x00024059
		[__DynamicallyInvokable]
		public static short ToInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00025E68 File Offset: 0x00024068
		public static short ToInt16(DateTime value)
		{
			return ((IConvertible)value).ToInt16(null);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00025E76 File Offset: 0x00024076
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(null);
			}
			return 0;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00025E89 File Offset: 0x00024089
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(provider);
			}
			return 0;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00025E9C File Offset: 0x0002409C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00025EA4 File Offset: 0x000240A4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(char value)
		{
			return (ushort)value;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00025EA7 File Offset: 0x000240A7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00025EBF File Offset: 0x000240BF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(byte value)
		{
			return (ushort)value;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00025EC2 File Offset: 0x000240C2
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00025EDA File Offset: 0x000240DA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00025EFA File Offset: 0x000240FA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(ushort value)
		{
			return value;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00025EFD File Offset: 0x000240FD
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00025F19 File Offset: 0x00024119
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(long value)
		{
			if (value < 0L || value > 65535L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00025F3B File Offset: 0x0002413B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00025F58 File Offset: 0x00024158
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(float value)
		{
			return Convert.ToUInt16((double)value);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00025F61 File Offset: 0x00024161
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(double value)
		{
			return Convert.ToUInt16(Convert.ToInt32(value));
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00025F6E File Offset: 0x0002416E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(decimal value)
		{
			return decimal.ToUInt16(decimal.Round(value, 0));
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00025F7C File Offset: 0x0002417C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00025F8E File Offset: 0x0002418E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00025F9D File Offset: 0x0002419D
		[CLSCompliant(false)]
		public static ushort ToUInt16(DateTime value)
		{
			return ((IConvertible)value).ToUInt16(null);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00025FAB File Offset: 0x000241AB
		[__DynamicallyInvokable]
		public static int ToInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(null);
			}
			return 0;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00025FBE File Offset: 0x000241BE
		[__DynamicallyInvokable]
		public static int ToInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(provider);
			}
			return 0;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00025FD1 File Offset: 0x000241D1
		[__DynamicallyInvokable]
		public static int ToInt32(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00025FD9 File Offset: 0x000241D9
		[__DynamicallyInvokable]
		public static int ToInt32(char value)
		{
			return (int)value;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00025FDC File Offset: 0x000241DC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static int ToInt32(sbyte value)
		{
			return (int)value;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00025FDF File Offset: 0x000241DF
		[__DynamicallyInvokable]
		public static int ToInt32(byte value)
		{
			return (int)value;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00025FE2 File Offset: 0x000241E2
		[__DynamicallyInvokable]
		public static int ToInt32(short value)
		{
			return (int)value;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00025FE5 File Offset: 0x000241E5
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static int ToInt32(ushort value)
		{
			return (int)value;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00025FE8 File Offset: 0x000241E8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static int ToInt32(uint value)
		{
			if (value > 2147483647U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
			}
			return (int)value;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00026003 File Offset: 0x00024203
		[__DynamicallyInvokable]
		public static int ToInt32(int value)
		{
			return value;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00026006 File Offset: 0x00024206
		[__DynamicallyInvokable]
		public static int ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
			}
			return (int)value;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002602C File Offset: 0x0002422C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static int ToInt32(ulong value)
		{
			if (value > 2147483647UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
			}
			return (int)value;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00026049 File Offset: 0x00024249
		[__DynamicallyInvokable]
		public static int ToInt32(float value)
		{
			return Convert.ToInt32((double)value);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00026054 File Offset: 0x00024254
		[__DynamicallyInvokable]
		public static int ToInt32(double value)
		{
			if (value >= 0.0)
			{
				if (value < 2147483647.5)
				{
					int num = (int)value;
					double num2 = value - (double)num;
					if (num2 > 0.5 || (num2 == 0.5 && (num & 1) != 0))
					{
						num++;
					}
					return num;
				}
			}
			else if (value >= -2147483648.5)
			{
				int num3 = (int)value;
				double num4 = value - (double)num3;
				if (num4 < -0.5 || (num4 == -0.5 && (num3 & 1) != 0))
				{
					num3--;
				}
				return num3;
			}
			throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000260EA File Offset: 0x000242EA
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static int ToInt32(decimal value)
		{
			return decimal.FCallToInt32(value);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000260F2 File Offset: 0x000242F2
		[__DynamicallyInvokable]
		public static int ToInt32(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00026104 File Offset: 0x00024304
		[__DynamicallyInvokable]
		public static int ToInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00026113 File Offset: 0x00024313
		public static int ToInt32(DateTime value)
		{
			return ((IConvertible)value).ToInt32(null);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00026121 File Offset: 0x00024321
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(null);
			}
			return 0U;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00026134 File Offset: 0x00024334
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(provider);
			}
			return 0U;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00026147 File Offset: 0x00024347
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(bool value)
		{
			if (!value)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002614F File Offset: 0x0002434F
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(char value)
		{
			return (uint)value;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00026152 File Offset: 0x00024352
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00026169 File Offset: 0x00024369
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(byte value)
		{
			return (uint)value;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002616C File Offset: 0x0002436C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00026183 File Offset: 0x00024383
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(ushort value)
		{
			return (uint)value;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00026186 File Offset: 0x00024386
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(int value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002619D File Offset: 0x0002439D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(uint value)
		{
			return value;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000261A0 File Offset: 0x000243A0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)(-1)))
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000261BE File Offset: 0x000243BE
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(ulong value)
		{
			if (value > (ulong)(-1))
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000261D7 File Offset: 0x000243D7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(float value)
		{
			return Convert.ToUInt32((double)value);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000261E0 File Offset: 0x000243E0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(double value)
		{
			if (value >= -0.5 && value < 4294967295.5)
			{
				uint num = (uint)value;
				double num2 = value - num;
				if (num2 > 0.5 || (num2 == 0.5 && (num & 1U) != 0U))
				{
					num += 1U;
				}
				return num;
			}
			throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00026240 File Offset: 0x00024440
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(decimal value)
		{
			return decimal.ToUInt32(decimal.Round(value, 0));
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002624E File Offset: 0x0002444E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(string value)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00026260 File Offset: 0x00024460
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002626F File Offset: 0x0002446F
		[CLSCompliant(false)]
		public static uint ToUInt32(DateTime value)
		{
			return ((IConvertible)value).ToUInt32(null);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002627D File Offset: 0x0002447D
		[__DynamicallyInvokable]
		public static long ToInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(null);
			}
			return 0L;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00026291 File Offset: 0x00024491
		[__DynamicallyInvokable]
		public static long ToInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(provider);
			}
			return 0L;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000262A5 File Offset: 0x000244A5
		[__DynamicallyInvokable]
		public static long ToInt64(bool value)
		{
			return value ? 1L : 0L;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000262AF File Offset: 0x000244AF
		[__DynamicallyInvokable]
		public static long ToInt64(char value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000262B3 File Offset: 0x000244B3
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static long ToInt64(sbyte value)
		{
			return (long)value;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000262B7 File Offset: 0x000244B7
		[__DynamicallyInvokable]
		public static long ToInt64(byte value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000262BB File Offset: 0x000244BB
		[__DynamicallyInvokable]
		public static long ToInt64(short value)
		{
			return (long)value;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000262BF File Offset: 0x000244BF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static long ToInt64(ushort value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000262C3 File Offset: 0x000244C3
		[__DynamicallyInvokable]
		public static long ToInt64(int value)
		{
			return (long)value;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000262C7 File Offset: 0x000244C7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static long ToInt64(uint value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000262CB File Offset: 0x000244CB
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static long ToInt64(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int64"));
			}
			return (long)value;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000262EA File Offset: 0x000244EA
		[__DynamicallyInvokable]
		public static long ToInt64(long value)
		{
			return value;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000262ED File Offset: 0x000244ED
		[__DynamicallyInvokable]
		public static long ToInt64(float value)
		{
			return Convert.ToInt64((double)value);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000262F6 File Offset: 0x000244F6
		[__DynamicallyInvokable]
		public static long ToInt64(double value)
		{
			return checked((long)Math.Round(value));
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000262FF File Offset: 0x000244FF
		[__DynamicallyInvokable]
		public static long ToInt64(decimal value)
		{
			return decimal.ToInt64(decimal.Round(value, 0));
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002630D File Offset: 0x0002450D
		[__DynamicallyInvokable]
		public static long ToInt64(string value)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00026320 File Offset: 0x00024520
		[__DynamicallyInvokable]
		public static long ToInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00026330 File Offset: 0x00024530
		public static long ToInt64(DateTime value)
		{
			return ((IConvertible)value).ToInt64(null);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002633E File Offset: 0x0002453E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(null);
			}
			return 0UL;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00026352 File Offset: 0x00024552
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(provider);
			}
			return 0UL;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00026366 File Offset: 0x00024566
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(bool value)
		{
			if (!value)
			{
				return 0UL;
			}
			return 1UL;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00026370 File Offset: 0x00024570
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(char value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00026374 File Offset: 0x00024574
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002638C File Offset: 0x0002458C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(byte value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00026390 File Offset: 0x00024590
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x000263A8 File Offset: 0x000245A8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(ushort value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x000263AC File Offset: 0x000245AC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(int value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000263C4 File Offset: 0x000245C4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(uint value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000263C8 File Offset: 0x000245C8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(long value)
		{
			if (value < 0L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
			}
			return (ulong)value;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x000263E0 File Offset: 0x000245E0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(ulong value)
		{
			return value;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000263E3 File Offset: 0x000245E3
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(float value)
		{
			return Convert.ToUInt64((double)value);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000263EC File Offset: 0x000245EC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(double value)
		{
			return checked((ulong)Math.Round(value));
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000263F5 File Offset: 0x000245F5
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(decimal value)
		{
			return decimal.ToUInt64(decimal.Round(value, 0));
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00026403 File Offset: 0x00024603
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(string value)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00026416 File Offset: 0x00024616
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00026426 File Offset: 0x00024626
		[CLSCompliant(false)]
		public static ulong ToUInt64(DateTime value)
		{
			return ((IConvertible)value).ToUInt64(null);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00026434 File Offset: 0x00024634
		[__DynamicallyInvokable]
		public static float ToSingle(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(null);
			}
			return 0f;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002644B File Offset: 0x0002464B
		[__DynamicallyInvokable]
		public static float ToSingle(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(provider);
			}
			return 0f;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00026462 File Offset: 0x00024662
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static float ToSingle(sbyte value)
		{
			return (float)value;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00026466 File Offset: 0x00024666
		[__DynamicallyInvokable]
		public static float ToSingle(byte value)
		{
			return (float)value;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002646A File Offset: 0x0002466A
		public static float ToSingle(char value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00026478 File Offset: 0x00024678
		[__DynamicallyInvokable]
		public static float ToSingle(short value)
		{
			return (float)value;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002647C File Offset: 0x0002467C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static float ToSingle(ushort value)
		{
			return (float)value;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00026480 File Offset: 0x00024680
		[__DynamicallyInvokable]
		public static float ToSingle(int value)
		{
			return (float)value;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00026484 File Offset: 0x00024684
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static float ToSingle(uint value)
		{
			return value;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00026489 File Offset: 0x00024689
		[__DynamicallyInvokable]
		public static float ToSingle(long value)
		{
			return (float)value;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002648D File Offset: 0x0002468D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static float ToSingle(ulong value)
		{
			return value;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00026492 File Offset: 0x00024692
		[__DynamicallyInvokable]
		public static float ToSingle(float value)
		{
			return value;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00026495 File Offset: 0x00024695
		[__DynamicallyInvokable]
		public static float ToSingle(double value)
		{
			return (float)value;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00026499 File Offset: 0x00024699
		[__DynamicallyInvokable]
		public static float ToSingle(decimal value)
		{
			return (float)value;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000264A2 File Offset: 0x000246A2
		[__DynamicallyInvokable]
		public static float ToSingle(string value)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000264B8 File Offset: 0x000246B8
		[__DynamicallyInvokable]
		public static float ToSingle(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000264CF File Offset: 0x000246CF
		[__DynamicallyInvokable]
		public static float ToSingle(bool value)
		{
			return (float)(value ? 1 : 0);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x000264D9 File Offset: 0x000246D9
		public static float ToSingle(DateTime value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000264E7 File Offset: 0x000246E7
		[__DynamicallyInvokable]
		public static double ToDouble(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(null);
			}
			return 0.0;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00026502 File Offset: 0x00024702
		[__DynamicallyInvokable]
		public static double ToDouble(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(provider);
			}
			return 0.0;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002651D File Offset: 0x0002471D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static double ToDouble(sbyte value)
		{
			return (double)value;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00026521 File Offset: 0x00024721
		[__DynamicallyInvokable]
		public static double ToDouble(byte value)
		{
			return (double)value;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00026525 File Offset: 0x00024725
		[__DynamicallyInvokable]
		public static double ToDouble(short value)
		{
			return (double)value;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00026529 File Offset: 0x00024729
		public static double ToDouble(char value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00026537 File Offset: 0x00024737
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static double ToDouble(ushort value)
		{
			return (double)value;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002653B File Offset: 0x0002473B
		[__DynamicallyInvokable]
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002653F File Offset: 0x0002473F
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static double ToDouble(uint value)
		{
			return value;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00026544 File Offset: 0x00024744
		[__DynamicallyInvokable]
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00026548 File Offset: 0x00024748
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static double ToDouble(ulong value)
		{
			return value;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002654D File Offset: 0x0002474D
		[__DynamicallyInvokable]
		public static double ToDouble(float value)
		{
			return (double)value;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00026551 File Offset: 0x00024751
		[__DynamicallyInvokable]
		public static double ToDouble(double value)
		{
			return value;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00026554 File Offset: 0x00024754
		[__DynamicallyInvokable]
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002655D File Offset: 0x0002475D
		[__DynamicallyInvokable]
		public static double ToDouble(string value)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00026577 File Offset: 0x00024777
		[__DynamicallyInvokable]
		public static double ToDouble(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00026592 File Offset: 0x00024792
		[__DynamicallyInvokable]
		public static double ToDouble(bool value)
		{
			return (double)(value ? 1 : 0);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0002659C File Offset: 0x0002479C
		public static double ToDouble(DateTime value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000265AA File Offset: 0x000247AA
		[__DynamicallyInvokable]
		public static decimal ToDecimal(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return 0m;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000265C1 File Offset: 0x000247C1
		[__DynamicallyInvokable]
		public static decimal ToDecimal(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(provider);
			}
			return 0m;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000265D8 File Offset: 0x000247D8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static decimal ToDecimal(sbyte value)
		{
			return value;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000265E0 File Offset: 0x000247E0
		[__DynamicallyInvokable]
		public static decimal ToDecimal(byte value)
		{
			return value;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x000265E8 File Offset: 0x000247E8
		public static decimal ToDecimal(char value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000265F6 File Offset: 0x000247F6
		[__DynamicallyInvokable]
		public static decimal ToDecimal(short value)
		{
			return value;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000265FE File Offset: 0x000247FE
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static decimal ToDecimal(ushort value)
		{
			return value;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00026606 File Offset: 0x00024806
		[__DynamicallyInvokable]
		public static decimal ToDecimal(int value)
		{
			return value;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002660E File Offset: 0x0002480E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static decimal ToDecimal(uint value)
		{
			return value;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00026616 File Offset: 0x00024816
		[__DynamicallyInvokable]
		public static decimal ToDecimal(long value)
		{
			return value;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002661E File Offset: 0x0002481E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static decimal ToDecimal(ulong value)
		{
			return value;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00026626 File Offset: 0x00024826
		[__DynamicallyInvokable]
		public static decimal ToDecimal(float value)
		{
			return (decimal)value;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002662E File Offset: 0x0002482E
		[__DynamicallyInvokable]
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00026636 File Offset: 0x00024836
		[__DynamicallyInvokable]
		public static decimal ToDecimal(string value)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002664C File Offset: 0x0002484C
		[__DynamicallyInvokable]
		public static decimal ToDecimal(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, NumberStyles.Number, provider);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00026660 File Offset: 0x00024860
		[__DynamicallyInvokable]
		public static decimal ToDecimal(decimal value)
		{
			return value;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00026663 File Offset: 0x00024863
		[__DynamicallyInvokable]
		public static decimal ToDecimal(bool value)
		{
			return value ? 1 : 0;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00026671 File Offset: 0x00024871
		public static decimal ToDecimal(DateTime value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002667F File Offset: 0x0002487F
		public static DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00026682 File Offset: 0x00024882
		[__DynamicallyInvokable]
		public static DateTime ToDateTime(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(null);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00026699 File Offset: 0x00024899
		[__DynamicallyInvokable]
		public static DateTime ToDateTime(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(provider);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000266B0 File Offset: 0x000248B0
		[__DynamicallyInvokable]
		public static DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000266C8 File Offset: 0x000248C8
		[__DynamicallyInvokable]
		public static DateTime ToDateTime(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, provider);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000266DC File Offset: 0x000248DC
		[CLSCompliant(false)]
		public static DateTime ToDateTime(sbyte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000266EA File Offset: 0x000248EA
		public static DateTime ToDateTime(byte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000266F8 File Offset: 0x000248F8
		public static DateTime ToDateTime(short value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00026706 File Offset: 0x00024906
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ushort value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00026714 File Offset: 0x00024914
		public static DateTime ToDateTime(int value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00026722 File Offset: 0x00024922
		[CLSCompliant(false)]
		public static DateTime ToDateTime(uint value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00026730 File Offset: 0x00024930
		public static DateTime ToDateTime(long value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002673E File Offset: 0x0002493E
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ulong value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002674C File Offset: 0x0002494C
		public static DateTime ToDateTime(bool value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002675A File Offset: 0x0002495A
		public static DateTime ToDateTime(char value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00026768 File Offset: 0x00024968
		public static DateTime ToDateTime(float value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00026776 File Offset: 0x00024976
		public static DateTime ToDateTime(double value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00026784 File Offset: 0x00024984
		public static DateTime ToDateTime(decimal value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00026792 File Offset: 0x00024992
		[__DynamicallyInvokable]
		public static string ToString(object value)
		{
			return Convert.ToString(value, null);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002679C File Offset: 0x0002499C
		[__DynamicallyInvokable]
		public static string ToString(object value, IFormatProvider provider)
		{
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.ToString(provider);
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(null, provider);
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000267DD File Offset: 0x000249DD
		[__DynamicallyInvokable]
		public static string ToString(bool value)
		{
			return value.ToString();
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000267E6 File Offset: 0x000249E6
		[__DynamicallyInvokable]
		public static string ToString(bool value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000267F0 File Offset: 0x000249F0
		[__DynamicallyInvokable]
		public static string ToString(char value)
		{
			return char.ToString(value);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000267F8 File Offset: 0x000249F8
		[__DynamicallyInvokable]
		public static string ToString(char value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00026802 File Offset: 0x00024A02
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(sbyte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00026810 File Offset: 0x00024A10
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(sbyte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002681A File Offset: 0x00024A1A
		[__DynamicallyInvokable]
		public static string ToString(byte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00026828 File Offset: 0x00024A28
		[__DynamicallyInvokable]
		public static string ToString(byte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00026832 File Offset: 0x00024A32
		[__DynamicallyInvokable]
		public static string ToString(short value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00026840 File Offset: 0x00024A40
		[__DynamicallyInvokable]
		public static string ToString(short value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002684A File Offset: 0x00024A4A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ushort value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00026858 File Offset: 0x00024A58
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ushort value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00026862 File Offset: 0x00024A62
		[__DynamicallyInvokable]
		public static string ToString(int value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00026870 File Offset: 0x00024A70
		[__DynamicallyInvokable]
		public static string ToString(int value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002687A File Offset: 0x00024A7A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(uint value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00026888 File Offset: 0x00024A88
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(uint value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00026892 File Offset: 0x00024A92
		[__DynamicallyInvokable]
		public static string ToString(long value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000268A0 File Offset: 0x00024AA0
		[__DynamicallyInvokable]
		public static string ToString(long value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000268AA File Offset: 0x00024AAA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ulong value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000268B8 File Offset: 0x00024AB8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ulong value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000268C2 File Offset: 0x00024AC2
		[__DynamicallyInvokable]
		public static string ToString(float value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000268D0 File Offset: 0x00024AD0
		[__DynamicallyInvokable]
		public static string ToString(float value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000268DA File Offset: 0x00024ADA
		[__DynamicallyInvokable]
		public static string ToString(double value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000268E8 File Offset: 0x00024AE8
		[__DynamicallyInvokable]
		public static string ToString(double value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000268F2 File Offset: 0x00024AF2
		[__DynamicallyInvokable]
		public static string ToString(decimal value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00026900 File Offset: 0x00024B00
		[__DynamicallyInvokable]
		public static string ToString(decimal value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002690A File Offset: 0x00024B0A
		[__DynamicallyInvokable]
		public static string ToString(DateTime value)
		{
			return value.ToString();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00026913 File Offset: 0x00024B13
		[__DynamicallyInvokable]
		public static string ToString(DateTime value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002691D File Offset: 0x00024B1D
		public static string ToString(string value)
		{
			return value;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00026920 File Offset: 0x00024B20
		public static string ToString(string value, IFormatProvider provider)
		{
			return value;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00026924 File Offset: 0x00024B24
		[__DynamicallyInvokable]
		public static byte ToByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 4608);
			if (num < 0 || num > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)num;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00026980 File Offset: 0x00024B80
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 5120);
			if (fromBase != 10 && num <= 255)
			{
				return (sbyte)num;
			}
			if (num < -128 || num > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)num;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000269E8 File Offset: 0x00024BE8
		[__DynamicallyInvokable]
		public static short ToInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 6144);
			if (fromBase != 10 && num <= 65535)
			{
				return (short)num;
			}
			if (num < -32768 || num > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)num;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00026A58 File Offset: 0x00024C58
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 4608);
			if (num < 0 || num > 65535)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)num;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00026AB2 File Offset: 0x00024CB2
		[__DynamicallyInvokable]
		public static int ToInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.StringToInt(value, fromBase, 4096);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00026AE2 File Offset: 0x00024CE2
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return (uint)ParseNumbers.StringToInt(value, fromBase, 4608);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00026B12 File Offset: 0x00024D12
		[__DynamicallyInvokable]
		public static long ToInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.StringToLong(value, fromBase, 4096);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00026B42 File Offset: 0x00024D42
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return (ulong)ParseNumbers.StringToLong(value, fromBase, 4608);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00026B72 File Offset: 0x00024D72
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ToString(byte value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 64);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00026BA2 File Offset: 0x00024DA2
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ToString(short value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 128);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00026BD5 File Offset: 0x00024DD5
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ToString(int value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.IntToString(value, toBase, -1, ' ', 0);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00026C04 File Offset: 0x00024E04
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ToString(long value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.LongToString(value, toBase, -1, ' ', 0);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00026C33 File Offset: 0x00024E33
		[__DynamicallyInvokable]
		public static string ToBase64String(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(inArray, 0, inArray.Length, Base64FormattingOptions.None);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00026C4E File Offset: 0x00024E4E
		[ComVisible(false)]
		public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(inArray, 0, inArray.Length, options);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00026C69 File Offset: 0x00024E69
		[__DynamicallyInvokable]
		public static string ToBase64String(byte[] inArray, int offset, int length)
		{
			return Convert.ToBase64String(inArray, offset, length, Base64FormattingOptions.None);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00026C74 File Offset: 0x00024E74
		[SecuritySafeCritical]
		[ComVisible(false)]
		public unsafe static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)options }));
			}
			int num = inArray.Length;
			if (offset > num - length)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
			}
			if (num == 0)
			{
				return string.Empty;
			}
			bool flag = options == Base64FormattingOptions.InsertLineBreaks;
			int num2 = Convert.ToBase64_CalculateAndValidateOutputLength(length, flag);
			string text = string.FastAllocateString(num2);
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				byte* ptr2;
				if (inArray == null || inArray.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &inArray[0];
				}
				int num3 = Convert.ConvertToBase64Array(ptr, ptr2, offset, length, flag);
				return text;
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00026D66 File Offset: 0x00024F66
		[__DynamicallyInvokable]
		public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
		{
			return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, Base64FormattingOptions.None);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00026D74 File Offset: 0x00024F74
		[SecuritySafeCritical]
		[ComVisible(false)]
		public unsafe static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (outArray == null)
			{
				throw new ArgumentNullException("outArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (offsetIn < 0)
			{
				throw new ArgumentOutOfRangeException("offsetIn", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (offsetOut < 0)
			{
				throw new ArgumentOutOfRangeException("offsetOut", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)options }));
			}
			int num = inArray.Length;
			if (offsetIn > num - length)
			{
				throw new ArgumentOutOfRangeException("offsetIn", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
			}
			if (num == 0)
			{
				return 0;
			}
			bool flag = options == Base64FormattingOptions.InsertLineBreaks;
			int num2 = outArray.Length;
			int num3 = Convert.ToBase64_CalculateAndValidateOutputLength(length, flag);
			if (offsetOut > num2 - num3)
			{
				throw new ArgumentOutOfRangeException("offsetOut", Environment.GetResourceString("ArgumentOutOfRange_OffsetOut"));
			}
			int num4;
			fixed (char* ptr = &outArray[offsetOut])
			{
				char* ptr2 = ptr;
				fixed (byte[] array = inArray)
				{
					byte* ptr3;
					if (inArray == null || array.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array[0];
					}
					num4 = Convert.ConvertToBase64Array(ptr2, ptr3, offsetIn, length, flag);
				}
			}
			return num4;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00026EAC File Offset: 0x000250AC
		[SecurityCritical]
		private unsafe static int ConvertToBase64Array(char* outChars, byte* inData, int offset, int length, bool insertLineBreaks)
		{
			int num = length % 3;
			int num2 = offset + (length - num);
			int num3 = 0;
			int num4 = 0;
			char[] array;
			char* ptr;
			if ((array = Convert.base64Table) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			int i;
			for (i = offset; i < num2; i += 3)
			{
				if (insertLineBreaks)
				{
					if (num4 == 76)
					{
						outChars[num3++] = '\r';
						outChars[num3++] = '\n';
						num4 = 0;
					}
					num4 += 4;
				}
				outChars[num3] = ptr[(inData[i] & 252) >> 2];
				outChars[num3 + 1] = ptr[((int)(inData[i] & 3) << 4) | ((inData[i + 1] & 240) >> 4)];
				outChars[num3 + 2] = ptr[((int)(inData[i + 1] & 15) << 2) | ((inData[i + 2] & 192) >> 6)];
				outChars[num3 + 3] = ptr[inData[i + 2] & 63];
				num3 += 4;
			}
			i = num2;
			if (insertLineBreaks && num != 0 && num4 == 76)
			{
				outChars[num3++] = '\r';
				outChars[num3++] = '\n';
			}
			if (num != 1)
			{
				if (num == 2)
				{
					outChars[num3] = ptr[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr[((int)(inData[i] & 3) << 4) | ((inData[i + 1] & 240) >> 4)];
					outChars[num3 + 2] = ptr[(inData[i + 1] & 15) << 2];
					outChars[num3 + 3] = ptr[64];
					num3 += 4;
				}
			}
			else
			{
				outChars[num3] = ptr[(inData[i] & 252) >> 2];
				outChars[num3 + 1] = ptr[(inData[i] & 3) << 4];
				outChars[num3 + 2] = ptr[64];
				outChars[num3 + 3] = ptr[64];
				num3 += 4;
			}
			array = null;
			return num3;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000270C4 File Offset: 0x000252C4
		private static int ToBase64_CalculateAndValidateOutputLength(int inputLength, bool insertLineBreaks)
		{
			long num = (long)inputLength / 3L * 4L;
			num += ((inputLength % 3 != 0) ? 4L : 0L);
			if (num == 0L)
			{
				return 0;
			}
			if (insertLineBreaks)
			{
				long num2 = num / 76L;
				if (num % 76L == 0L)
				{
					num2 -= 1L;
				}
				num += num2 * 2L;
			}
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			return (int)num;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002711C File Offset: 0x0002531C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static byte[] FromBase64String(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Convert.FromBase64CharPtr(ptr, s.Length);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00027154 File Offset: 0x00025354
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static byte[] FromBase64CharArray(char[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
			}
			char* ptr;
			if (inArray == null || inArray.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &inArray[0];
			}
			return Convert.FromBase64CharPtr(ptr + offset, length);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000271E4 File Offset: 0x000253E4
		[SecurityCritical]
		private unsafe static byte[] FromBase64CharPtr(char* inputPtr, int inputLength)
		{
			while (inputLength > 0)
			{
				int num = (int)inputPtr[inputLength - 1];
				if (num != 32 && num != 10 && num != 13 && num != 9)
				{
					break;
				}
				inputLength--;
			}
			int num2 = Convert.FromBase64_ComputeResultLength(inputPtr, inputLength);
			byte[] array = new byte[num2];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			int num3 = Convert.FromBase64_Decode(inputPtr, inputLength, ptr, num2);
			array2 = null;
			return array;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00027258 File Offset: 0x00025458
		[SecurityCritical]
		private unsafe static int FromBase64_Decode(char* startInputPtr, int inputLength, byte* startDestPtr, int destLength)
		{
			char* ptr = startInputPtr;
			byte* ptr2 = startDestPtr;
			char* ptr3 = ptr + inputLength;
			byte* ptr4 = ptr2 + destLength;
			uint num = 255U;
			while (ptr < ptr3)
			{
				uint num2 = (uint)(*ptr);
				ptr++;
				if (num2 - 65U <= 25U)
				{
					num2 -= 65U;
				}
				else if (num2 - 97U <= 25U)
				{
					num2 -= 71U;
				}
				else
				{
					if (num2 - 48U > 9U)
					{
						if (num2 <= 32U)
						{
							if (num2 - 9U <= 1U || num2 == 13U || num2 == 32U)
							{
								continue;
							}
						}
						else
						{
							if (num2 == 43U)
							{
								num2 = 62U;
								goto IL_A7;
							}
							if (num2 == 47U)
							{
								num2 = 63U;
								goto IL_A7;
							}
							if (num2 == 61U)
							{
								if (ptr == ptr3)
								{
									num <<= 6;
									if ((num & 2147483648U) == 0U)
									{
										throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
									}
									if ((int)((long)(ptr4 - ptr2)) < 2)
									{
										return -1;
									}
									*(ptr2++) = (byte)(num >> 16);
									*(ptr2++) = (byte)(num >> 8);
									num = 255U;
									break;
								}
								else
								{
									while (ptr < ptr3 - 1)
									{
										int num3 = (int)(*ptr);
										if (num3 != 32 && num3 != 10 && num3 != 13 && num3 != 9)
										{
											break;
										}
										ptr++;
									}
									if (ptr != ptr3 - 1 || *ptr != '=')
									{
										throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
									}
									num <<= 12;
									if ((num & 2147483648U) == 0U)
									{
										throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
									}
									if ((int)((long)(ptr4 - ptr2)) < 1)
									{
										return -1;
									}
									*(ptr2++) = (byte)(num >> 16);
									num = 255U;
									break;
								}
							}
						}
						throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
					}
					num2 -= 4294967292U;
				}
				IL_A7:
				num = (num << 6) | num2;
				if ((num & 2147483648U) != 0U)
				{
					if ((int)((long)(ptr4 - ptr2)) < 3)
					{
						return -1;
					}
					*ptr2 = (byte)(num >> 16);
					ptr2[1] = (byte)(num >> 8);
					ptr2[2] = (byte)num;
					ptr2 += 3;
					num = 255U;
				}
			}
			if (num != 255U)
			{
				throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
			}
			return (int)((long)(ptr2 - startDestPtr));
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00027450 File Offset: 0x00025650
		[SecurityCritical]
		private unsafe static int FromBase64_ComputeResultLength(char* inputPtr, int inputLength)
		{
			char* ptr = inputPtr + inputLength;
			int num = inputLength;
			int num2 = 0;
			while (inputPtr < ptr)
			{
				uint num3 = (uint)(*inputPtr);
				inputPtr++;
				if (num3 <= 32U)
				{
					num--;
				}
				else if (num3 == 61U)
				{
					num--;
					num2++;
				}
			}
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					num2 = 2;
				}
				else
				{
					if (num2 != 2)
					{
						throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
					}
					num2 = 1;
				}
			}
			return num / 4 * 3 + num2;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x000274B8 File Offset: 0x000256B8
		// Note: this type is marked as 'beforefieldinit'.
		static Convert()
		{
		}

		// Token: 0x04000534 RID: 1332
		internal static readonly RuntimeType[] ConvertTypes = new RuntimeType[]
		{
			(RuntimeType)typeof(Empty),
			(RuntimeType)typeof(object),
			(RuntimeType)typeof(DBNull),
			(RuntimeType)typeof(bool),
			(RuntimeType)typeof(char),
			(RuntimeType)typeof(sbyte),
			(RuntimeType)typeof(byte),
			(RuntimeType)typeof(short),
			(RuntimeType)typeof(ushort),
			(RuntimeType)typeof(int),
			(RuntimeType)typeof(uint),
			(RuntimeType)typeof(long),
			(RuntimeType)typeof(ulong),
			(RuntimeType)typeof(float),
			(RuntimeType)typeof(double),
			(RuntimeType)typeof(decimal),
			(RuntimeType)typeof(DateTime),
			(RuntimeType)typeof(object),
			(RuntimeType)typeof(string)
		};

		// Token: 0x04000535 RID: 1333
		private static readonly RuntimeType EnumType = (RuntimeType)typeof(Enum);

		// Token: 0x04000536 RID: 1334
		internal static readonly char[] base64Table = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
			'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
			'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', '+', '/', '='
		};

		// Token: 0x04000537 RID: 1335
		private const int base64LineBreakPosition = 76;

		// Token: 0x04000538 RID: 1336
		public static readonly object DBNull = System.DBNull.Value;
	}
}
