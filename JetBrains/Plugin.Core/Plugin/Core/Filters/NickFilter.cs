// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Filters.NickFilter
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.SharpDX;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Filters;

public static class NickFilter
{
  public static List<string> Filters;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(Vector3 format)
  {
    // ISSUE: unable to decompile the method.
  }

  public override bool Equals(object formatProvider)
  {
    // ISSUE: unable to decompile the method.
  }

  public static implicit operator RawVector3(Vector3 format)
  {
    return new Vector3Union() { Vec3 = format }.RawVec3;
  }

  public static implicit operator Vector3([In] RawVector3 obj0)
  {
    return new Vector3Union() { RawVec3 = obj0 }.Vec3;
  }
}
