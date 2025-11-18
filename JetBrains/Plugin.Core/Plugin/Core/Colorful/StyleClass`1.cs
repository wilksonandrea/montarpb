// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.StyleClass`1
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Plugin.Core.Colorful;

public class StyleClass<T> : IEquatable<StyleClass<T>>
{
  protected virtual void TryIncrementColorIndex()
  {
    if (((ColorAlternator) this).nextColorIndex >= ((ColorAlternator) this).Colors.Length - 1)
      ((ColorAlternator) this).nextColorIndex = 0;
    else
      ((ColorAlternator) this).nextColorIndex = ((ColorAlternator) this).nextColorIndex + 1;
  }

  public StyleClass()
  {
    ((PatternCollection<T>) this).patterns = new List<Pattern<T>>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public PatternCollection<T> Prototype() => ((PatternCollection<T>) this).PrototypeCore();

  protected abstract PatternCollection<T> PrototypeCore();

  public abstract bool MatchFound(string other);

  public T Target { get; protected set; }

  public Color Color { get; protected set; }
}
