// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.StatisticClan
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class StatisticClan
{
  [CompilerGenerated]
  [SpecialName]
  public int get_AverageDamage() => ((StatisticBattlecup) this).int_9;

  [CompilerGenerated]
  [SpecialName]
  public void set_AverageDamage(int value) => ((StatisticBattlecup) this).int_9 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_PlayTime() => ((StatisticBattlecup) this).int_10;

  [CompilerGenerated]
  [SpecialName]
  public void set_PlayTime(int value) => ((StatisticBattlecup) this).int_10 = value;

  public long OwnerId { get; set; }

  public int Matches { get; set; }

  public int MatchWins
  {
    [CompilerGenerated, SpecialName] get => ((StatisticClan) this).int_1;
    [CompilerGenerated, SpecialName] set => ((StatisticClan) this).int_1 = value;
  }

  public int MatchLoses
  {
    [CompilerGenerated, SpecialName] get => ((StatisticClan) this).int_2;
    [CompilerGenerated, SpecialName] set => ((StatisticClan) this).int_2 = value;
  }
}
