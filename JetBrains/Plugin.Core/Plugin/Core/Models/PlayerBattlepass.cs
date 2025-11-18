// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerBattlepass
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerBattlepass
{
  [CompilerGenerated]
  [SpecialName]
  public int get_ExpUp() => ((PCCafeModel) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_ExpUp(int value) => ((PCCafeModel) this).int_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public SortedList<CafeEnum, List<ItemsModel>> get_Rewards() => ((PCCafeModel) this).sortedList_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Rewards(SortedList<CafeEnum, List<ItemsModel>> value)
  {
    // ISSUE: reference to a compiler-generated field
    ((PCCafeModel) this).sortedList_0 = value;
  }

  public PlayerBattlepass(CafeEnum GoodId)
  {
    ((PCCafeModel) this).Type = GoodId;
    ((PCCafeModel) this).PointUp = 0;
    this.set_ExpUp(0);
  }

  public int Id { get; set; }

  public int Level { get; set; }

  public bool IsPremium { get; set; }

  public int TotalPoints { get; set; }

  public int DailyPoints
  {
    [CompilerGenerated, SpecialName] get => ((PlayerBattlepass) this).int_3;
    [CompilerGenerated, SpecialName] set => ((PlayerBattlepass) this).int_3 = value;
  }

  public uint LastRecord
  {
    [CompilerGenerated, SpecialName] get => ((PlayerBattlepass) this).uint_0;
    [CompilerGenerated, SpecialName] set => ((PlayerBattlepass) this).uint_0 = value;
  }
}
