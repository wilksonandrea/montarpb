// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerStatistic
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerStatistic
{
  [CompilerGenerated]
  [SpecialName]
  public long get_PlayerId() => ((PlayerTopup) this).long_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_PlayerId(long Type) => ((PlayerTopup) this).long_1 = Type;

  [CompilerGenerated]
  [SpecialName]
  public int get_GoodsId() => ((PlayerTopup) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_GoodsId(int Item) => ((PlayerTopup) this).int_0 = Item;

  public StatisticTotal Basic { get; set; }

  public StatisticSeason Season { get; set; }

  public StatisticDaily Daily { get; set; }

  public StatisticClan Clan { get; set; }

  public StatisticWeapon Weapon { get; set; }

  public StatisticAcemode Acemode { get; set; }

  public StatisticBattlecup Battlecup { get; set; }

  public int GetKDRatio()
  {
    return this.Basic.HeadshotsCount <= 0 && this.Basic.KillsCount <= 0 ? 0 : (int) Math.Floor(((double) (this.Basic.KillsCount * 100) + 0.5) / (double) (this.Basic.KillsCount + this.Basic.DeathsCount));
  }

  public int GetHSRatio()
  {
    return this.Basic.KillsCount <= 0 ? 0 : (int) Math.Floor((double) (this.Basic.HeadshotsCount * 100) / ((double) this.Basic.KillsCount + 0.5));
  }
}
