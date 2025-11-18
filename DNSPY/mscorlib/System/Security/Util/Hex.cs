using System;

namespace System.Security.Util
{
	// Token: 0x0200037B RID: 891
	internal static class Hex
	{
		// Token: 0x06002C2D RID: 11309 RVA: 0x000A42CB File Offset: 0x000A24CB
		private static char HexDigit(int num)
		{
			return (char)((num < 10) ? (num + 48) : (num + 55));
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000A42E0 File Offset: 0x000A24E0
		public static string EncodeHexString(byte[] sArray)
		{
			string text = null;
			if (sArray != null)
			{
				char[] array = new char[sArray.Length * 2];
				int i = 0;
				int num = 0;
				while (i < sArray.Length)
				{
					int num2 = (sArray[i] & 240) >> 4;
					array[num++] = Hex.HexDigit(num2);
					num2 = (int)(sArray[i] & 15);
					array[num++] = Hex.HexDigit(num2);
					i++;
				}
				text = new string(array);
			}
			return text;
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000A4348 File Offset: 0x000A2548
		internal static string EncodeHexStringFromInt(byte[] sArray)
		{
			string text = null;
			if (sArray != null)
			{
				char[] array = new char[sArray.Length * 2];
				int num = sArray.Length;
				int num2 = 0;
				while (num-- > 0)
				{
					int num3 = (sArray[num] & 240) >> 4;
					array[num2++] = Hex.HexDigit(num3);
					num3 = (int)(sArray[num] & 15);
					array[num2++] = Hex.HexDigit(num3);
				}
				text = new string(array);
			}
			return text;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000A43B0 File Offset: 0x000A25B0
		public static int ConvertHexDigit(char val)
		{
			if (val <= '9' && val >= '0')
			{
				return (int)(val - '0');
			}
			if (val >= 'a' && val <= 'f')
			{
				return (int)(val - 'a' + '\n');
			}
			if (val >= 'A' && val <= 'F')
			{
				return (int)(val - 'A' + '\n');
			}
			throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"));
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000A4400 File Offset: 0x000A2600
		public static byte[] DecodeHexString(string hexString)
		{
			if (hexString == null)
			{
				throw new ArgumentNullException("hexString");
			}
			bool flag = false;
			int i = 0;
			int num = hexString.Length;
			if (num >= 2 && hexString[0] == '0' && (hexString[1] == 'x' || hexString[1] == 'X'))
			{
				num = hexString.Length - 2;
				i = 2;
			}
			if (num % 2 != 0 && num % 3 != 2)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHexFormat"));
			}
			byte[] array;
			if (num >= 3 && hexString[i + 2] == ' ')
			{
				flag = true;
				array = new byte[num / 3 + 1];
			}
			else
			{
				array = new byte[num / 2];
			}
			int num2 = 0;
			while (i < hexString.Length)
			{
				int num3 = Hex.ConvertHexDigit(hexString[i]);
				int num4 = Hex.ConvertHexDigit(hexString[i + 1]);
				array[num2] = (byte)(num4 | (num3 << 4));
				if (flag)
				{
					i++;
				}
				i += 2;
				num2++;
			}
			return array;
		}
	}
}
