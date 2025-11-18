namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Figlet
    {
        private readonly FigletFont figletFont_0;

        public Figlet()
        {
            this.figletFont_0 = FigletFont.Default;
        }

        public Figlet(FigletFont figletFont_1)
        {
            if (figletFont_1 == null)
            {
                throw new ArgumentNullException("font");
            }
            this.figletFont_0 = figletFont_1;
        }

        private static void smethod_0(string string_0, int int_0, int int_1, int int_2, char[,] char_0, int[,] int_3)
        {
            for (int i = int_1; i < (int_1 + string_0.Length); i++)
            {
                char_0[int_2, i] = string_0[i - int_1];
                int_3[int_2, i] = int_0;
            }
        }

        private static int smethod_1(FigletFont figletFont_1, string string_0)
        {
            List<int> list = new List<int>();
            string str = string_0;
            int num = 0;
            while (num < str.Length)
            {
                char ch = str[num];
                int item = 0;
                int num3 = 1;
                while (true)
                {
                    if (num3 > figletFont_1.Height)
                    {
                        list.Add(item);
                        num++;
                        break;
                    }
                    string str2 = smethod_2(figletFont_1, ch, num3);
                    item = (str2.Length > item) ? str2.Length : item;
                    num3++;
                }
            }
            return ((IEnumerable<int>) list).Sum();
        }

        private static string smethod_2(FigletFont figletFont_1, char char_0, int int_0)
        {
            int num = figletFont_1.CommentLines + ((Convert.ToInt32(char_0) - 0x20) * figletFont_1.Height);
            string input = figletFont_1.Lines[num + int_0];
            input = Regex.Replace(input, @"\" + input[input.Length - 1].ToString() + "{1,2}$", string.Empty);
            if (figletFont_1.Kerning > 0)
            {
                input = input + new string(' ', figletFont_1.Kerning);
            }
            return input.Replace(figletFont_1.HardBlank, " ");
        }

        public StyledString ToAscii(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (Encoding.UTF8.GetByteCount(value) != value.Length)
            {
                throw new ArgumentException("String contains non-ascii characters");
            }
            StringBuilder builder = new StringBuilder();
            int num = smethod_1(this.figletFont_0, value);
            char[,] chArray = new char[this.figletFont_0.Height + 1, num];
            int[,] numArray = new int[this.figletFont_0.Height + 1, num];
            Color[,] colorArray = new Color[this.figletFont_0.Height + 1, num];
            int num2 = 1;
            while (num2 <= this.figletFont_0.Height)
            {
                int num3 = 0;
                int num4 = 0;
                while (true)
                {
                    if (num4 >= value.Length)
                    {
                        builder.AppendLine();
                        num2++;
                        break;
                    }
                    char ch = value[num4];
                    string str = smethod_2(this.figletFont_0, ch, num2);
                    builder.Append(str);
                    smethod_0(str, num4, num3, num2, chArray, numArray);
                    num3 += str.Length;
                    num4++;
                }
            }
            StyledString text1 = new StyledString(value, builder.ToString());
            text1.CharacterGeometry = chArray;
            text1.CharacterIndexGeometry = numArray;
            text1.ColorGeometry = colorArray;
            return text1;
        }
    }
}

