using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public class FigletFont
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private int int_5;

	[CompilerGenerated]
	private string[] string_1;

	[CompilerGenerated]
	private int int_6;

	[CompilerGenerated]
	private int int_7;

	[CompilerGenerated]
	private int int_8;

	[CompilerGenerated]
	private string string_2;

	public static FigletFont Default => Parse(DefaultFonts.SmallSlant);

	public int BaseLine
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		private set
		{
			int_0 = value;
		}
	}

	public int CodeTagCount
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		private set
		{
			int_1 = value;
		}
	}

	public int CommentLines
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		private set
		{
			int_2 = value;
		}
	}

	public int FullLayout
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		private set
		{
			int_3 = value;
		}
	}

	public string HardBlank
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		private set
		{
			string_0 = value;
		}
	}

	public int Height
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		private set
		{
			int_4 = value;
		}
	}

	public int Kerning
	{
		[CompilerGenerated]
		get
		{
			return int_5;
		}
		[CompilerGenerated]
		private set
		{
			int_5 = value;
		}
	}

	public string[] Lines
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		private set
		{
			string_1 = value;
		}
	}

	public int MaxLength
	{
		[CompilerGenerated]
		get
		{
			return int_6;
		}
		[CompilerGenerated]
		private set
		{
			int_6 = value;
		}
	}

	public int OldLayout
	{
		[CompilerGenerated]
		get
		{
			return int_7;
		}
		[CompilerGenerated]
		private set
		{
			int_7 = value;
		}
	}

	public int PrintDirection
	{
		[CompilerGenerated]
		get
		{
			return int_8;
		}
		[CompilerGenerated]
		private set
		{
			int_8 = value;
		}
	}

	public string Signature
	{
		[CompilerGenerated]
		get
		{
			return string_2;
		}
		[CompilerGenerated]
		private set
		{
			string_2 = value;
		}
	}

	public static FigletFont Load(byte[] bytes)
	{
		using MemoryStream stream = new MemoryStream(bytes);
		return Load(stream);
	}

	public static FigletFont Load(Stream stream)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		List<string> list = new List<string>();
		using (StreamReader streamReader = new StreamReader(stream))
		{
			while (!streamReader.EndOfStream)
			{
				list.Add(streamReader.ReadLine());
			}
		}
		return Parse(list);
	}

	public static FigletFont Load(string filePath)
	{
		if (filePath == null)
		{
			throw new ArgumentNullException("filePath");
		}
		return Parse(File.ReadLines(filePath));
	}

	public static FigletFont Parse(string fontContent)
	{
		if (fontContent == null)
		{
			throw new ArgumentNullException("fontContent");
		}
		return Parse(fontContent.Split(new string[3] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
	}

	public static FigletFont Parse(IEnumerable<string> fontLines)
	{
		if (fontLines == null)
		{
			throw new ArgumentNullException("fontLines");
		}
		FigletFont figletFont = new FigletFont
		{
			Lines = fontLines.ToArray()
		};
		string[] array = figletFont.Lines.First().Split(' ');
		figletFont.Signature = array.First().Remove(array.First().Length - 1);
		if (figletFont.Signature == "flf2a")
		{
			figletFont.HardBlank = array.First().Last().ToString();
			figletFont.Height = smethod_0(array, 1);
			figletFont.BaseLine = smethod_0(array, 2);
			figletFont.MaxLength = smethod_0(array, 3);
			figletFont.OldLayout = smethod_0(array, 4);
			figletFont.CommentLines = smethod_0(array, 5);
			figletFont.PrintDirection = smethod_0(array, 6);
			figletFont.FullLayout = smethod_0(array, 7);
			figletFont.CodeTagCount = smethod_0(array, 8);
		}
		return figletFont;
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
}
