// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.StatisticWeapon
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class StatisticWeapon
{
  [CompilerGenerated]
  [SpecialName]
  public int get_TotalKillsCount() => ((StatisticTotal) this).int_10;

  [CompilerGenerated]
  [SpecialName]
  public void set_TotalKillsCount(int value) => ((StatisticTotal) this).int_10 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_TotalMatchesCount() => ((StatisticTotal) this).int_11;

  [CompilerGenerated]
  [SpecialName]
  public void set_TotalMatchesCount(int value) => ((StatisticTotal) this).int_11 = value;

  public long OwnerId { get; set; }

  public int AssaultKills { get; set; }

  public int AssaultDeaths { get; set; }

  public int SmgKills { get; set; }

  public int SmgDeaths { get; set; }

  public int SniperKills { get; set; }

  public int SniperDeaths { get; set; }

  public int MachinegunKills { get; set; }

  public int MachinegunDeaths { get; set; }

  public int ShotgunKills { get; set; }

  public int ShotgunDeaths { get; set; }

  public int ShieldKills
  {
    [CompilerGenerated, SpecialName] get => ((StatisticWeapon) this).int_10;
    [CompilerGenerated, SpecialName] set => ((StatisticWeapon) this).int_10 = value;
  }

  public int ShieldDeaths
  {
    [CompilerGenerated, SpecialName] get => ((StatisticWeapon) this).int_11;
    [CompilerGenerated, SpecialName] set => ((StatisticWeapon) this).int_11 = value;
  }
}
