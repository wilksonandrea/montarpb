// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.PatternBasedColorAlternator`1
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class PatternBasedColorAlternator<T> : 
  ColorAlternator,
  IPrototypable<PatternBasedColorAlternator<T>>
{
  private PatternCollection<T> patternCollection_0;
  private bool bool_0;

  public abstract IEnumerable<MatchLocation> GetMatchLocations(T other);

  public abstract IEnumerable<T> GetMatches(T obj);

  public bool Equals(Pattern<T> other)
  {
    return other != null && ((Pattern<T>) this).Value.Equals((object) other.Value);
  }

  public override bool Equals(object value) => ((Pattern<T>) this).Equals(value as Pattern<T>);

  public override int GetHashCode() => 163 * (79 + ((Pattern<T>) this).Value.GetHashCode());
}
