using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Plugin.Core.Colorful
{
	public sealed class ColorMapper
	{
		private const int int_0 = -11;

		private readonly static IntPtr intptr_0;

		static ColorMapper()
		{
			ColorMapper.intptr_0 = new IntPtr(-1);
		}

		public ColorMapper()
		{
		}

		public Dictionary<string, COLORREF> GetBufferColors()
		{
			Dictionary<string, COLORREF> strs = new Dictionary<string, COLORREF>();
			ColorMapper.Struct3 struct3 = this.method_0(ColorMapper.GetStdHandle(-11));
			strs.Add("black", struct3.colorref_0);
			strs.Add("darkBlue", struct3.colorref_1);
			strs.Add("darkGreen", struct3.colorref_2);
			strs.Add("darkCyan", struct3.colorref_3);
			strs.Add("darkRed", struct3.colorref_4);
			strs.Add("darkMagenta", struct3.colorref_5);
			strs.Add("darkYellow", struct3.colorref_6);
			strs.Add("gray", struct3.colorref_7);
			strs.Add("darkGray", struct3.colorref_8);
			strs.Add("blue", struct3.colorref_9);
			strs.Add("green", struct3.colorref_10);
			strs.Add("cyan", struct3.colorref_11);
			strs.Add("red", struct3.colorref_12);
			strs.Add("magenta", struct3.colorref_13);
			strs.Add("yellow", struct3.colorref_14);
			strs.Add("white", struct3.colorref_15);
			return strs;
		}

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern bool GetConsoleScreenBufferInfoEx(IntPtr intptr_1, ref ColorMapper.Struct3 struct3_0);

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern IntPtr GetStdHandle(int int_1);

		public void MapColor(ConsoleColor oldColor, Color newColor)
		{
			this.method_1(oldColor, newColor.R, newColor.G, newColor.B);
		}

		private ColorMapper.Struct3 method_0(IntPtr intptr_1)
		{
			ColorMapper.Struct3 struct3 = new ColorMapper.Struct3()
			{
				int_0 = Marshal.SizeOf<ColorMapper.Struct3>(struct3)
			};
			if (intptr_1 == ColorMapper.intptr_0)
			{
				throw this.method_3(Marshal.GetLastWin32Error());
			}
			if (!ColorMapper.GetConsoleScreenBufferInfoEx(intptr_1, ref struct3))
			{
				throw this.method_3(Marshal.GetLastWin32Error());
			}
			return struct3;
		}

		private void method_1(ConsoleColor consoleColor_0, uint uint_0, uint uint_1, uint uint_2)
		{
			IntPtr stdHandle = ColorMapper.GetStdHandle(-11);
			ColorMapper.Struct3 cOLORREF = this.method_0(stdHandle);
			switch (consoleColor_0)
			{
				case ConsoleColor.Black:
				{
					cOLORREF.colorref_0 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.DarkBlue:
				{
					cOLORREF.colorref_1 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.DarkGreen:
				{
					cOLORREF.colorref_2 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.DarkCyan:
				{
					cOLORREF.colorref_3 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.DarkRed:
				{
					cOLORREF.colorref_4 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.DarkMagenta:
				{
					cOLORREF.colorref_5 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.DarkYellow:
				{
					cOLORREF.colorref_6 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.Gray:
				{
					cOLORREF.colorref_7 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.DarkGray:
				{
					cOLORREF.colorref_8 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.Blue:
				{
					cOLORREF.colorref_9 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.Green:
				{
					cOLORREF.colorref_10 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.Cyan:
				{
					cOLORREF.colorref_11 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.Red:
				{
					cOLORREF.colorref_12 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.Magenta:
				{
					cOLORREF.colorref_13 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.Yellow:
				{
					cOLORREF.colorref_14 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
				case ConsoleColor.White:
				{
					cOLORREF.colorref_15 = new COLORREF(uint_0, uint_1, uint_2);
					break;
				}
			}
			this.method_2(stdHandle, cOLORREF);
		}

		private void method_2(IntPtr intptr_1, ColorMapper.Struct3 struct3_0)
		{
			ref short short3 = ref struct3_0.struct2_0.short_3;
			short3 = (short)(short3 + 1);
			ref short short2 = ref struct3_0.struct2_0.short_2;
			short2 = (short)(short2 + 1);
			if (!ColorMapper.SetConsoleScreenBufferInfoEx(intptr_1, ref struct3_0))
			{
				throw this.method_3(Marshal.GetLastWin32Error());
			}
		}

		private Exception method_3(int int_1)
		{
			if (int_1 == 6)
			{
				return new ConsoleAccessException();
			}
			return new ColorMappingException(int_1);
		}

		public void SetBatchBufferColors(Dictionary<string, COLORREF> colors)
		{
			IntPtr stdHandle = ColorMapper.GetStdHandle(-11);
			ColorMapper.Struct3 ıtem = this.method_0(stdHandle);
			ıtem.colorref_0 = colors["black"];
			ıtem.colorref_1 = colors["darkBlue"];
			ıtem.colorref_2 = colors["darkGreen"];
			ıtem.colorref_3 = colors["darkCyan"];
			ıtem.colorref_4 = colors["darkRed"];
			ıtem.colorref_5 = colors["darkMagenta"];
			ıtem.colorref_6 = colors["darkYellow"];
			ıtem.colorref_7 = colors["gray"];
			ıtem.colorref_8 = colors["darkGray"];
			ıtem.colorref_9 = colors["blue"];
			ıtem.colorref_10 = colors["green"];
			ıtem.colorref_11 = colors["cyan"];
			ıtem.colorref_12 = colors["red"];
			ıtem.colorref_13 = colors["magenta"];
			ıtem.colorref_14 = colors["yellow"];
			ıtem.colorref_15 = colors["white"];
			this.method_2(stdHandle, ıtem);
		}

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern bool SetConsoleScreenBufferInfoEx(IntPtr intptr_1, ref ColorMapper.Struct3 struct3_0);

		private struct Struct1
		{
			internal short short_0;

			internal short short_1;
		}

		private struct Struct2
		{
			internal short short_0;

			internal short short_1;

			internal short short_2;

			internal short short_3;
		}

		private struct Struct3
		{
			internal int int_0;

			internal ColorMapper.Struct1 struct1_0;

			internal ColorMapper.Struct1 struct1_1;

			internal ushort ushort_0;

			internal ColorMapper.Struct2 struct2_0;

			internal ColorMapper.Struct1 struct1_2;

			internal ushort ushort_1;

			internal bool bool_0;

			internal COLORREF colorref_0;

			internal COLORREF colorref_1;

			internal COLORREF colorref_2;

			internal COLORREF colorref_3;

			internal COLORREF colorref_4;

			internal COLORREF colorref_5;

			internal COLORREF colorref_6;

			internal COLORREF colorref_7;

			internal COLORREF colorref_8;

			internal COLORREF colorref_9;

			internal COLORREF colorref_10;

			internal COLORREF colorref_11;

			internal COLORREF colorref_12;

			internal COLORREF colorref_13;

			internal COLORREF colorref_14;

			internal COLORREF colorref_15;
		}
	}
}