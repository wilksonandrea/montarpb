using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000101 RID: 257
	public class FigletFont
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x000079A6 File Offset: 0x00005BA6
		public static FigletFont Default
		{
			get
			{
				return FigletFont.Parse(DefaultFonts.SmallSlant);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x000079B2 File Offset: 0x00005BB2
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x000079BA File Offset: 0x00005BBA
		public int BaseLine
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			private set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x000079C3 File Offset: 0x00005BC3
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x000079CB File Offset: 0x00005BCB
		public int CodeTagCount
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			private set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x000079D4 File Offset: 0x00005BD4
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x000079DC File Offset: 0x00005BDC
		public int CommentLines
		{
			[CompilerGenerated]
			get
			{
				return this.int_2;
			}
			[CompilerGenerated]
			private set
			{
				this.int_2 = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x000079E5 File Offset: 0x00005BE5
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x000079ED File Offset: 0x00005BED
		public int FullLayout
		{
			[CompilerGenerated]
			get
			{
				return this.int_3;
			}
			[CompilerGenerated]
			private set
			{
				this.int_3 = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x000079F6 File Offset: 0x00005BF6
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x000079FE File Offset: 0x00005BFE
		public string HardBlank
		{
			[CompilerGenerated]
			get
			{
				return this.string_0;
			}
			[CompilerGenerated]
			private set
			{
				this.string_0 = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00007A07 File Offset: 0x00005C07
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00007A0F File Offset: 0x00005C0F
		public int Height
		{
			[CompilerGenerated]
			get
			{
				return this.int_4;
			}
			[CompilerGenerated]
			private set
			{
				this.int_4 = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00007A18 File Offset: 0x00005C18
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x00007A20 File Offset: 0x00005C20
		public int Kerning
		{
			[CompilerGenerated]
			get
			{
				return this.int_5;
			}
			[CompilerGenerated]
			private set
			{
				this.int_5 = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00007A29 File Offset: 0x00005C29
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00007A31 File Offset: 0x00005C31
		public string[] Lines
		{
			[CompilerGenerated]
			get
			{
				return this.string_1;
			}
			[CompilerGenerated]
			private set
			{
				this.string_1 = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00007A3A File Offset: 0x00005C3A
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x00007A42 File Offset: 0x00005C42
		public int MaxLength
		{
			[CompilerGenerated]
			get
			{
				return this.int_6;
			}
			[CompilerGenerated]
			private set
			{
				this.int_6 = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00007A4B File Offset: 0x00005C4B
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00007A53 File Offset: 0x00005C53
		public int OldLayout
		{
			[CompilerGenerated]
			get
			{
				return this.int_7;
			}
			[CompilerGenerated]
			private set
			{
				this.int_7 = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00007A5C File Offset: 0x00005C5C
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x00007A64 File Offset: 0x00005C64
		public int PrintDirection
		{
			[CompilerGenerated]
			get
			{
				return this.int_8;
			}
			[CompilerGenerated]
			private set
			{
				this.int_8 = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00007A6D File Offset: 0x00005C6D
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x00007A75 File Offset: 0x00005C75
		public string Signature
		{
			[CompilerGenerated]
			get
			{
				return this.string_2;
			}
			[CompilerGenerated]
			private set
			{
				this.string_2 = value;
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00021B74 File Offset: 0x0001FD74
		public static FigletFont Load(byte[] bytes)
		{
			FigletFont figletFont;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				figletFont = FigletFont.Load(memoryStream);
			}
			return figletFont;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00021BAC File Offset: 0x0001FDAC
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
			return FigletFont.Parse(list);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00007A7E File Offset: 0x00005C7E
		public static FigletFont Load(string filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			return FigletFont.Parse(File.ReadLines(filePath));
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00007A99 File Offset: 0x00005C99
		public static FigletFont Parse(string fontContent)
		{
			if (fontContent == null)
			{
				throw new ArgumentNullException("fontContent");
			}
			return FigletFont.Parse(fontContent.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00021C10 File Offset: 0x0001FE10
		public static FigletFont Parse(IEnumerable<string> fontLines)
		{
			if (fontLines == null)
			{
				throw new ArgumentNullException("fontLines");
			}
			FigletFont figletFont = new FigletFont
			{
				Lines = fontLines.ToArray<string>()
			};
			string[] array = figletFont.Lines.First<string>().Split(new char[] { ' ' });
			figletFont.Signature = array.First<string>().Remove(array.First<string>().Length - 1);
			if (figletFont.Signature == "flf2a")
			{
				figletFont.HardBlank = array.First<string>().Last<char>().ToString();
				figletFont.Height = FigletFont.smethod_0(array, 1);
				figletFont.BaseLine = FigletFont.smethod_0(array, 2);
				figletFont.MaxLength = FigletFont.smethod_0(array, 3);
				figletFont.OldLayout = FigletFont.smethod_0(array, 4);
				figletFont.CommentLines = FigletFont.smethod_0(array, 5);
				figletFont.PrintDirection = FigletFont.smethod_0(array, 6);
				figletFont.FullLayout = FigletFont.smethod_0(array, 7);
				figletFont.CodeTagCount = FigletFont.smethod_0(array, 8);
			}
			return figletFont;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00021D10 File Offset: 0x0001FF10
		private static int smethod_0(string[] string_3, int int_9)
		{
			int num = 0;
			if (string_3.Length > int_9)
			{
				int.TryParse(string_3[int_9], out num);
			}
			return num;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00002116 File Offset: 0x00000316
		public FigletFont()
		{
		}

		// Token: 0x04000700 RID: 1792
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000701 RID: 1793
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000702 RID: 1794
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000703 RID: 1795
		[CompilerGenerated]
		private int int_3;

		// Token: 0x04000704 RID: 1796
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000705 RID: 1797
		[CompilerGenerated]
		private int int_4;

		// Token: 0x04000706 RID: 1798
		[CompilerGenerated]
		private int int_5;

		// Token: 0x04000707 RID: 1799
		[CompilerGenerated]
		private string[] string_1;

		// Token: 0x04000708 RID: 1800
		[CompilerGenerated]
		private int int_6;

		// Token: 0x04000709 RID: 1801
		[CompilerGenerated]
		private int int_7;

		// Token: 0x0400070A RID: 1802
		[CompilerGenerated]
		private int int_8;

		// Token: 0x0400070B RID: 1803
		[CompilerGenerated]
		private string string_2;
	}
}
