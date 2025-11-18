namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public sealed class ColorMapper
    {
        private const int int_0 = -11;
        private static readonly IntPtr intptr_0 = new IntPtr(-1);

        public Dictionary<string, COLORREF> GetBufferColors()
        {
            IntPtr stdHandle = GetStdHandle(-11);
            Struct3 struct2 = this.method_0(stdHandle);
            Dictionary<string, COLORREF> dictionary1 = new Dictionary<string, COLORREF>();
            dictionary1.Add("black", struct2.colorref_0);
            dictionary1.Add("darkBlue", struct2.colorref_1);
            dictionary1.Add("darkGreen", struct2.colorref_2);
            dictionary1.Add("darkCyan", struct2.colorref_3);
            dictionary1.Add("darkRed", struct2.colorref_4);
            dictionary1.Add("darkMagenta", struct2.colorref_5);
            dictionary1.Add("darkYellow", struct2.colorref_6);
            dictionary1.Add("gray", struct2.colorref_7);
            dictionary1.Add("darkGray", struct2.colorref_8);
            dictionary1.Add("blue", struct2.colorref_9);
            dictionary1.Add("green", struct2.colorref_10);
            dictionary1.Add("cyan", struct2.colorref_11);
            dictionary1.Add("red", struct2.colorref_12);
            dictionary1.Add("magenta", struct2.colorref_13);
            dictionary1.Add("yellow", struct2.colorref_14);
            dictionary1.Add("white", struct2.colorref_15);
            return dictionary1;
        }

        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern bool GetConsoleScreenBufferInfoEx(IntPtr intptr_1, ref Struct3 struct3_0);
        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern IntPtr GetStdHandle(int int_1);
        public void MapColor(ConsoleColor oldColor, Color newColor)
        {
            this.method_1(oldColor, newColor.R, newColor.G, newColor.B);
        }

        private Struct3 method_0(IntPtr intptr_1)
        {
            Struct3 structure = new Struct3();
            structure.int_0 = Marshal.SizeOf<Struct3>(structure);
            if (intptr_1 == intptr_0)
            {
                throw this.method_3(Marshal.GetLastWin32Error());
            }
            if (!GetConsoleScreenBufferInfoEx(intptr_1, ref structure))
            {
                throw this.method_3(Marshal.GetLastWin32Error());
            }
            return structure;
        }

        private void method_1(ConsoleColor consoleColor_0, uint uint_0, uint uint_1, uint uint_2)
        {
            IntPtr stdHandle = GetStdHandle(-11);
            Struct3 struct2 = this.method_0(stdHandle);
            switch (consoleColor_0)
            {
                case ConsoleColor.Black:
                    struct2.colorref_0 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.DarkBlue:
                    struct2.colorref_1 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.DarkGreen:
                    struct2.colorref_2 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.DarkCyan:
                    struct2.colorref_3 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.DarkRed:
                    struct2.colorref_4 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.DarkMagenta:
                    struct2.colorref_5 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.DarkYellow:
                    struct2.colorref_6 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.Gray:
                    struct2.colorref_7 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.DarkGray:
                    struct2.colorref_8 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.Blue:
                    struct2.colorref_9 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.Green:
                    struct2.colorref_10 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.Cyan:
                    struct2.colorref_11 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.Red:
                    struct2.colorref_12 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.Magenta:
                    struct2.colorref_13 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.Yellow:
                    struct2.colorref_14 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                case ConsoleColor.White:
                    struct2.colorref_15 = new COLORREF(uint_0, uint_1, uint_2);
                    break;

                default:
                    break;
            }
            this.method_2(stdHandle, struct2);
        }

        private unsafe void method_2(IntPtr intptr_1, Struct3 struct3_0)
        {
            short* numPtr1 = &struct3_0.struct2_0.short_3;
            numPtr1[0] = (short) (numPtr1[0] + 1);
            short* numPtr2 = &struct3_0.struct2_0.short_2;
            numPtr2[0] = (short) (numPtr2[0] + 1);
            if (!SetConsoleScreenBufferInfoEx(intptr_1, ref struct3_0))
            {
                throw this.method_3(Marshal.GetLastWin32Error());
            }
        }

        private Exception method_3(int int_1) => 
            (int_1 != 6) ? ((Exception) new ColorMappingException(int_1)) : ((Exception) new ConsoleAccessException());

        public void SetBatchBufferColors(Dictionary<string, COLORREF> colors)
        {
            IntPtr stdHandle = GetStdHandle(-11);
            Struct3 struct2 = this.method_0(stdHandle);
            struct2.colorref_0 = colors["black"];
            struct2.colorref_1 = colors["darkBlue"];
            struct2.colorref_2 = colors["darkGreen"];
            struct2.colorref_3 = colors["darkCyan"];
            struct2.colorref_4 = colors["darkRed"];
            struct2.colorref_5 = colors["darkMagenta"];
            struct2.colorref_6 = colors["darkYellow"];
            struct2.colorref_7 = colors["gray"];
            struct2.colorref_8 = colors["darkGray"];
            struct2.colorref_9 = colors["blue"];
            struct2.colorref_10 = colors["green"];
            struct2.colorref_11 = colors["cyan"];
            struct2.colorref_12 = colors["red"];
            struct2.colorref_13 = colors["magenta"];
            struct2.colorref_14 = colors["yellow"];
            struct2.colorref_15 = colors["white"];
            this.method_2(stdHandle, struct2);
        }

        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern bool SetConsoleScreenBufferInfoEx(IntPtr intptr_1, ref Struct3 struct3_0);

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct1
        {
            internal short short_0;
            internal short short_1;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct2
        {
            internal short short_0;
            internal short short_1;
            internal short short_2;
            internal short short_3;
        }

        [StructLayout(LayoutKind.Sequential)]
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

