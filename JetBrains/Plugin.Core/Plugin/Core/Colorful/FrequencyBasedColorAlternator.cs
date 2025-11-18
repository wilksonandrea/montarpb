// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.FrequencyBasedColorAlternator
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class FrequencyBasedColorAlternator : 
  ColorAlternator,
  IPrototypable<FrequencyBasedColorAlternator>
{
  private int int_0;
  private int int_1;

  private static int smethod_0(string[] bytes, [In] int obj1)
  {
    int result = 0;
    if (bytes.Length > obj1)
      int.TryParse(bytes[obj1], out result);
    return result;
  }

  public FrequencyBasedColorAlternator()
  {
  }

  [SpecialName]
  public object get_Target() => ((Formatter) this).styleClass_0.Target;

  [SpecialName]
  public Color get_Color() => ((Formatter) this).styleClass_0.Color;

  public FrequencyBasedColorAlternator(object filePath, [In] Color obj1)
  {
    ((Formatter) this).styleClass_0 = new StyleClass<object>(filePath, obj1);
  }
}
