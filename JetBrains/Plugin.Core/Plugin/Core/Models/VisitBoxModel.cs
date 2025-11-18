// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.VisitBoxModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class VisitBoxModel
{
  public int GetSeasonHSRatio()
  {
    return ((PlayerStatistic) this).Season.KillsCount <= 0 ? 0 : (int) Math.Floor((double) (((PlayerStatistic) this).Season.HeadshotsCount * 100) / ((double) ((PlayerStatistic) this).Season.KillsCount + 0.5));
  }

  public int GetBCWinRatio()
  {
    return ((PlayerStatistic) this).Battlecup.MatchWins <= 0 && ((PlayerStatistic) this).Battlecup.Matches <= 0 ? 0 : (int) Math.Floor(((double) (((PlayerStatistic) this).Battlecup.MatchWins * 100) + 0.5) / (double) (((PlayerStatistic) this).Battlecup.MatchWins + ((PlayerStatistic) this).Battlecup.MatchLoses));
  }

  public int GetBCKDRatio()
  {
    return ((PlayerStatistic) this).Battlecup.HeadshotsCount <= 0 && ((PlayerStatistic) this).Battlecup.KillsCount <= 0 ? 0 : (int) Math.Floor(((double) (((PlayerStatistic) this).Battlecup.KillsCount * 100) + 0.5) / (double) (((PlayerStatistic) this).Battlecup.KillsCount + ((PlayerStatistic) this).Battlecup.DeathsCount));
  }

  public VisitBoxModel()
  {
  }

  public VisitBoxModel(int value) => ((RankModel) this).Id = value;

  public VisitItemModel Reward1 { get; set; }

  public VisitItemModel Reward2 { get; set; }

  public int RewardCount
  {
    get => this.int_0;
    [CompilerGenerated, SpecialName] set => ((VisitBoxModel) this).int_0 = value;
  }

  public bool IsBothReward
  {
    [CompilerGenerated, SpecialName] get => ((VisitBoxModel) this).bool_0;
    [CompilerGenerated, SpecialName] set => ((VisitBoxModel) this).bool_0 = value;
  }
}
