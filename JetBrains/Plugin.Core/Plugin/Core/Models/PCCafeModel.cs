// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PCCafeModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PCCafeModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_GoodId(int value) => ((PassItemModel) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public bool get_IsReward() => ((PassItemModel) this).bool_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_IsReward(bool value) => ((PassItemModel) this).bool_0 = value;

  public void SetGoodId(int value)
  {
    this.set_GoodId(value);
    if (value <= 0)
      return;
    this.set_IsReward(true);
  }

  public CafeEnum Type { get; set; }

  public int PointUp { get; set; }

  public int ExpUp
  {
    [CompilerGenerated, SpecialName] get => ((PCCafeModel) this).int_1;
    [CompilerGenerated, SpecialName] set => ((PCCafeModel) this).int_1 = value;
  }

  public SortedList<CafeEnum, List<ItemsModel>> Rewards
  {
    [CompilerGenerated, SpecialName] get => ((PCCafeModel) this).sortedList_0;
    [CompilerGenerated, SpecialName] set => ((PCCafeModel) this).sortedList_0 = value;
  }
}
