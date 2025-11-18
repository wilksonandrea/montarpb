// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.ColorAlternatorFactory
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Drawing;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class ColorAlternatorFactory
{
  public ColorAlternatorFactory(Color[] R) => ((ColorAlternator) this).Colors = R;

  public ColorAlternator Prototype() => this.PrototypeCore();

  protected abstract ColorAlternator PrototypeCore();
}
