// Decompiled with JetBrains decompiler
// Type: Plugin.Core.SharpDX.Half3
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.SharpDX;

public struct Half3 : IEquatable<Half3>
{
  public Half X;
  public Half Y;
  public Half Z;

  public override int GetHashCode()
  {
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ushort ushort0 = (^(Half&) ref this).ushort_0;
    return (int) ushort0 * 3 / 2 ^ (int) ushort0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Equals(ref Half Value, [In] ref Half obj1)
  {
    return (int) Value.ushort_0 == (int) obj1.ushort_0;
  }

  public bool Equals([In] Half obj0) => (int) obj0.ushort_0 == (int) (^(Half&) ref this).ushort_0;

  public override bool Equals(object left) => left is Half half && this.Equals(half);

  static Half3()
  {
    Half.Epsilon = 0.0004887581f;
    Half.MaxValue = 65504f;
    Half.MinValue = 6.103516E-05f;
  }

  public Half3([In] float obj0, float right, [In] float obj2)
  {
    this.X = new Half(obj0);
    this.Y = new Half(right);
    this.Z = new Half(obj2);
  }

  public Half3(ushort other, [In] ushort obj1, [In] ushort obj2)
  {
    this.X = new Half(other);
    this.Y = new Half(obj1);
    this.Z = new Half(obj2);
  }

  public static implicit operator Half3([In] Vector3 obj0) => new Half3(obj0.X, obj0.Y, obj0.Z);

  public static implicit operator Vector3([In] Half3 obj0)
  {
    return new Vector3((float) obj0.X, (float) obj0.Y, (float) obj0.Z);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(Half3 ushort_0, Half3 ushort_1)
  {
    return Class9.Equals(ref ushort_0, ref ushort_1);
  }
}
