// Decompiled with JetBrains decompiler
// Type: dummy_ptr.{992044d9-fc6a-4ce3-a566-8a12e0526213}
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Colorful;
using System.Linq;

#nullable disable
namespace dummy_ptr;

internal abstract class \u007B992044d9\u002Dfc6a\u002D4ce3\u002Da566\u002D8a12e0526213\u007D
{
  static \u007B992044d9\u002Dfc6a\u002D4ce3\u002Da566\u002D8a12e0526213\u007D()
  {
    // ISSUE: reference to a compiler-generated field
    TextPatternCollection.Class35.\u003C\u003E9 = (TextPatternCollection.Class35) new \u007B992044d9\u002Dfc6a\u002D4ce3\u002Da566\u002D8a12e0526213\u007D();
  }

  public \u007B992044d9\u002Dfc6a\u002D4ce3\u002Da566\u002D8a12e0526213\u007D()
  {
  }

  internal string method_0(Pattern<string> input) => input.Value;

  public \u007B992044d9\u002Dfc6a\u002D4ce3\u002Da566\u002D8a12e0526213\u007D()
  {
  }

  internal bool method_0(Pattern<string> int_2)
  {
    // ISSUE: reference to a compiler-generated field
    return int_2.GetMatchLocations(((TextPatternCollection.Class36) this).string_0).Count<MatchLocation>() > 0;
  }

  public abstract void mp000001();

  public abstract void mp000002();

  public abstract void mp000003();

  public abstract void mp000004();

  public abstract void mp000005();
}
