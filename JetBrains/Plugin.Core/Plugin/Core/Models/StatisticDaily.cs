// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.StatisticDaily
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class StatisticDaily
{
  [CompilerGenerated]
  [SpecialName]
  public int get_MatchWins() => ((StatisticClan) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_MatchWins(int value) => ((StatisticClan) this).int_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_MatchLoses() => ((StatisticClan) this).int_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_MatchLoses(int value) => ((StatisticClan) this).int_2 = value;

  public long OwnerId { get; set; }

  public int Matches { get; set; }

  public int MatchWins { get; set; }

  public int MatchLoses { get; set; }

  public int MatchDraws { get; set; }

  public int KillsCount { get; set; }

  public int DeathsCount { get; set; }

  public int HeadshotsCount { get; set; }

  public int ExpGained { get; set; }

  public int PointGained
  {
    [CompilerGenerated, SpecialName] get => ((StatisticDaily) this).int_8;
    [CompilerGenerated, SpecialName] set => ((StatisticDaily) this).int_8 = value;
  }

  public uint Playtime
  {
    [CompilerGenerated, SpecialName] get => ((StatisticDaily) this).uint_0;
    [CompilerGenerated, SpecialName] set => ((StatisticDaily) this).uint_0 = value;
  }
}
