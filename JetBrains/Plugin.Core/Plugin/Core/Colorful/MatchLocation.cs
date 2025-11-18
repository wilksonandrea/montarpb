// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.MatchLocation
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public class MatchLocation : 
  IEquatable<MatchLocation>,
  IComparable<MatchLocation>,
  IPrototypable<MatchLocation>
{
  internal bool method_2([In] int obj0) => obj0 == 0;

  public MatchLocation()
  {
  }

  internal bool method_0([In] int obj0, [In] int obj1, T0 color_1, T0 int_1)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return obj1 > ((GradientGenerator.Class29<T0>) this).int_0 - 1 && !color_1.Equals((object) int_1) || ((GradientGenerator.Class29<T0>) this).func_0(obj0);
  }

  internal bool method_1(int int_0) => int_0 < ((GradientGenerator.Class29<T0>) this).int_1;

  public abstract T0 Prototype();

  public int Beginning { get; private set; }

  public int End { get; [param: In] private set; }

  public MatchLocation([In] int obj0, [In] int obj1)
  {
    this.Beginning = obj0;
    this.End = obj1;
  }

  public MatchLocation Prototype() => new MatchLocation(this.Beginning, this.End);
}
