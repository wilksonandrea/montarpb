// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.Figlet
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Colorful;

public class Figlet
{
  private readonly FigletFont figletFont_0;

  [DebuggerHidden]
  [SpecialName]
  private T0 System\u002ECollections\u002EGeneric\u002EIEnumerator\u003CT\u003E\u002Eget_Current()
  {
    // ISSUE: reference to a compiler-generated field
    return ((Class23.Class27<T0>) this).gparam_0;
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
    return (object) ((Class23.Class27<T0>) this).gparam_0;
  }

  [DebuggerHidden]
  private IEnumerator<T0> System\u002ECollections\u002EGeneric\u002EIEnumerable\u003CT\u003E\u002EGetEnumerator()
  {
    // ISSUE: variable of a compiler-generated type
    Class23.Class27<T0> enumerator;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (((Class23.Class27<T0>) this).int_0 == -2 && ((Class23.Class27<T0>) this).int_1 == Environment.CurrentManagedThreadId)
    {
      // ISSUE: reference to a compiler-generated field
      ((Class23.Class27<T0>) this).int_0 = 0;
      enumerator = (Class23.Class27<T0>) this;
    }
    else
    {
      // ISSUE: object of a compiler-generated type is created
      enumerator = new Class23.Class27<T0>(0);
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    enumerator.ienumerable_0 = ((Class23.Class27<T0>) this).ienumerable_1;
    return (IEnumerator<T0>) enumerator;
  }

  [DebuggerHidden]
  private IEnumerator System\u002ECollections\u002EIEnumerable\u002EGetEnumerator()
  {
    // ISSUE: reference to a compiler-generated method
    return (IEnumerator) ((Class23.Class27<T0>) this).System\u002ECollections\u002EGeneric\u002EIEnumerable\u003CT\u003E\u002EGetEnumerator();
  }

  public Figlet() => this.figletFont_0 = FigletFont.Default;
}
