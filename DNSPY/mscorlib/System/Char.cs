using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000B6 RID: 182
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Char : IComparable, IConvertible, IComparable<char>, IEquatable<char>
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x00021901 File Offset: 0x0001FB01
		private static bool IsLatin1(char ch)
		{
			return ch <= 'ÿ';
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002190E File Offset: 0x0001FB0E
		private static bool IsAscii(char ch)
		{
			return ch <= '\u007f';
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00021918 File Offset: 0x0001FB18
		private static UnicodeCategory GetLatin1UnicodeCategory(char ch)
		{
			return (UnicodeCategory)char.categoryForLatin1[(int)ch];
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00021921 File Offset: 0x0001FB21
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)(this | ((int)this << 16));
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002192B File Offset: 0x0001FB2B
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is char && this == (char)obj;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00021941 File Offset: 0x0001FB41
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(char obj)
		{
			return this == obj;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00021948 File Offset: 0x0001FB48
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is char))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeChar"));
			}
			return (int)(this - (char)value);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00021970 File Offset: 0x0001FB70
		[__DynamicallyInvokable]
		public int CompareTo(char value)
		{
			return (int)(this - value);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00021976 File Offset: 0x0001FB76
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return char.ToString(this);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002197F File Offset: 0x0001FB7F
		public string ToString(IFormatProvider provider)
		{
			return char.ToString(this);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00021988 File Offset: 0x0001FB88
		[__DynamicallyInvokable]
		public static string ToString(char c)
		{
			return new string(c, 1);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00021991 File Offset: 0x0001FB91
		[__DynamicallyInvokable]
		public static char Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
			}
			return s[0];
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000219C1 File Offset: 0x0001FBC1
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out char result)
		{
			result = '\0';
			if (s == null)
			{
				return false;
			}
			if (s.Length != 1)
			{
				return false;
			}
			result = s[0];
			return true;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000219E0 File Offset: 0x0001FBE0
		[__DynamicallyInvokable]
		public static bool IsDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00021A03 File Offset: 0x0001FC03
		internal static bool CheckLetter(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00021A0C File Offset: 0x0001FC0C
		[__DynamicallyInvokable]
		public static bool IsLetter(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00021A4C File Offset: 0x0001FC4C
		private static bool IsWhiteSpaceLatin1(char c)
		{
			return c == ' ' || (c >= '\t' && c <= '\r') || c == '\u00a0' || c == '\u0085';
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00021A70 File Offset: 0x0001FC70
		[__DynamicallyInvokable]
		public static bool IsWhiteSpace(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsWhiteSpaceLatin1(c);
			}
			return CharUnicodeInfo.IsWhiteSpace(c);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00021A87 File Offset: 0x0001FC87
		[__DynamicallyInvokable]
		public static bool IsUpper(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00021ABC File Offset: 0x0001FCBC
		[__DynamicallyInvokable]
		public static bool IsLower(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00021AF1 File Offset: 0x0001FCF1
		internal static bool CheckPunctuation(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.ConnectorPunctuation <= 6;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00021AFD File Offset: 0x0001FCFD
		[__DynamicallyInvokable]
		public static bool IsPunctuation(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00021B1E File Offset: 0x0001FD1E
		internal static bool CheckLetterOrDigit(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter || uc == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00021B2B File Offset: 0x0001FD2B
		[__DynamicallyInvokable]
		public static bool IsLetterOrDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00021B4C File Offset: 0x0001FD4C
		[__DynamicallyInvokable]
		public static char ToUpper(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(c);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00021B68 File Offset: 0x0001FD68
		[__DynamicallyInvokable]
		public static char ToUpper(char c)
		{
			return char.ToUpper(c, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00021B75 File Offset: 0x0001FD75
		[__DynamicallyInvokable]
		public static char ToUpperInvariant(char c)
		{
			return char.ToUpper(c, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00021B82 File Offset: 0x0001FD82
		[__DynamicallyInvokable]
		public static char ToLower(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(c);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00021B9E File Offset: 0x0001FD9E
		[__DynamicallyInvokable]
		public static char ToLower(char c)
		{
			return char.ToLower(c, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00021BAB File Offset: 0x0001FDAB
		[__DynamicallyInvokable]
		public static char ToLowerInvariant(char c)
		{
			return char.ToLower(c, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		public TypeCode GetTypeCode()
		{
			return TypeCode.Char;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00021BBB File Offset: 0x0001FDBB
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "Boolean" }));
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00021BE2 File Offset: 0x0001FDE2
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00021BE6 File Offset: 0x0001FDE6
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00021BEF File Offset: 0x0001FDEF
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00021BF8 File Offset: 0x0001FDF8
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00021C01 File Offset: 0x0001FE01
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00021C0A File Offset: 0x0001FE0A
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00021C13 File Offset: 0x0001FE13
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00021C1C File Offset: 0x0001FE1C
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00021C25 File Offset: 0x0001FE25
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00021C2E File Offset: 0x0001FE2E
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "Single" }));
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00021C55 File Offset: 0x0001FE55
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "Double" }));
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00021C7C File Offset: 0x0001FE7C
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "Decimal" }));
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00021CA3 File Offset: 0x0001FEA3
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "DateTime" }));
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00021CCA File Offset: 0x0001FECA
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00021CDA File Offset: 0x0001FEDA
		[__DynamicallyInvokable]
		public static bool IsControl(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00021CFC File Offset: 0x0001FEFC
		[__DynamicallyInvokable]
		public static bool IsControl(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.Control;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00021D54 File Offset: 0x0001FF54
		[__DynamicallyInvokable]
		public static bool IsDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00021DB0 File Offset: 0x0001FFB0
		[__DynamicallyInvokable]
		public static bool IsLetter(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00021E28 File Offset: 0x00020028
		[__DynamicallyInvokable]
		public static bool IsLetterOrDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00021E80 File Offset: 0x00020080
		[__DynamicallyInvokable]
		public static bool IsLower(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00021EEB File Offset: 0x000200EB
		internal static bool CheckNumber(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.DecimalDigitNumber <= 2;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00021EF6 File Offset: 0x000200F6
		[__DynamicallyInvokable]
		public static bool IsNumber(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00021F30 File Offset: 0x00020130
		[__DynamicallyInvokable]
		public static bool IsNumber(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00021FA0 File Offset: 0x000201A0
		[__DynamicallyInvokable]
		public static bool IsPunctuation(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00021FF7 File Offset: 0x000201F7
		internal static bool CheckSeparator(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00022003 File Offset: 0x00020203
		private static bool IsSeparatorLatin1(char c)
		{
			return c == ' ' || c == '\u00a0';
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00022014 File Offset: 0x00020214
		[__DynamicallyInvokable]
		public static bool IsSeparator(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00022030 File Offset: 0x00020230
		[__DynamicallyInvokable]
		public static bool IsSeparator(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00022082 File Offset: 0x00020282
		[__DynamicallyInvokable]
		public static bool IsSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udfff';
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00022099 File Offset: 0x00020299
		[__DynamicallyInvokable]
		public static bool IsSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsSurrogate(s[index]);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000220C9 File Offset: 0x000202C9
		internal static bool CheckSymbol(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.MathSymbol <= 3;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000220D5 File Offset: 0x000202D5
		[__DynamicallyInvokable]
		public static bool IsSymbol(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000220F8 File Offset: 0x000202F8
		[__DynamicallyInvokable]
		public static bool IsSymbol(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(s[index]));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00022154 File Offset: 0x00020354
		[__DynamicallyInvokable]
		public static bool IsUpper(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000221C0 File Offset: 0x000203C0
		[__DynamicallyInvokable]
		public static bool IsWhiteSpace(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.IsWhiteSpaceLatin1(s[index]);
			}
			return CharUnicodeInfo.IsWhiteSpace(s, index);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00022211 File Offset: 0x00020411
		public static UnicodeCategory GetUnicodeCategory(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory((int)c);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00022228 File Offset: 0x00020428
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.GetLatin1UnicodeCategory(s[index]);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00022279 File Offset: 0x00020479
		[__DynamicallyInvokable]
		public static double GetNumericValue(char c)
		{
			return CharUnicodeInfo.GetNumericValue(c);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00022281 File Offset: 0x00020481
		[__DynamicallyInvokable]
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.GetNumericValue(s, index);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000222AC File Offset: 0x000204AC
		[__DynamicallyInvokable]
		public static bool IsHighSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udbff';
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000222C3 File Offset: 0x000204C3
		[__DynamicallyInvokable]
		public static bool IsHighSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsHighSurrogate(s[index]);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000222F7 File Offset: 0x000204F7
		[__DynamicallyInvokable]
		public static bool IsLowSurrogate(char c)
		{
			return c >= '\udc00' && c <= '\udfff';
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002230E File Offset: 0x0002050E
		[__DynamicallyInvokable]
		public static bool IsLowSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsLowSurrogate(s[index]);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00022344 File Offset: 0x00020544
		[__DynamicallyInvokable]
		public static bool IsSurrogatePair(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return index + 1 < s.Length && char.IsSurrogatePair(s[index], s[index + 1]);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00022399 File Offset: 0x00020599
		[__DynamicallyInvokable]
		public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
		{
			return highSurrogate >= '\ud800' && highSurrogate <= '\udbff' && lowSurrogate >= '\udc00' && lowSurrogate <= '\udfff';
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000223C4 File Offset: 0x000205C4
		[__DynamicallyInvokable]
		public static string ConvertFromUtf32(int utf32)
		{
			if (utf32 < 0 || utf32 > 1114111 || (utf32 >= 55296 && utf32 <= 57343))
			{
				throw new ArgumentOutOfRangeException("utf32", Environment.GetResourceString("ArgumentOutOfRange_InvalidUTF32"));
			}
			if (utf32 < 65536)
			{
				return char.ToString((char)utf32);
			}
			utf32 -= 65536;
			return new string(new char[]
			{
				(char)(utf32 / 1024 + 55296),
				(char)(utf32 % 1024 + 56320)
			});
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002244C File Offset: 0x0002064C
		[__DynamicallyInvokable]
		public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
		{
			if (!char.IsHighSurrogate(highSurrogate))
			{
				throw new ArgumentOutOfRangeException("highSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidHighSurrogate"));
			}
			if (!char.IsLowSurrogate(lowSurrogate))
			{
				throw new ArgumentOutOfRangeException("lowSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidLowSurrogate"));
			}
			return (int)((highSurrogate - '\ud800') * 'Ѐ' + (lowSurrogate - '\udc00')) + 65536;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000224B0 File Offset: 0x000206B0
		[__DynamicallyInvokable]
		public static int ConvertToUtf32(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int num = (int)(s[index] - '\ud800');
			if (num < 0 || num > 2047)
			{
				return (int)s[index];
			}
			if (num > 1023)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLowSurrogate", new object[] { index }), "s");
			}
			if (index >= s.Length - 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", new object[] { index }), "s");
			}
			int num2 = (int)(s[index + 1] - '\udc00');
			if (num2 >= 0 && num2 <= 1023)
			{
				return num * 1024 + num2 + 65536;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", new object[] { index }), "s");
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000225BF File Offset: 0x000207BF
		// Note: this type is marked as 'beforefieldinit'.
		static Char()
		{
		}

		// Token: 0x040003F7 RID: 1015
		internal char m_value;

		// Token: 0x040003F8 RID: 1016
		[__DynamicallyInvokable]
		public const char MaxValue = '\uffff';

		// Token: 0x040003F9 RID: 1017
		[__DynamicallyInvokable]
		public const char MinValue = '\0';

		// Token: 0x040003FA RID: 1018
		private static readonly byte[] categoryForLatin1 = new byte[]
		{
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 11, 24, 24, 24, 26, 24, 24, 24,
			20, 21, 24, 25, 24, 19, 24, 24, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 24, 24,
			25, 25, 25, 24, 24, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 20, 24, 21, 27, 18, 27, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 20, 25, 21, 25, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			11, 24, 26, 26, 26, 26, 28, 28, 27, 28,
			1, 22, 25, 19, 28, 27, 28, 25, 10, 10,
			27, 1, 28, 24, 27, 10, 1, 23, 10, 10,
			10, 24, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 25, 0, 0, 0, 0,
			0, 0, 0, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 25, 1, 1,
			1, 1, 1, 1, 1, 1
		};

		// Token: 0x040003FB RID: 1019
		internal const int UNICODE_PLANE00_END = 65535;

		// Token: 0x040003FC RID: 1020
		internal const int UNICODE_PLANE01_START = 65536;

		// Token: 0x040003FD RID: 1021
		internal const int UNICODE_PLANE16_END = 1114111;

		// Token: 0x040003FE RID: 1022
		internal const int HIGH_SURROGATE_START = 55296;

		// Token: 0x040003FF RID: 1023
		internal const int LOW_SURROGATE_END = 57343;
	}
}
