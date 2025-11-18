// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.Formatter
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class Formatter
{
  private StyleClass<object> styleClass_0;

  public static FigletFont Load(string value)
  {
    return value != null ? Formatter.Parse(File.ReadLines(value)) : throw new ArgumentNullException("filePath");
  }

  public static FigletFont Parse(string value)
  {
    return value != null ? Formatter.Parse((IEnumerable<string>) value.Split(new string[3]
    {
      "\r\n",
      "\r",
      "\n"
    }, StringSplitOptions.None)) : throw new ArgumentNullException("fontContent");
  }

  public static FigletFont Parse(IEnumerable<string> value)
  {
    if (value == null)
      throw new ArgumentNullException("fontLines");
    FrequencyBasedColorAlternator basedColorAlternator = new FrequencyBasedColorAlternator();
    ((FigletFont) basedColorAlternator).Lines = value.ToArray<string>();
    FigletFont figletFont = (FigletFont) basedColorAlternator;
    string[] strArray = ((IEnumerable<string>) figletFont.Lines).First<string>().Split(' ');
    figletFont.Signature = ((IEnumerable<string>) strArray).First<string>().Remove(((IEnumerable<string>) strArray).First<string>().Length - 1);
    if (figletFont.Signature == "flf2a")
    {
      figletFont.HardBlank = ((IEnumerable<string>) strArray).First<string>().Last<char>().ToString();
      figletFont.Height = FrequencyBasedColorAlternator.smethod_0(strArray, 1);
      figletFont.BaseLine = FrequencyBasedColorAlternator.smethod_0(strArray, 2);
      figletFont.MaxLength = FrequencyBasedColorAlternator.smethod_0(strArray, 3);
      figletFont.OldLayout = FrequencyBasedColorAlternator.smethod_0(strArray, 4);
      figletFont.CommentLines = FrequencyBasedColorAlternator.smethod_0(strArray, 5);
      figletFont.PrintDirection = FrequencyBasedColorAlternator.smethod_0(strArray, 6);
      figletFont.FullLayout = FrequencyBasedColorAlternator.smethod_0(strArray, 7);
      figletFont.CodeTagCount = FrequencyBasedColorAlternator.smethod_0(strArray, 8);
    }
    return figletFont;
  }

  public object Target
  {
    [SpecialName] get => ((Formatter) this).styleClass_0.Target;
  }

  public Color Color
  {
    [SpecialName] get => ((Formatter) this).styleClass_0.Color;
  }
}
