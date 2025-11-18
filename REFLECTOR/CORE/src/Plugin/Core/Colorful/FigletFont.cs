namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class FigletFont
    {
        public static FigletFont Load(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return Load(stream);
            }
        }

        public static FigletFont Load(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            List<string> fontLines = new List<string>();
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    fontLines.Add(reader.ReadLine());
                }
            }
            return Parse(fontLines);
        }

        public static FigletFont Load(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }
            return Parse(File.ReadLines(filePath));
        }

        public static FigletFont Parse(IEnumerable<string> fontLines)
        {
            if (fontLines == null)
            {
                throw new ArgumentNullException("fontLines");
            }
            FigletFont font1 = new FigletFont();
            font1.Lines = fontLines.ToArray<string>();
            FigletFont font = font1;
            char[] separator = new char[] { ' ' };
            string[] source = font.Lines.First<string>().Split(separator);
            font.Signature = source.First<string>().Remove(source.First<string>().Length - 1);
            if (font.Signature == "flf2a")
            {
                font.HardBlank = source.First<string>().Last<char>().ToString();
                font.Height = smethod_0(source, 1);
                font.BaseLine = smethod_0(source, 2);
                font.MaxLength = smethod_0(source, 3);
                font.OldLayout = smethod_0(source, 4);
                font.CommentLines = smethod_0(source, 5);
                font.PrintDirection = smethod_0(source, 6);
                font.FullLayout = smethod_0(source, 7);
                font.CodeTagCount = smethod_0(source, 8);
            }
            return font;
        }

        public static FigletFont Parse(string fontContent)
        {
            if (fontContent == null)
            {
                throw new ArgumentNullException("fontContent");
            }
            string[] separator = new string[] { "\r\n", "\r", "\n" };
            return Parse(fontContent.Split(separator, StringSplitOptions.None));
        }

        private static int smethod_0(string[] string_3, int int_9)
        {
            int result = 0;
            if (string_3.Length > int_9)
            {
                int.TryParse(string_3[int_9], out result);
            }
            return result;
        }

        public static FigletFont Default =>
            Parse(DefaultFonts.SmallSlant);

        public int BaseLine { get; private set; }

        public int CodeTagCount { get; private set; }

        public int CommentLines { get; private set; }

        public int FullLayout { get; private set; }

        public string HardBlank { get; private set; }

        public int Height { get; private set; }

        public int Kerning { get; private set; }

        public string[] Lines { get; private set; }

        public int MaxLength { get; private set; }

        public int OldLayout { get; private set; }

        public int PrintDirection { get; private set; }

        public string Signature { get; private set; }
    }
}

