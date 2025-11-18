// Decompiled with JetBrains decompiler
// Type: Plugin.Core.SharpDX.Vector3Union
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.SharpDX;

[StructLayout(LayoutKind.Explicit)]
public struct Vector3Union
{
  [FieldOffset(0)]
  public Vector3 Vec3;
  [FieldOffset(0)]
  public RawVector3 RawVec3;
}
