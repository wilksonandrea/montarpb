// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.ShopData
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class ShopData
{
  [CompilerGenerated]
  [SpecialName]
  public int get_SessionId() => ((PlayerSession) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_SessionId(int value) => ((PlayerSession) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public long get_PlayerId() => ((PlayerSession) this).long_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_PlayerId(long value) => ((PlayerSession) this).long_0 = value;

  public byte[] Buffer { get; set; }

  public int ItemsCount
  {
    [CompilerGenerated, SpecialName] get => ((ShopData) this).int_0;
    [CompilerGenerated, SpecialName] set => ((ShopData) this).int_0 = value;
  }

  public int Offset
  {
    [CompilerGenerated, SpecialName] get => ((ShopData) this).int_1;
    [CompilerGenerated, SpecialName] set => ((ShopData) this).int_1 = value;
  }
}
