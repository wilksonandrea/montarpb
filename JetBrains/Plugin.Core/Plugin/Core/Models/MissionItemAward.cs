// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.MissionItemAward
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class MissionItemAward
{
  [CompilerGenerated]
  [SpecialName]
  public int get_ArrayIdx() => ((MissionCardModel) this).int_6;

  [CompilerGenerated]
  [SpecialName]
  public void set_ArrayIdx(int value) => ((MissionCardModel) this).int_6 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Flag() => ((MissionCardModel) this).int_7;

  [CompilerGenerated]
  [SpecialName]
  public void set_Flag(int value) => ((MissionCardModel) this).int_7 = value;

  public MissionItemAward(int value, [In] int obj1)
  {
    ((MissionCardModel) this).CardBasicId = value;
    ((MissionCardModel) this).MissionBasicId = obj1;
    this.set_ArrayIdx(value * 4 + obj1);
    this.set_Flag(15 << 4 * obj1);
  }

  public int MissionId
  {
    [CompilerGenerated, SpecialName] get => ((MissionItemAward) this).int_0;
    [CompilerGenerated, SpecialName] set => ((MissionItemAward) this).int_0 = value;
  }

  public ItemsModel Item
  {
    [CompilerGenerated, SpecialName] get => ((MissionItemAward) this).itemsModel_0;
    [CompilerGenerated, SpecialName] set => ((MissionItemAward) this).itemsModel_0 = value;
  }
}
