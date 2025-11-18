// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.StyleSheet
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class StyleSheet
{
  public Color UnstyledColor;

  [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern string EndInvoke(IAsyncResult unstyledInput);

  [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
  public extern StyleSheet([In] object obj0, IntPtr matchLocation);

  [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern string Invoke([In] string obj0);

  [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern IAsyncResult BeginInvoke([In] string obj0, [In] AsyncCallback obj1, [In] object obj2);

  [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern string EndInvoke([In] IAsyncResult obj0);

  public List<StyleClass<TextPattern>> Styles
  {
    get => this.list_0;
    [CompilerGenerated, SpecialName] private set => ((StyleSheet) this).list_0 = value;
  }
}
