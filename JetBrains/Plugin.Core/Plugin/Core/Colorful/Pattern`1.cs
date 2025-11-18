// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.Pattern`1
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public abstract class Pattern<T> : IEquatable<Pattern<T>>
{
  public bool Equals(MatchLocation int_2)
  {
    return int_2 != null && ((MatchLocation) this).Beginning == int_2.Beginning && ((MatchLocation) this).End == int_2.End;
  }

  public override bool Equals(object value)
  {
    // ISSUE: explicit non-virtual call
    return __nonvirtual (((Pattern<>) this).Equals(value as MatchLocation));
  }

  public override int GetHashCode()
  {
    return 163 * (79 + ((MatchLocation) this).Beginning.GetHashCode()) * (79 + ((MatchLocation) this).End.GetHashCode());
  }

  public int CompareTo(MatchLocation value)
  {
    return ((MatchLocation) this).Beginning.CompareTo(value.Beginning);
  }

  public override string ToString()
  {
    int num = ((MatchLocation) this).Beginning;
    string str1 = num.ToString();
    num = ((MatchLocation) this).End;
    string str2 = num.ToString();
    return $"{str1}, {str2}";
  }

  public T Value { get; private set; }

  public Pattern([In] T obj0) => this.Value = obj0;
}
