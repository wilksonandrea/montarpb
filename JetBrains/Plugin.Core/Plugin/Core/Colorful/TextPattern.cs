// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.TextPattern
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class TextPattern : Pattern<string>
{
  private Regex regex_0;

  internal MatchLocation method_0(
    KeyValuePair<StyleClass<TextPattern>, MatchLocation> styleSheet_1)
  {
    return styleSheet_1.Value;
  }

  public TextPattern(Color input)
  {
    ((TextFormatter) this).string_0 = "{[0-9][^}]*}";
    // ISSUE: explicit constructor call
    base.\u002Ector();
    ((TextFormatter) this).color_0 = input;
    // ISSUE: object of a compiler-generated type is created
    ((TextFormatter) this).textPattern_0 = (TextPattern) new TextPattern.Class33(((TextFormatter) this).string_0);
  }

  public TextPattern(Color string_0, [In] string obj1)
  {
    ((TextFormatter) this).string_0 = "{[0-9][^}]*}";
    // ISSUE: explicit constructor call
    base.\u002Ector();
    ((TextFormatter) this).color_0 = string_0;
    // ISSUE: object of a compiler-generated type is created
    ((TextFormatter) this).textPattern_0 = (TextPattern) new TextPattern.Class33(((TextFormatter) this).string_0);
  }
}
