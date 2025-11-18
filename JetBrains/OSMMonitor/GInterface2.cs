// Decompiled with JetBrains decompiler
// Type: GInterface2
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
[CompilerGenerated]
[Guid("9C4C6277-5027-441E-AFAE-CA1F542DA009")]
[TypeIdentifier]
[ComImport]
public interface GInterface2 : IEnumerable
{
  [SpecialName]
  [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
  sealed extern void _VtblGap1_1();

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void imethod_0([MarshalAs(UnmanagedType.Interface), In] GInterface1 ginterface1_0);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void imethod_1([MarshalAs(UnmanagedType.BStr), In] string string_0);
}
