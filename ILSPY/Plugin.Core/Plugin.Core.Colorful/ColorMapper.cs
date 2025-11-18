using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Plugin.Core.Colorful;

public sealed class ColorMapper
{
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

		internal Struct1 struct1_0;

		internal Struct1 struct1_1;

		internal ushort ushort_0;

		internal Struct2 struct2_0;

		internal Struct1 struct1_2;

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

	private const int int_0 = -11;

	private static readonly IntPtr intptr_0 = new IntPtr(-1);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern IntPtr GetStdHandle(int int_1);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern bool GetConsoleScreenBufferInfoEx(IntPtr intptr_1, ref Struct3 struct3_0);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern bool SetConsoleScreenBufferInfoEx(IntPtr intptr_1, ref Struct3 struct3_0);

	public void MapColor(ConsoleColor oldColor, Color newColor)
	{
		method_1(oldColor, newColor.R, newColor.G, newColor.B);
	}

	public Dictionary<string, COLORREF> GetBufferColors()
	{
		Dictionary<string, COLORREF> dictionary = new Dictionary<string, COLORREF>();
		IntPtr stdHandle = GetStdHandle(-11);
		Struct3 @struct = method_0(stdHandle);
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

	public void SetBatchBufferColors(Dictionary<string, COLORREF> colors)
	{
		IntPtr stdHandle = GetStdHandle(-11);
		Struct3 struct3_ = method_0(stdHandle);
		struct3_.colorref_0 = colors["black"];
		struct3_.colorref_1 = colors["darkBlue"];
		struct3_.colorref_2 = colors["darkGreen"];
		struct3_.colorref_3 = colors["darkCyan"];
		struct3_.colorref_4 = colors["darkRed"];
		struct3_.colorref_5 = colors["darkMagenta"];
		struct3_.colorref_6 = colors["darkYellow"];
		struct3_.colorref_7 = colors["gray"];
		struct3_.colorref_8 = colors["darkGray"];
		struct3_.colorref_9 = colors["blue"];
		struct3_.colorref_10 = colors["green"];
		struct3_.colorref_11 = colors["cyan"];
		struct3_.colorref_12 = colors["red"];
		struct3_.colorref_13 = colors["magenta"];
		struct3_.colorref_14 = colors["yellow"];
		struct3_.colorref_15 = colors["white"];
		method_2(stdHandle, struct3_);
	}

	private Struct3 method_0(IntPtr intptr_1)
	{
		Struct3 struct3_ = default(Struct3);
		struct3_.int_0 = Marshal.SizeOf(struct3_);
		if (intptr_1 == intptr_0)
		{
			throw method_3(Marshal.GetLastWin32Error());
		}
		if (!GetConsoleScreenBufferInfoEx(intptr_1, ref struct3_))
		{
			throw method_3(Marshal.GetLastWin32Error());
		}
		return struct3_;
	}

	private void method_1(ConsoleColor consoleColor_0, uint uint_0, uint uint_1, uint uint_2)
	{
		IntPtr stdHandle = GetStdHandle(-11);
		Struct3 struct3_ = method_0(stdHandle);
		switch (consoleColor_0)
		{
		case ConsoleColor.Black:
			struct3_.colorref_0 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.DarkBlue:
			struct3_.colorref_1 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.DarkGreen:
			struct3_.colorref_2 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.DarkCyan:
			struct3_.colorref_3 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.DarkRed:
			struct3_.colorref_4 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.DarkMagenta:
			struct3_.colorref_5 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.DarkYellow:
			struct3_.colorref_6 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.Gray:
			struct3_.colorref_7 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.DarkGray:
			struct3_.colorref_8 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.Blue:
			struct3_.colorref_9 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.Green:
			struct3_.colorref_10 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.Cyan:
			struct3_.colorref_11 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.Red:
			struct3_.colorref_12 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.Magenta:
			struct3_.colorref_13 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.Yellow:
			struct3_.colorref_14 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		case ConsoleColor.White:
			struct3_.colorref_15 = new COLORREF(uint_0, uint_1, uint_2);
			break;
		}
		method_2(stdHandle, struct3_);
	}

	private void method_2(IntPtr intptr_1, Struct3 struct3_0)
	{
		struct3_0.struct2_0.short_3++;
		struct3_0.struct2_0.short_2++;
		if (!SetConsoleScreenBufferInfoEx(intptr_1, ref struct3_0))
		{
			throw method_3(Marshal.GetLastWin32Error());
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
}
