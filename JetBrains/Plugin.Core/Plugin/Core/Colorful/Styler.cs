// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.Styler
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class Styler : StyleClass<TextPattern>, IEquatable<Styler>
{
  public Styler(string value, [In] string obj1)
  {
    ((StyledString) this).AbstractValue = value;
    ((StyledString) this).ConcreteValue = obj1;
  }

  public bool Equals(StyledString value)
  {
    return value != null && ((StyledString) this).AbstractValue == value.AbstractValue && ((StyledString) this).ConcreteValue == value.ConcreteValue;
  }

  public override bool Equals(object value) => this.Equals(value as StyledString);

  public override int GetHashCode()
  {
    return 163 * (79 + ((StyledString) this).AbstractValue.GetHashCode()) * (79 + ((StyledString) this).ConcreteValue.GetHashCode());
  }

  public override string ToString() => ((StyledString) this).ConcreteValue;

  public Styler.MatchFound MatchFoundHandler
  {
    get => this.matchFound_0;
    [CompilerGenerated, SpecialName] private set => ((Styler) this).matchFound_0 = value;
  }

  public delegate void MatchFound();

  public delegate string MatchFoundLite([In] string obj0, [In] MatchLocation obj1, string matchFound_1);
}
