// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.TextPatternCollection
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class TextPatternCollection : PatternCollection<string>
{
  [DebuggerHidden]
  [SpecialName]
  private string System\u002ECollections\u002EGeneric\u002EIEnumerator\u003CSystem\u002EString\u003E\u002Eget_Current()
  {
    // ISSUE: reference to a compiler-generated field
    return ((TextPattern.Class34) this).string_0;
  }

  [DebuggerHidden]
  private void System\u002ECollections\u002EIEnumerator\u002EReset()
  {
    throw new NotSupportedException();
  }

  [DebuggerHidden]
  [SpecialName]
  private object System\u002ECollections\u002EIEnumerator\u002Eget_Current()
  {
    // ISSUE: reference to a compiler-generated field
    return (object) ((TextPattern.Class34) this).string_0;
  }

  [DebuggerHidden]
  private IEnumerator<string> System\u002ECollections\u002EGeneric\u002EIEnumerable\u003CSystem\u002EString\u003E\u002EGetEnumerator()
  {
    // ISSUE: variable of a compiler-generated type
    TextPattern.Class34 enumerator;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (((TextPattern.Class34) this).int_0 == -2 && ((TextPattern.Class34) this).int_1 == Environment.CurrentManagedThreadId)
    {
      // ISSUE: reference to a compiler-generated field
      ((TextPattern.Class34) this).int_0 = 0;
      enumerator = (TextPattern.Class34) this;
    }
    else
    {
      // ISSUE: object of a compiler-generated type is created
      enumerator = new TextPattern.Class34(0);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      enumerator.textPattern_0 = ((TextPattern.Class34) this).textPattern_0;
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    enumerator.string_1 = ((TextPattern.Class34) this).string_2;
    return (IEnumerator<string>) enumerator;
  }
}
