// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.GradientGenerator
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class GradientGenerator
{
  public GradientGenerator(int fontLines, [In] Color[] obj1)
    : this(obj1)
  {
    ((FrequencyBasedColorAlternator) this).int_0 = fontLines;
  }

  public FrequencyBasedColorAlternator Prototype()
  {
    // ISSUE: reference to a compiler-generated method
    return (FrequencyBasedColorAlternator) new GradientGenerator(((FrequencyBasedColorAlternator) this).int_0, ((IEnumerable<Color>) ((ColorAlternator) this).Colors).smethod_1<Color>().ToArray<Color>());
  }

  protected virtual ColorAlternator PrototypeCore() => (ColorAlternator) this.Prototype();
}
