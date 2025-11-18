// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.REComparer
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Utility;

public class REComparer : EqualityComparer<object>
{
  static REComparer() => ObjectCopier.Class6.\u003C\u003E9 = (ObjectCopier.Class6) new REComparer();

  internal bool method_0([In] FieldInfo obj0) => obj0.IsPrivate;
}
