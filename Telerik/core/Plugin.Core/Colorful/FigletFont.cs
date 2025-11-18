using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public class FigletFont
	{
		public int BaseLine
		{
			get;
			private set;
		}

		public int CodeTagCount
		{
			get;
			private set;
		}

		public int CommentLines
		{
			get;
			private set;
		}

		public static FigletFont Default
		{
			get
			{
				return FigletFont.Parse(DefaultFonts.SmallSlant);
			}
		}

		public int FullLayout
		{
			get;
			private set;
		}

		public string HardBlank
		{
			get;
			private set;
		}

		public int Height
		{
			get;
			private set;
		}

		public int Kerning
		{
			get;
			private set;
		}

		public string[] Lines
		{
			get;
			private set;
		}

		public int MaxLength
		{
			get;
			private set;
		}

		public int OldLayout
		{
			get;
			private set;
		}

		public int PrintDirection
		{
			get;
			private set;
		}

		public string Signature
		{
			get;
			private set;
		}

		public FigletFont()
		{
		}

		public static FigletFont Load(byte[] bytes)
		{
			FigletFont figletFont;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				figletFont = FigletFont.Load(memoryStream);
			}
			return figletFont;
		}

		public static FigletFont Load(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			List<string> strs = new List<string>();
			using (StreamReader streamReader = new StreamReader(stream))
			{
				while (!streamReader.EndOfStream)
				{
					strs.Add(streamReader.ReadLine());
				}
			}
			return FigletFont.Parse(strs);
		}

		public static FigletFont Load(string filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			return FigletFont.Parse(File.ReadLines(filePath));
		}

		public static FigletFont Parse(string fontContent)
		{
			if (fontContent == null)
			{
				throw new ArgumentNullException("fontContent");
			}
			return FigletFont.Parse(fontContent.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
		}

		public static FigletFont Parse(IEnumerable<string> fontLines)
		{
			if (fontLines == null)
			{
				throw new ArgumentNullException("fontLines");
			}
			FigletFont figletFont = new FigletFont()
			{
				Lines = fontLines.ToArray<string>()
			};
			string[] strArrays = figletFont.Lines.First<string>().Split(new char[] { ' ' });
			figletFont.Signature = strArrays.First<string>().Remove(strArrays.First<string>().Length - 1);
			if (figletFont.Signature == "flf2a")
			{
				char chr = strArrays.First<string>().Last<char>();
				figletFont.HardBlank = chr.ToString();
				figletFont.Height = FigletFont.smethod_0(strArrays, 1);
				figletFont.BaseLine = FigletFont.smethod_0(strArrays, 2);
				figletFont.MaxLength = FigletFont.smethod_0(strArrays, 3);
				figletFont.OldLayout = FigletFont.smethod_0(strArrays, 4);
				figletFont.CommentLines = FigletFont.smethod_0(strArrays, 5);
				figletFont.PrintDirection = FigletFont.smethod_0(strArrays, 6);
				figletFont.FullLayout = FigletFont.smethod_0(strArrays, 7);
				figletFont.CodeTagCount = FigletFont.smethod_0(strArrays, 8);
			}
			return figletFont;
		}

		private static int smethod_0(string[] string_3, int int_9)
		{
			int ınt32 = 0;
			if ((int)string_3.Length > int_9)
			{
				int.TryParse(string_3[int_9], out ınt32);
			}
			return ınt32;
		}
	}
}