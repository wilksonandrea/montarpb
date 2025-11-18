using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000F2 RID: 242
	public sealed class ColorMapper
	{
		// Token: 0x0600093E RID: 2366
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int int_1);

		// Token: 0x0600093F RID: 2367
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetConsoleScreenBufferInfoEx(IntPtr intptr_1, ref ColorMapper.Struct3 struct3_0);

		// Token: 0x06000940 RID: 2368
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool SetConsoleScreenBufferInfoEx(IntPtr intptr_1, ref ColorMapper.Struct3 struct3_0);

		// Token: 0x06000941 RID: 2369 RVA: 0x00007746 File Offset: 0x00005946
		public void MapColor(ConsoleColor oldColor, Color newColor)
		{
			this.method_1(oldColor, (uint)newColor.R, (uint)newColor.G, (uint)newColor.B);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002100C File Offset: 0x0001F20C
		public Dictionary<string, COLORREF> GetBufferColors()
		{
			Dictionary<string, COLORREF> dictionary = new Dictionary<string, COLORREF>();
			IntPtr stdHandle = ColorMapper.GetStdHandle(-11);
			ColorMapper.Struct3 @struct = this.method_0(stdHandle);
			dictionary.Add("black", @struct.colorref_0);
			dictionary.Add("darkBlue", @struct.colorref_1);
			dictionary.Add("darkGreen", @struct.colorref_2);
			dictionary.Add("darkCyan", @struct.colorref_3);
			dictionary.Add("darkRed", @struct.colorref_4);
			dictionary.Add("darkMagenta", @struct.colorref_5);
			dictionary.Add("darkYellow", @struct.colorref_6);
			dictionary.Add("gray", @struct.colorref_7);
			dictionary.Add("darkGray", @struct.colorref_8);
			dictionary.Add("blue", @struct.colorref_9);
			dictionary.Add("green", @struct.colorref_10);
			dictionary.Add("cyan", @struct.colorref_11);
			dictionary.Add("red", @struct.colorref_12);
			dictionary.Add("magenta", @struct.colorref_13);
			dictionary.Add("yellow", @struct.colorref_14);
			dictionary.Add("white", @struct.colorref_15);
			return dictionary;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00021140 File Offset: 0x0001F340
		public void SetBatchBufferColors(Dictionary<string, COLORREF> colors)
		{
			IntPtr stdHandle = ColorMapper.GetStdHandle(-11);
			ColorMapper.Struct3 @struct = this.method_0(stdHandle);
			@struct.colorref_0 = colors["black"];
			@struct.colorref_1 = colors["darkBlue"];
			@struct.colorref_2 = colors["darkGreen"];
			@struct.colorref_3 = colors["darkCyan"];
			@struct.colorref_4 = colors["darkRed"];
			@struct.colorref_5 = colors["darkMagenta"];
			@struct.colorref_6 = colors["darkYellow"];
			@struct.colorref_7 = colors["gray"];
			@struct.colorref_8 = colors["darkGray"];
			@struct.colorref_9 = colors["blue"];
			@struct.colorref_10 = colors["green"];
			@struct.colorref_11 = colors["cyan"];
			@struct.colorref_12 = colors["red"];
			@struct.colorref_13 = colors["magenta"];
			@struct.colorref_14 = colors["yellow"];
			@struct.colorref_15 = colors["white"];
			this.method_2(stdHandle, @struct);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00021288 File Offset: 0x0001F488
		private ColorMapper.Struct3 method_0(IntPtr intptr_1)
		{
			ColorMapper.Struct3 @struct = default(ColorMapper.Struct3);
			@struct.int_0 = Marshal.SizeOf<ColorMapper.Struct3>(@struct);
			if (intptr_1 == ColorMapper.intptr_0)
			{
				throw this.method_3(Marshal.GetLastWin32Error());
			}
			if (!ColorMapper.GetConsoleScreenBufferInfoEx(intptr_1, ref @struct))
			{
				throw this.method_3(Marshal.GetLastWin32Error());
			}
			return @struct;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000212DC File Offset: 0x0001F4DC
		private void method_1(ConsoleColor consoleColor_0, uint uint_0, uint uint_1, uint uint_2)
		{
			IntPtr stdHandle = ColorMapper.GetStdHandle(-11);
			ColorMapper.Struct3 @struct = this.method_0(stdHandle);
			switch (consoleColor_0)
			{
			case ConsoleColor.Black:
				@struct.colorref_0 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.DarkBlue:
				@struct.colorref_1 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.DarkGreen:
				@struct.colorref_2 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.DarkCyan:
				@struct.colorref_3 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.DarkRed:
				@struct.colorref_4 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.DarkMagenta:
				@struct.colorref_5 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.DarkYellow:
				@struct.colorref_6 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.Gray:
				@struct.colorref_7 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.DarkGray:
				@struct.colorref_8 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.Blue:
				@struct.colorref_9 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.Green:
				@struct.colorref_10 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.Cyan:
				@struct.colorref_11 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.Red:
				@struct.colorref_12 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.Magenta:
				@struct.colorref_13 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.Yellow:
				@struct.colorref_14 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			case ConsoleColor.White:
				@struct.colorref_15 = new COLORREF(uint_0, uint_1, uint_2);
				break;
			}
			this.method_2(stdHandle, @struct);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00007764 File Offset: 0x00005964
		private void method_2(IntPtr intptr_1, ColorMapper.Struct3 struct3_0)
		{
			struct3_0.struct2_0.short_3 = struct3_0.struct2_0.short_3 + 1;
			struct3_0.struct2_0.short_2 = struct3_0.struct2_0.short_2 + 1;
			if (!ColorMapper.SetConsoleScreenBufferInfoEx(intptr_1, ref struct3_0))
			{
				throw this.method_3(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00021484 File Offset: 0x0001F684
		private Exception method_3(int int_1)
		{
			if (int_1 == 6)
			{
				return new ConsoleAccessException();
			}
			return new ColorMappingException(int_1);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00002116 File Offset: 0x00000316
		public ColorMapper()
		{
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000077A0 File Offset: 0x000059A0
		// Note: this type is marked as 'beforefieldinit'.
		static ColorMapper()
		{
		}

		// Token: 0x040006CA RID: 1738
		private const int int_0 = -11;

		// Token: 0x040006CB RID: 1739
		private static readonly IntPtr intptr_0 = new IntPtr(-1);

		// Token: 0x020000F3 RID: 243
		private struct Struct1
		{
			// Token: 0x040006CC RID: 1740
			internal short short_0;

			// Token: 0x040006CD RID: 1741
			internal short short_1;
		}

		// Token: 0x020000F4 RID: 244
		private struct Struct2
		{
			// Token: 0x040006CE RID: 1742
			internal short short_0;

			// Token: 0x040006CF RID: 1743
			internal short short_1;

			// Token: 0x040006D0 RID: 1744
			internal short short_2;

			// Token: 0x040006D1 RID: 1745
			internal short short_3;
		}

		// Token: 0x020000F5 RID: 245
		private struct Struct3
		{
			// Token: 0x040006D2 RID: 1746
			internal int int_0;

			// Token: 0x040006D3 RID: 1747
			internal ColorMapper.Struct1 struct1_0;

			// Token: 0x040006D4 RID: 1748
			internal ColorMapper.Struct1 struct1_1;

			// Token: 0x040006D5 RID: 1749
			internal ushort ushort_0;

			// Token: 0x040006D6 RID: 1750
			internal ColorMapper.Struct2 struct2_0;

			// Token: 0x040006D7 RID: 1751
			internal ColorMapper.Struct1 struct1_2;

			// Token: 0x040006D8 RID: 1752
			internal ushort ushort_1;

			// Token: 0x040006D9 RID: 1753
			internal bool bool_0;

			// Token: 0x040006DA RID: 1754
			internal COLORREF colorref_0;

			// Token: 0x040006DB RID: 1755
			internal COLORREF colorref_1;

			// Token: 0x040006DC RID: 1756
			internal COLORREF colorref_2;

			// Token: 0x040006DD RID: 1757
			internal COLORREF colorref_3;

			// Token: 0x040006DE RID: 1758
			internal COLORREF colorref_4;

			// Token: 0x040006DF RID: 1759
			internal COLORREF colorref_5;

			// Token: 0x040006E0 RID: 1760
			internal COLORREF colorref_6;

			// Token: 0x040006E1 RID: 1761
			internal COLORREF colorref_7;

			// Token: 0x040006E2 RID: 1762
			internal COLORREF colorref_8;

			// Token: 0x040006E3 RID: 1763
			internal COLORREF colorref_9;

			// Token: 0x040006E4 RID: 1764
			internal COLORREF colorref_10;

			// Token: 0x040006E5 RID: 1765
			internal COLORREF colorref_11;

			// Token: 0x040006E6 RID: 1766
			internal COLORREF colorref_12;

			// Token: 0x040006E7 RID: 1767
			internal COLORREF colorref_13;

			// Token: 0x040006E8 RID: 1768
			internal COLORREF colorref_14;

			// Token: 0x040006E9 RID: 1769
			internal COLORREF colorref_15;
		}
	}
}
