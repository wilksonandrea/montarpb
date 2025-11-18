// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.RankModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Plugin.Core.Models;

public class RankModel
{
  public int Id;
  public string Title;
  public int OnNextLevel;
  public int OnGoldUp;
  public int OnAllExp;
  public SortedList<int, List<int>> Rewards;

  public int GetSeasonKDRatio()
  {
    return ((PlayerStatistic) this).Season.HeadshotsCount <= 0 && ((PlayerStatistic) this).Season.KillsCount <= 0 ? 0 : (int) Math.Floor(((double) (((PlayerStatistic) this).Season.KillsCount * 100) + 0.5) / (double) (((PlayerStatistic) this).Season.KillsCount + ((PlayerStatistic) this).Season.DeathsCount));
  }
}
