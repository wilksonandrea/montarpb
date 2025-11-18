// Decompiled with JetBrains decompiler
// Type: Class9
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.SharpDX;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
internal class Class9
{
  private static readonly uint[] uint_0;
  private static readonly uint[] uint_1;
  private static readonly uint[] uint_2;
  private static readonly ushort[] ushort_0;
  private static readonly byte[] byte_0;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=([In] Half3 obj0, [In] Half3 obj1)
  {
    return !Class9.Equals(ref obj0, ref obj1);
  }

  public override int GetHashCode()
  {
    return (((Half3) this).X.GetHashCode() * 397 ^ ((Half3) this).Y.GetHashCode()) * 397 ^ ((Half3) this).Z.GetHashCode();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Equals(ref Half3 left, ref Half3 right)
  {
    return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool Equals([In] Half3 obj0)
  {
    return ((Half3) this).X == obj0.X && ((Half3) this).Y == obj0.Y && ((Half3) this).Z == obj0.Z;
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct Struct0
  {
    [FieldOffset(0)]
    public uint uint_0;
    [FieldOffset(0)]
    public float float_0;
  }
}
