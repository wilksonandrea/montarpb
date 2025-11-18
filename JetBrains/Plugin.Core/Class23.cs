// Decompiled with JetBrains decompiler
// Type: Class23
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Colorful;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

#nullable disable
internal static class Class23
{
  public ConsoleColor Replace(Color value, [In] Color obj1)
  {
    ConsoleColor key;
    if (!((ColorStore) this).Colors.TryRemove(value, out key))
      throw new ArgumentException("An attempt was made to replace a nonexistent color in the ColorStore!");
    ((ColorStore) this).Colors.TryAdd(obj1, key);
    ((ColorStore) this).ConsoleColors[key] = obj1;
    return key;
  }

  public bool RequiresUpdate([In] Color obj0) => !((ColorStore) this).Colors.ContainsKey(obj0);

  public Class23()
    : this(string.Format("Color conversion failed because a handle to the actual windows console was not found."))
  {
  }

  public Class23()
  {
  }
}
