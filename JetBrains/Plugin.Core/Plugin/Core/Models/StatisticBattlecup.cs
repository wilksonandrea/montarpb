// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.StatisticBattlecup
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class StatisticBattlecup
{
  [CompilerGenerated]
  [SpecialName]
  public int get_Escapes() => ((StatisticAcemode) this).int_7;

  [CompilerGenerated]
  [SpecialName]
  public void set_Escapes(int value) => ((StatisticAcemode) this).int_7 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Winstreaks() => ((StatisticAcemode) this).int_8;

  [CompilerGenerated]
  [SpecialName]
  public void set_Winstreaks(int value) => ((StatisticAcemode) this).int_8 = value;

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

  public int AverageDamage
  {
    [CompilerGenerated, SpecialName] get => ((StatisticBattlecup) this).int_9;
    [CompilerGenerated, SpecialName] set => ((StatisticBattlecup) this).int_9 = value;
  }

  public int PlayTime
  {
    [CompilerGenerated, SpecialName] get => ((StatisticBattlecup) this).int_10;
    [CompilerGenerated, SpecialName] set => ((StatisticBattlecup) this).int_10 = value;
  }
}
