// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.FigletFont
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Plugin.Core.Colorful;

public class FigletFont
{
  public FigletFont(FigletFont gparam_0)
  {
    ((Figlet) this).figletFont_0 = gparam_0 != null ? gparam_0 : throw new ArgumentNullException("font");
  }

  public StyledString ToAscii([In] string obj0)
  {
    if (obj0 == null)
      throw new ArgumentNullException("value");
    if (Encoding.UTF8.GetByteCount(obj0) != obj0.Length)
      throw new ArgumentException("String contains non-ascii characters");
    StringBuilder stringBuilder = new StringBuilder();
    int length = FigletFont.smethod_1(((Figlet) this).figletFont_0, obj0);
    char[,] chArray = new char[((Figlet) this).figletFont_0.Height + 1, length];
    int[,] numArray = new int[((Figlet) this).figletFont_0.Height + 1, length];
    Color[,] colorArray = new Color[((Figlet) this).figletFont_0.Height + 1, length];
    for (int index1 = 1; index1 <= ((Figlet) this).figletFont_0.Height; ++index1)
    {
      int num = 0;
      for (int index2 = 0; index2 < obj0.Length; ++index2)
      {
        string gparam_0 = FigletFont.smethod_2(((Figlet) this).figletFont_0, obj0[index2], index1);
        stringBuilder.Append(gparam_0);
        FigletFont.smethod_0(gparam_0, index2, num, index1, chArray, numArray);
        num += gparam_0.Length;
      }
      stringBuilder.AppendLine();
    }
    Styler ascii = new Styler(obj0, stringBuilder.ToString());
    ((StyledString) ascii).CharacterGeometry = chArray;
    ((StyledString) ascii).CharacterIndexGeometry = numArray;
    ((StyledString) ascii).ColorGeometry = colorArray;
    return (StyledString) ascii;
  }

  private static void smethod_0(
    string gparam_0,
    [In] int obj1,
    [In] int obj2,
    [In] int obj3,
    [In] char[,] obj4,
    [In] int[,] obj5)
  {
    for (int index = obj2; index < obj2 + gparam_0.Length; ++index)
    {
      obj4[obj3, index] = gparam_0[index - obj2];
      obj5[obj3, index] = obj1;
    }
  }

  private static int smethod_1([In] FigletFont obj0, string int_0)
  {
    List<int> source = new List<int>();
    foreach (char ch in int_0)
    {
      int num = 0;
      for (int index = 1; index <= obj0.Height; ++index)
      {
        string str = FigletFont.smethod_2(obj0, ch, index);
        num = str.Length > num ? str.Length : num;
      }
      source.Add(num);
    }
    return source.Sum();
  }

  private static string smethod_2([In] FigletFont obj0, [In] char obj1, [In] int obj2)
  {
    int num = obj0.CommentLines + (Convert.ToInt32(obj1) - 32 /*0x20*/) * obj0.Height;
    string line = obj0.Lines[num + obj2];
    char ch = line[line.Length - 1];
    string str = Regex.Replace(line, $"\\{ch.ToString()}{{1,2}}$", string.Empty);
    if (obj0.Kerning > 0)
      str += new string(' ', obj0.Kerning);
    return str.Replace(obj0.HardBlank, " ");
  }

  public static FigletFont Default => Formatter.Parse(DefaultFonts.SmallSlant);

  public int BaseLine { get; private set; }

  public int CodeTagCount { get; [param: In] private set; }

  public int CommentLines { get; private set; }

  public int FullLayout { get; [param: In] private set; }

  public string HardBlank { get; [param: In] private set; }

  public int Height { get; private set; }

  public int Kerning { get; private set; }

  public string[] Lines { get; private set; }

  public int MaxLength { get; private set; }

  public int OldLayout { get; private set; }

  public int PrintDirection { get; private set; }

  public string Signature { get; private set; }

  public static FigletFont Load(byte[] value)
  {
    using (MemoryStream memoryStream = new MemoryStream(value))
      return FigletFont.Load((Stream) memoryStream);
  }

  public static FigletFont Load(Stream value)
  {
    if (value == null)
      throw new ArgumentNullException("stream");
    List<string> stringList = new List<string>();
    using (StreamReader streamReader = new StreamReader(value))
    {
      while (!streamReader.EndOfStream)
        stringList.Add(streamReader.ReadLine());
    }
    return Formatter.Parse((IEnumerable<string>) stringList);
  }
}
