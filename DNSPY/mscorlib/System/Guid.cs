using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using Microsoft.Win32;

namespace System
{
	// Token: 0x020000EB RID: 235
	[ComVisible(true)]
	[NonVersionable]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Guid : IFormattable, IComparable, IComparable<Guid>, IEquatable<Guid>
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002D35C File Offset: 0x0002B55C
		[__DynamicallyInvokable]
		public Guid(byte[] b)
		{
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			if (b.Length != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_GuidArrayCtor", new object[] { "16" }));
			}
			this._a = ((int)b[3] << 24) | ((int)b[2] << 16) | ((int)b[1] << 8) | (int)b[0];
			this._b = (short)(((int)b[5] << 8) | (int)b[4]);
			this._c = (short)(((int)b[7] << 8) | (int)b[6]);
			this._d = b[8];
			this._e = b[9];
			this._f = b[10];
			this._g = b[11];
			this._h = b[12];
			this._i = b[13];
			this._j = b[14];
			this._k = b[15];
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002D428 File Offset: 0x0002B628
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public Guid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = (int)a;
			this._b = (short)b;
			this._c = (short)c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0002D48C File Offset: 0x0002B68C
		[__DynamicallyInvokable]
		public Guid(int a, short b, short c, byte[] d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			if (d.Length != 8)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_GuidArrayCtor", new object[] { "8" }));
			}
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d[0];
			this._e = d[1];
			this._f = d[2];
			this._g = d[3];
			this._h = d[4];
			this._i = d[5];
			this._j = d[6];
			this._k = d[7];
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002D534 File Offset: 0x0002B734
		[__DynamicallyInvokable]
		public Guid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002D598 File Offset: 0x0002B798
		[__DynamicallyInvokable]
		public Guid(string g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			this = Guid.Empty;
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.All);
			if (Guid.TryParseGuid(g, Guid.GuidStyles.Any, ref guidResult))
			{
				this = guidResult.parsedGuid;
				return;
			}
			throw guidResult.GetGuidParseException();
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
		[__DynamicallyInvokable]
		public static Guid Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.AllButOverflow);
			if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref guidResult))
			{
				return guidResult.parsedGuid;
			}
			throw guidResult.GetGuidParseException();
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0002D638 File Offset: 0x0002B838
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out Guid result)
		{
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.None);
			if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref guidResult))
			{
				result = guidResult.parsedGuid;
				return true;
			}
			result = Guid.Empty;
			return false;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002D67C File Offset: 0x0002B87C
		[__DynamicallyInvokable]
		public static Guid ParseExact(string input, string format)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
			}
			char c = format[0];
			Guid.GuidStyles guidStyles;
			if (c == 'D' || c == 'd')
			{
				guidStyles = Guid.GuidStyles.RequireDashes;
			}
			else if (c == 'N' || c == 'n')
			{
				guidStyles = Guid.GuidStyles.None;
			}
			else if (c == 'B' || c == 'b')
			{
				guidStyles = Guid.GuidStyles.BraceFormat;
			}
			else if (c == 'P' || c == 'p')
			{
				guidStyles = Guid.GuidStyles.ParenthesisFormat;
			}
			else
			{
				if (c != 'X' && c != 'x')
				{
					throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
				}
				guidStyles = Guid.GuidStyles.HexFormat;
			}
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.AllButOverflow);
			if (Guid.TryParseGuid(input, guidStyles, ref guidResult))
			{
				return guidResult.parsedGuid;
			}
			throw guidResult.GetGuidParseException();
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002D74C File Offset: 0x0002B94C
		[__DynamicallyInvokable]
		public static bool TryParseExact(string input, string format, out Guid result)
		{
			if (format == null || format.Length != 1)
			{
				result = Guid.Empty;
				return false;
			}
			char c = format[0];
			Guid.GuidStyles guidStyles;
			if (c == 'D' || c == 'd')
			{
				guidStyles = Guid.GuidStyles.RequireDashes;
			}
			else if (c == 'N' || c == 'n')
			{
				guidStyles = Guid.GuidStyles.None;
			}
			else if (c == 'B' || c == 'b')
			{
				guidStyles = Guid.GuidStyles.BraceFormat;
			}
			else if (c == 'P' || c == 'p')
			{
				guidStyles = Guid.GuidStyles.ParenthesisFormat;
			}
			else
			{
				if (c != 'X' && c != 'x')
				{
					result = Guid.Empty;
					return false;
				}
				guidStyles = Guid.GuidStyles.HexFormat;
			}
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.None);
			if (Guid.TryParseGuid(input, guidStyles, ref guidResult))
			{
				result = guidResult.parsedGuid;
				return true;
			}
			result = Guid.Empty;
			return false;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002D80C File Offset: 0x0002BA0C
		private static bool TryParseGuid(string g, Guid.GuidStyles flags, ref Guid.GuidResult result)
		{
			if (g == null)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
				return false;
			}
			string text = g.Trim();
			if (text.Length == 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
				return false;
			}
			bool flag = text.IndexOf('-', 0) >= 0;
			if (flag)
			{
				if ((flags & (Guid.GuidStyles.AllowDashes | Guid.GuidStyles.RequireDashes)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireDashes) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
				return false;
			}
			bool flag2 = text.IndexOf('{', 0) >= 0;
			if (flag2)
			{
				if ((flags & (Guid.GuidStyles.AllowBraces | Guid.GuidStyles.RequireBraces)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireBraces) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
				return false;
			}
			bool flag3 = text.IndexOf('(', 0) >= 0;
			if (flag3)
			{
				if ((flags & (Guid.GuidStyles.AllowParenthesis | Guid.GuidStyles.RequireParenthesis)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireParenthesis) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
				return false;
			}
			bool flag4;
			try
			{
				if (flag)
				{
					flag4 = Guid.TryParseGuidWithDashes(text, ref result);
				}
				else if (flag2)
				{
					flag4 = Guid.TryParseGuidWithHexPrefix(text, ref result);
				}
				else
				{
					flag4 = Guid.TryParseGuidWithNoStyle(text, ref result);
				}
			}
			catch (IndexOutOfRangeException ex)
			{
				result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Format_GuidUnrecognized", null, null, ex);
				flag4 = false;
			}
			catch (ArgumentException ex2)
			{
				result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Format_GuidUnrecognized", null, null, ex2);
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002D96C File Offset: 0x0002BB6C
		private static bool TryParseGuidWithHexPrefix(string guidString, ref Guid.GuidResult result)
		{
			guidString = Guid.EatAllWhitespace(guidString);
			if (string.IsNullOrEmpty(guidString) || guidString[0] != '{')
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidBrace");
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", "{0xdddddddd, etc}");
				return false;
			}
			int num = 3;
			int num2 = guidString.IndexOf(',', num) - num;
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
				return false;
			}
			if (!Guid.StringToInt(guidString.Substring(num, num2), -1, 4096, out result.parsedGuid._a, ref result))
			{
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", "{0xdddddddd, 0xdddd, etc}");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.IndexOf(',', num) - num;
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
				return false;
			}
			if (!Guid.StringToShort(guidString.Substring(num, num2), -1, 4096, out result.parsedGuid._b, ref result))
			{
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", "{0xdddddddd, 0xdddd, 0xdddd, etc}");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.IndexOf(',', num) - num;
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
				return false;
			}
			if (!Guid.StringToShort(guidString.Substring(num, num2), -1, 4096, out result.parsedGuid._c, ref result))
			{
				return false;
			}
			if (guidString.Length <= num + num2 + 1 || guidString[num + num2 + 1] != '{')
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidBrace");
				return false;
			}
			num2++;
			byte[] array = new byte[8];
			for (int i = 0; i < 8; i++)
			{
				if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", "{... { ... 0xdd, ...}}");
					return false;
				}
				num = num + num2 + 3;
				if (i < 7)
				{
					num2 = guidString.IndexOf(',', num) - num;
					if (num2 <= 0)
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
						return false;
					}
				}
				else
				{
					num2 = guidString.IndexOf('}', num) - num;
					if (num2 <= 0)
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidBraceAfterLastNumber");
						return false;
					}
				}
				uint num3 = (uint)Convert.ToInt32(guidString.Substring(num, num2), 16);
				if (num3 > 255U)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Overflow_Byte");
					return false;
				}
				array[i] = (byte)num3;
			}
			result.parsedGuid._d = array[0];
			result.parsedGuid._e = array[1];
			result.parsedGuid._f = array[2];
			result.parsedGuid._g = array[3];
			result.parsedGuid._h = array[4];
			result.parsedGuid._i = array[5];
			result.parsedGuid._j = array[6];
			result.parsedGuid._k = array[7];
			if (num + num2 + 1 >= guidString.Length || guidString[num + num2 + 1] != '}')
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidEndBrace");
				return false;
			}
			if (num + num2 + 1 != guidString.Length - 1)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_ExtraJunkAtEnd");
				return false;
			}
			return true;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0002DC68 File Offset: 0x0002BE68
		private static bool TryParseGuidWithNoStyle(string guidString, ref Guid.GuidResult result)
		{
			int num = 0;
			int num2 = 0;
			if (guidString.Length != 32)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
				return false;
			}
			foreach (char c in guidString)
			{
				if (c < '0' || c > '9')
				{
					char c2 = char.ToUpper(c, CultureInfo.InvariantCulture);
					if (c2 < 'A' || c2 > 'F')
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvalidChar");
						return false;
					}
				}
			}
			if (!Guid.StringToInt(guidString.Substring(num, 8), -1, 4096, out result.parsedGuid._a, ref result))
			{
				return false;
			}
			num += 8;
			if (!Guid.StringToShort(guidString.Substring(num, 4), -1, 4096, out result.parsedGuid._b, ref result))
			{
				return false;
			}
			num += 4;
			if (!Guid.StringToShort(guidString.Substring(num, 4), -1, 4096, out result.parsedGuid._c, ref result))
			{
				return false;
			}
			num += 4;
			int num3;
			if (!Guid.StringToInt(guidString.Substring(num, 4), -1, 4096, out num3, ref result))
			{
				return false;
			}
			num += 4;
			num2 = num;
			long num4;
			if (!Guid.StringToLong(guidString, ref num2, num, out num4, ref result))
			{
				return false;
			}
			if (num2 - num != 12)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
				return false;
			}
			result.parsedGuid._d = (byte)(num3 >> 8);
			result.parsedGuid._e = (byte)num3;
			num3 = (int)(num4 >> 32);
			result.parsedGuid._f = (byte)(num3 >> 8);
			result.parsedGuid._g = (byte)num3;
			num3 = (int)num4;
			result.parsedGuid._h = (byte)(num3 >> 24);
			result.parsedGuid._i = (byte)(num3 >> 16);
			result.parsedGuid._j = (byte)(num3 >> 8);
			result.parsedGuid._k = (byte)num3;
			return true;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0002DE1C File Offset: 0x0002C01C
		private static bool TryParseGuidWithDashes(string guidString, ref Guid.GuidResult result)
		{
			int num = 0;
			int num2 = 0;
			if (guidString[0] == '{')
			{
				if (guidString.Length != 38 || guidString[37] != '}')
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
					return false;
				}
				num = 1;
			}
			else if (guidString[0] == '(')
			{
				if (guidString.Length != 38 || guidString[37] != ')')
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
					return false;
				}
				num = 1;
			}
			else if (guidString.Length != 36)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
				return false;
			}
			if (guidString[8 + num] != '-' || guidString[13 + num] != '-' || guidString[18 + num] != '-' || guidString[23 + num] != '-')
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidDashes");
				return false;
			}
			num2 = num;
			int num3;
			if (!Guid.StringToInt(guidString, ref num2, 8, 8192, out num3, ref result))
			{
				return false;
			}
			result.parsedGuid._a = num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			result.parsedGuid._b = (short)num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			result.parsedGuid._c = (short)num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			num2++;
			num = num2;
			long num4;
			if (!Guid.StringToLong(guidString, ref num2, 8192, out num4, ref result))
			{
				return false;
			}
			if (num2 - num != 12)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
				return false;
			}
			result.parsedGuid._d = (byte)(num3 >> 8);
			result.parsedGuid._e = (byte)num3;
			num3 = (int)(num4 >> 32);
			result.parsedGuid._f = (byte)(num3 >> 8);
			result.parsedGuid._g = (byte)num3;
			num3 = (int)num4;
			result.parsedGuid._h = (byte)(num3 >> 24);
			result.parsedGuid._i = (byte)(num3 >> 16);
			result.parsedGuid._j = (byte)(num3 >> 8);
			result.parsedGuid._k = (byte)num3;
			return true;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0002E025 File Offset: 0x0002C225
		[SecuritySafeCritical]
		private static bool StringToShort(string str, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			return Guid.StringToShort(str, null, requiredLength, flags, out result, ref parseResult);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002E034 File Offset: 0x0002C234
		[SecuritySafeCritical]
		private unsafe static bool StringToShort(string str, ref int parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			fixed (int* ptr = &parsePos)
			{
				int* ptr2 = ptr;
				return Guid.StringToShort(str, ptr2, requiredLength, flags, out result, ref parseResult);
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0002E054 File Offset: 0x0002C254
		[SecurityCritical]
		private unsafe static bool StringToShort(string str, int* parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			result = 0;
			int num;
			bool flag = Guid.StringToInt(str, parsePos, requiredLength, flags, out num, ref parseResult);
			result = (short)num;
			return flag;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002E079 File Offset: 0x0002C279
		[SecuritySafeCritical]
		private static bool StringToInt(string str, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			return Guid.StringToInt(str, null, requiredLength, flags, out result, ref parseResult);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002E088 File Offset: 0x0002C288
		[SecuritySafeCritical]
		private unsafe static bool StringToInt(string str, ref int parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			fixed (int* ptr = &parsePos)
			{
				int* ptr2 = ptr;
				return Guid.StringToInt(str, ptr2, requiredLength, flags, out result, ref parseResult);
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002E0A8 File Offset: 0x0002C2A8
		[SecurityCritical]
		private unsafe static bool StringToInt(string str, int* parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			result = 0;
			int num = ((parsePos == null) ? 0 : (*parsePos));
			try
			{
				result = ParseNumbers.StringToInt(str, 16, flags, parsePos);
			}
			catch (OverflowException ex)
			{
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.All)
				{
					throw;
				}
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
				{
					throw new FormatException(Environment.GetResourceString("Format_GuidUnrecognized"), ex);
				}
				parseResult.SetFailure(ex);
				return false;
			}
			catch (Exception ex2)
			{
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.None)
				{
					parseResult.SetFailure(ex2);
					return false;
				}
				throw;
			}
			if (requiredLength != -1 && parsePos != null && *parsePos - num != requiredLength)
			{
				parseResult.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvalidChar");
				return false;
			}
			return true;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002E160 File Offset: 0x0002C360
		[SecuritySafeCritical]
		private static bool StringToLong(string str, int flags, out long result, ref Guid.GuidResult parseResult)
		{
			return Guid.StringToLong(str, null, flags, out result, ref parseResult);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002E170 File Offset: 0x0002C370
		[SecuritySafeCritical]
		private unsafe static bool StringToLong(string str, ref int parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
		{
			fixed (int* ptr = &parsePos)
			{
				int* ptr2 = ptr;
				return Guid.StringToLong(str, ptr2, flags, out result, ref parseResult);
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002E190 File Offset: 0x0002C390
		[SecuritySafeCritical]
		private unsafe static bool StringToLong(string str, int* parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
		{
			result = 0L;
			try
			{
				result = ParseNumbers.StringToLong(str, 16, flags, parsePos);
			}
			catch (OverflowException ex)
			{
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.All)
				{
					throw;
				}
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
				{
					throw new FormatException(Environment.GetResourceString("Format_GuidUnrecognized"), ex);
				}
				parseResult.SetFailure(ex);
				return false;
			}
			catch (Exception ex2)
			{
				if (parseResult.throwStyle == Guid.GuidParseThrowStyle.None)
				{
					parseResult.SetFailure(ex2);
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0002E21C File Offset: 0x0002C41C
		private static string EatAllWhitespace(string str)
		{
			int num = 0;
			char[] array = new char[str.Length];
			foreach (char c in str)
			{
				if (!char.IsWhiteSpace(c))
				{
					array[num++] = c;
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0002E268 File Offset: 0x0002C468
		private static bool IsHexPrefix(string str, int i)
		{
			return str.Length > i + 1 && str[i] == '0' && char.ToLower(str[i + 1], CultureInfo.InvariantCulture) == 'x';
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002E29C File Offset: 0x0002C49C
		[__DynamicallyInvokable]
		public byte[] ToByteArray()
		{
			return new byte[]
			{
				(byte)this._a,
				(byte)(this._a >> 8),
				(byte)(this._a >> 16),
				(byte)(this._a >> 24),
				(byte)this._b,
				(byte)(this._b >> 8),
				(byte)this._c,
				(byte)(this._c >> 8),
				this._d,
				this._e,
				this._f,
				this._g,
				this._h,
				this._i,
				this._j,
				this._k
			};
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002E35D File Offset: 0x0002C55D
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ToString("D", null);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002E36B File Offset: 0x0002C56B
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this._a ^ (((int)this._b << 16) | (int)((ushort)this._c)) ^ (((int)this._f << 24) | (int)this._k);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002E398 File Offset: 0x0002C598
		[__DynamicallyInvokable]
		public override bool Equals(object o)
		{
			if (o == null || !(o is Guid))
			{
				return false;
			}
			Guid guid = (Guid)o;
			return guid._a == this._a && guid._b == this._b && guid._c == this._c && guid._d == this._d && guid._e == this._e && guid._f == this._f && guid._g == this._g && guid._h == this._h && guid._i == this._i && guid._j == this._j && guid._k == this._k;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0002E46C File Offset: 0x0002C66C
		[__DynamicallyInvokable]
		public bool Equals(Guid g)
		{
			return g._a == this._a && g._b == this._b && g._c == this._c && g._d == this._d && g._e == this._e && g._f == this._f && g._g == this._g && g._h == this._h && g._i == this._i && g._j == this._j && g._k == this._k;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0002E52A File Offset: 0x0002C72A
		private int GetResult(uint me, uint them)
		{
			if (me < them)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002E534 File Offset: 0x0002C734
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is Guid))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeGuid"));
			}
			Guid guid = (Guid)value;
			if (guid._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)guid._a);
			}
			if (guid._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)guid._b);
			}
			if (guid._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)guid._c);
			}
			if (guid._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)guid._d);
			}
			if (guid._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)guid._e);
			}
			if (guid._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)guid._f);
			}
			if (guid._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)guid._g);
			}
			if (guid._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)guid._h);
			}
			if (guid._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)guid._i);
			}
			if (guid._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)guid._j);
			}
			if (guid._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)guid._k);
			}
			return 0;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002E6D4 File Offset: 0x0002C8D4
		[__DynamicallyInvokable]
		public int CompareTo(Guid value)
		{
			if (value._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)value._a);
			}
			if (value._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)value._b);
			}
			if (value._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)value._c);
			}
			if (value._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)value._d);
			}
			if (value._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)value._e);
			}
			if (value._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)value._f);
			}
			if (value._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)value._g);
			}
			if (value._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)value._h);
			}
			if (value._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)value._i);
			}
			if (value._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)value._j);
			}
			if (value._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)value._k);
			}
			return 0;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002E850 File Offset: 0x0002CA50
		[__DynamicallyInvokable]
		public static bool operator ==(Guid a, Guid b)
		{
			return a._a == b._a && a._b == b._b && a._c == b._c && a._d == b._d && a._e == b._e && a._f == b._f && a._g == b._g && a._h == b._h && a._i == b._i && a._j == b._j && a._k == b._k;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0002E90E File Offset: 0x0002CB0E
		[__DynamicallyInvokable]
		public static bool operator !=(Guid a, Guid b)
		{
			return !(a == b);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002E91C File Offset: 0x0002CB1C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static Guid NewGuid()
		{
			Guid guid;
			Marshal.ThrowExceptionForHR(Win32Native.CoCreateGuid(out guid), new IntPtr(-1));
			return guid;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0002E93C File Offset: 0x0002CB3C
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0002E946 File Offset: 0x0002CB46
		private static char HexToChar(int a)
		{
			a &= 15;
			return (char)((a > 9) ? (a - 10 + 97) : (a + 48));
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0002E961 File Offset: 0x0002CB61
		[SecurityCritical]
		private unsafe static int HexsToChars(char* guidChars, int offset, int a, int b)
		{
			return Guid.HexsToChars(guidChars, offset, a, b, false);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002E970 File Offset: 0x0002CB70
		[SecurityCritical]
		private unsafe static int HexsToChars(char* guidChars, int offset, int a, int b, bool hex)
		{
			if (hex)
			{
				guidChars[offset++] = '0';
				guidChars[offset++] = 'x';
			}
			guidChars[offset++] = Guid.HexToChar(a >> 4);
			guidChars[offset++] = Guid.HexToChar(a);
			if (hex)
			{
				guidChars[offset++] = ',';
				guidChars[offset++] = '0';
				guidChars[offset++] = 'x';
			}
			guidChars[offset++] = Guid.HexToChar(b >> 4);
			guidChars[offset++] = Guid.HexToChar(b);
			return offset;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002EA18 File Offset: 0x0002CC18
		[SecuritySafeCritical]
		public unsafe string ToString(string format, IFormatProvider provider)
		{
			if (format == null || format.Length == 0)
			{
				format = "D";
			}
			int num = 0;
			bool flag = true;
			bool flag2 = false;
			if (format.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
			}
			char c = format[0];
			string text;
			if (c == 'D' || c == 'd')
			{
				text = string.FastAllocateString(36);
			}
			else if (c == 'N' || c == 'n')
			{
				text = string.FastAllocateString(32);
				flag = false;
			}
			else if (c == 'B' || c == 'b')
			{
				text = string.FastAllocateString(38);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					ptr[(IntPtr)(num++) * 2] = '{';
					ptr[37] = '}';
				}
			}
			else if (c == 'P' || c == 'p')
			{
				text = string.FastAllocateString(38);
				fixed (string text3 = text)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					ptr2[(IntPtr)(num++) * 2] = '(';
					ptr2[37] = ')';
				}
			}
			else
			{
				if (c != 'X' && c != 'x')
				{
					throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
				}
				text = string.FastAllocateString(68);
				fixed (string text4 = text)
				{
					char* ptr3 = text4;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					ptr3[(IntPtr)(num++) * 2] = '{';
					ptr3[67] = '}';
				}
				flag = false;
				flag2 = true;
			}
			fixed (string text5 = text)
			{
				char* ptr4 = text5;
				if (ptr4 != null)
				{
					ptr4 += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (flag2)
				{
					ptr4[(IntPtr)(num++) * 2] = '0';
					ptr4[(IntPtr)(num++) * 2] = 'x';
					num = Guid.HexsToChars(ptr4, num, this._a >> 24, this._a >> 16);
					num = Guid.HexsToChars(ptr4, num, this._a >> 8, this._a);
					ptr4[(IntPtr)(num++) * 2] = ',';
					ptr4[(IntPtr)(num++) * 2] = '0';
					ptr4[(IntPtr)(num++) * 2] = 'x';
					num = Guid.HexsToChars(ptr4, num, this._b >> 8, (int)this._b);
					ptr4[(IntPtr)(num++) * 2] = ',';
					ptr4[(IntPtr)(num++) * 2] = '0';
					ptr4[(IntPtr)(num++) * 2] = 'x';
					num = Guid.HexsToChars(ptr4, num, this._c >> 8, (int)this._c);
					ptr4[(IntPtr)(num++) * 2] = ',';
					ptr4[(IntPtr)(num++) * 2] = '{';
					num = Guid.HexsToChars(ptr4, num, (int)this._d, (int)this._e, true);
					ptr4[(IntPtr)(num++) * 2] = ',';
					num = Guid.HexsToChars(ptr4, num, (int)this._f, (int)this._g, true);
					ptr4[(IntPtr)(num++) * 2] = ',';
					num = Guid.HexsToChars(ptr4, num, (int)this._h, (int)this._i, true);
					ptr4[(IntPtr)(num++) * 2] = ',';
					num = Guid.HexsToChars(ptr4, num, (int)this._j, (int)this._k, true);
					ptr4[(IntPtr)(num++) * 2] = '}';
				}
				else
				{
					num = Guid.HexsToChars(ptr4, num, this._a >> 24, this._a >> 16);
					num = Guid.HexsToChars(ptr4, num, this._a >> 8, this._a);
					if (flag)
					{
						ptr4[(IntPtr)(num++) * 2] = '-';
					}
					num = Guid.HexsToChars(ptr4, num, this._b >> 8, (int)this._b);
					if (flag)
					{
						ptr4[(IntPtr)(num++) * 2] = '-';
					}
					num = Guid.HexsToChars(ptr4, num, this._c >> 8, (int)this._c);
					if (flag)
					{
						ptr4[(IntPtr)(num++) * 2] = '-';
					}
					num = Guid.HexsToChars(ptr4, num, (int)this._d, (int)this._e);
					if (flag)
					{
						ptr4[(IntPtr)(num++) * 2] = '-';
					}
					num = Guid.HexsToChars(ptr4, num, (int)this._f, (int)this._g);
					num = Guid.HexsToChars(ptr4, num, (int)this._h, (int)this._i);
					num = Guid.HexsToChars(ptr4, num, (int)this._j, (int)this._k);
				}
			}
			return text;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002EE1B File Offset: 0x0002D01B
		// Note: this type is marked as 'beforefieldinit'.
		static Guid()
		{
		}

		// Token: 0x0400058D RID: 1421
		[__DynamicallyInvokable]
		public static readonly Guid Empty;

		// Token: 0x0400058E RID: 1422
		private int _a;

		// Token: 0x0400058F RID: 1423
		private short _b;

		// Token: 0x04000590 RID: 1424
		private short _c;

		// Token: 0x04000591 RID: 1425
		private byte _d;

		// Token: 0x04000592 RID: 1426
		private byte _e;

		// Token: 0x04000593 RID: 1427
		private byte _f;

		// Token: 0x04000594 RID: 1428
		private byte _g;

		// Token: 0x04000595 RID: 1429
		private byte _h;

		// Token: 0x04000596 RID: 1430
		private byte _i;

		// Token: 0x04000597 RID: 1431
		private byte _j;

		// Token: 0x04000598 RID: 1432
		private byte _k;

		// Token: 0x02000AEC RID: 2796
		[Flags]
		private enum GuidStyles
		{
			// Token: 0x0400318F RID: 12687
			None = 0,
			// Token: 0x04003190 RID: 12688
			AllowParenthesis = 1,
			// Token: 0x04003191 RID: 12689
			AllowBraces = 2,
			// Token: 0x04003192 RID: 12690
			AllowDashes = 4,
			// Token: 0x04003193 RID: 12691
			AllowHexPrefix = 8,
			// Token: 0x04003194 RID: 12692
			RequireParenthesis = 16,
			// Token: 0x04003195 RID: 12693
			RequireBraces = 32,
			// Token: 0x04003196 RID: 12694
			RequireDashes = 64,
			// Token: 0x04003197 RID: 12695
			RequireHexPrefix = 128,
			// Token: 0x04003198 RID: 12696
			HexFormat = 160,
			// Token: 0x04003199 RID: 12697
			NumberFormat = 0,
			// Token: 0x0400319A RID: 12698
			DigitFormat = 64,
			// Token: 0x0400319B RID: 12699
			BraceFormat = 96,
			// Token: 0x0400319C RID: 12700
			ParenthesisFormat = 80,
			// Token: 0x0400319D RID: 12701
			Any = 15
		}

		// Token: 0x02000AED RID: 2797
		private enum GuidParseThrowStyle
		{
			// Token: 0x0400319F RID: 12703
			None,
			// Token: 0x040031A0 RID: 12704
			All,
			// Token: 0x040031A1 RID: 12705
			AllButOverflow
		}

		// Token: 0x02000AEE RID: 2798
		private enum ParseFailureKind
		{
			// Token: 0x040031A3 RID: 12707
			None,
			// Token: 0x040031A4 RID: 12708
			ArgumentNull,
			// Token: 0x040031A5 RID: 12709
			Format,
			// Token: 0x040031A6 RID: 12710
			FormatWithParameter,
			// Token: 0x040031A7 RID: 12711
			NativeException,
			// Token: 0x040031A8 RID: 12712
			FormatWithInnerException
		}

		// Token: 0x02000AEF RID: 2799
		private struct GuidResult
		{
			// Token: 0x06006A0C RID: 27148 RVA: 0x0016D08A File Offset: 0x0016B28A
			internal void Init(Guid.GuidParseThrowStyle canThrow)
			{
				this.parsedGuid = Guid.Empty;
				this.throwStyle = canThrow;
			}

			// Token: 0x06006A0D RID: 27149 RVA: 0x0016D09E File Offset: 0x0016B29E
			internal void SetFailure(Exception nativeException)
			{
				this.m_failure = Guid.ParseFailureKind.NativeException;
				this.m_innerException = nativeException;
			}

			// Token: 0x06006A0E RID: 27150 RVA: 0x0016D0AE File Offset: 0x0016B2AE
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID)
			{
				this.SetFailure(failure, failureMessageID, null, null, null);
			}

			// Token: 0x06006A0F RID: 27151 RVA: 0x0016D0BB File Offset: 0x0016B2BB
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, null, null);
			}

			// Token: 0x06006A10 RID: 27152 RVA: 0x0016D0C8 File Offset: 0x0016B2C8
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName, Exception innerException)
			{
				this.m_failure = failure;
				this.m_failureMessageID = failureMessageID;
				this.m_failureMessageFormatArgument = failureMessageFormatArgument;
				this.m_failureArgumentName = failureArgumentName;
				this.m_innerException = innerException;
				if (this.throwStyle != Guid.GuidParseThrowStyle.None)
				{
					throw this.GetGuidParseException();
				}
			}

			// Token: 0x06006A11 RID: 27153 RVA: 0x0016D100 File Offset: 0x0016B300
			internal Exception GetGuidParseException()
			{
				switch (this.m_failure)
				{
				case Guid.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this.m_failureArgumentName, Environment.GetResourceString(this.m_failureMessageID));
				case Guid.ParseFailureKind.Format:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID));
				case Guid.ParseFailureKind.FormatWithParameter:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID, new object[] { this.m_failureMessageFormatArgument }));
				case Guid.ParseFailureKind.NativeException:
					return this.m_innerException;
				case Guid.ParseFailureKind.FormatWithInnerException:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID), this.m_innerException);
				default:
					return new FormatException(Environment.GetResourceString("Format_GuidUnrecognized"));
				}
			}

			// Token: 0x040031A9 RID: 12713
			internal Guid parsedGuid;

			// Token: 0x040031AA RID: 12714
			internal Guid.GuidParseThrowStyle throwStyle;

			// Token: 0x040031AB RID: 12715
			internal Guid.ParseFailureKind m_failure;

			// Token: 0x040031AC RID: 12716
			internal string m_failureMessageID;

			// Token: 0x040031AD RID: 12717
			internal object m_failureMessageFormatArgument;

			// Token: 0x040031AE RID: 12718
			internal string m_failureArgumentName;

			// Token: 0x040031AF RID: 12719
			internal Exception m_innerException;
		}
	}
}
