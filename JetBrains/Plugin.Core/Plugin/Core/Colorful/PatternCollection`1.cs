// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.PatternCollection`1
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public abstract class PatternCollection<T> : IPrototypable<PatternCollection<T>>
{
  protected List<Pattern<T>> patterns;

  public PatternCollection(PatternCollection<T> gparam_1, [In] Color[] obj1)
  {
    ((PatternBasedColorAlternator<T>) this).bool_0 = true;
    // ISSUE: explicit constructor call
    ((ColorAlternatorFactory) this).\u002Ector(obj1);
    ((PatternBasedColorAlternator<T>) this).patternCollection_0 = gparam_1;
  }

  public PatternBasedColorAlternator<T> Prototype()
  {
    // ISSUE: reference to a compiler-generated method
    return new PatternBasedColorAlternator<T>(((PatternBasedColorAlternator<T>) this).patternCollection_0.Prototype(), ((IEnumerable<Color>) ((ColorAlternator) this).Colors).smethod_1<Color>().ToArray<Color>());
  }

  protected virtual ColorAlternator PrototypeCore()
  {
    return (ColorAlternator) ((PatternBasedColorAlternator<T>) this).Prototype();
  }

  public virtual Color GetNextColor(string input)
  {
    if (((ColorAlternator) this).Colors.Length == 0)
      throw new InvalidOperationException("No colors have been supplied over which to alternate!");
    if (((PatternBasedColorAlternator<T>) this).bool_0)
    {
      ((PatternBasedColorAlternator<T>) this).bool_0 = false;
      return ((ColorAlternator) this).Colors[((ColorAlternator) this).nextColorIndex];
    }
    if (((PatternBasedColorAlternator<T>) this).patternCollection_0.MatchFound(input))
      ((Console) this).TryIncrementColorIndex();
    return ((ColorAlternator) this).Colors[((ColorAlternator) this).nextColorIndex];
  }
}
