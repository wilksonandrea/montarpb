// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.SafeList`1
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Plugin.Core.Utility;

public class SafeList<T>
{
  private readonly List<T> list_0;
  private readonly object object_0;

  public virtual int GetHashCode(object fieldInfo_0)
  {
    return fieldInfo_0 == null ? 0 : fieldInfo_0.GetHashCode();
  }

  public SafeList()
    : this()
  {
  }

  public static void ForEach(this Array array_1, Action<Array, int[]> int_0)
  {
    if (array_1.LongLength == 0L)
      return;
    Class8 class8 = (Class8) new SafeList<>(array_1);
    do
    {
      int_0(array_1, class8.int_0);
    }
    while (((SafeList<>) class8).method_0());
  }

  public SafeList(Array X)
  {
    ((Class8) this).int_1 = new int[X.Rank];
    for (int dimension = 0; dimension < X.Rank; ++dimension)
      ((Class8) this).int_1[dimension] = X.GetLength(dimension) - 1;
    ((Class8) this).int_0 = new int[X.Rank];
  }

  public bool method_0()
  {
    for (int index1 = 0; index1 < ((Class8) this).int_0.Length; ++index1)
    {
      if (((Class8) this).int_0[index1] < ((Class8) this).int_1[index1])
      {
        ++((Class8) this).int_0[index1];
        for (int index2 = 0; index2 < index1; ++index2)
          ((Class8) this).int_0[index2] = 0;
        return true;
      }
    }
    return false;
  }

  public SafeList()
  {
  }
}
