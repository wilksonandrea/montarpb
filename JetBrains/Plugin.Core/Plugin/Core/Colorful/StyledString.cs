// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.StyledString
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class StyledString : IEquatable<StyledString>
{
  public StyledString()
  {
  }

  public StyledString([In] T0 obj0, params Color color_1)
  {
    ((StyleClass<T0>) this).Target = obj0;
    ((StyleClass<T0>) this).Color = color_1;
  }

  public bool Equals(StyleClass<T0> input)
  {
    return input != null && ((StyleClass<T0>) this).Target.Equals((object) input.Target) && ((StyleClass<T0>) this).Color == input.Color;
  }

  public override bool Equals(object value)
  {
    return ((StyleClass<T0>) this).Equals(value as StyleClass<T0>);
  }

  public override int GetHashCode()
  {
    return 163 * (79 + ((StyleClass<T0>) this).Target.GetHashCode()) * (79 + ((StyleClass<T0>) this).Color.GetHashCode());
  }

  public string AbstractValue { get; private set; }

  public string ConcreteValue { get; private set; }

  public Color[,] ColorGeometry { get; [param: In] set; }

  public char[,] CharacterGeometry { get; set; }

  public int[,] CharacterIndexGeometry { get; set; }

  public StyledString(string value) => this.AbstractValue = value;
}
