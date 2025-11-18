// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.StatisticTotal
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class StatisticTotal
{
  [CompilerGenerated]
  [SpecialName]
  public int get_TotalKillsCount() => ((StatisticSeason) this).int_10;

  [CompilerGenerated]
  [SpecialName]
  public void set_TotalKillsCount(int value) => ((StatisticSeason) this).int_10 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_TotalMatchesCount() => ((StatisticSeason) this).int_11;

  [CompilerGenerated]
  [SpecialName]
  public void set_TotalMatchesCount(int value) => ((StatisticSeason) this).int_11 = value;

  public long OwnerId { get; set; }

  public int Matches { get; set; }

  public int MatchWins { get; set; }

  public int MatchLoses { get; set; }

  public int MatchDraws { get; set; }

  public int KillsCount { get; set; }

  public int DeathsCount { get; set; }

  public int HeadshotsCount { get; set; }

  public int AssistsCount { get; set; }

  public int EscapesCount { get; set; }

  public int MvpCount { get; set; }

  public int TotalKillsCount
  {
    [CompilerGenerated, SpecialName] get => ((StatisticTotal) this).int_10;
    [CompilerGenerated, SpecialName] set => ((StatisticTotal) this).int_10 = value;
  }

  public int TotalMatchesCount
  {
    [CompilerGenerated, SpecialName] get => ((StatisticTotal) this).int_11;
    [CompilerGenerated, SpecialName] set => ((StatisticTotal) this).int_11 = value;
  }
}
