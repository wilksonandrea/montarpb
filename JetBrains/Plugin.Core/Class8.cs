// Decompiled with JetBrains decompiler
// Type: Class8
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Utility;
using System;
using System.Runtime.InteropServices;

#nullable disable
internal class Class8
{
  public int[] int_0;
  private readonly int[] int_1;

  internal void method_0(Array original, [In] int[] obj1)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    original.SetValue(ObjectCopier.smethod_0(((ObjectCopier.Class7) this).array_0.GetValue(obj1), ((ObjectCopier.Class7) this).idictionary_0), obj1);
  }

  public virtual bool Equals(object object_0, [In] object obj1) => object_0 == obj1;
}
