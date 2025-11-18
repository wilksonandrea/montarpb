// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.StatisticAcemode
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class StatisticAcemode
{
  [CompilerGenerated]
  [SpecialName]
  public long get_PlayerId() => ((SlotMatch) this).long_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_PlayerId(long value) => ((SlotMatch) this).long_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public SlotMatchState get_State() => ((SlotMatch) this).slotMatchState_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_State(SlotMatchState value) => ((SlotMatch) this).slotMatchState_0 = value;

  public StatisticAcemode(int slotModel_2) => ((SlotMatch) this).Id = slotModel_2;

  public long OwnerId { get; [param: In] set; }

  public int Matches { get; set; }

  public int MatchWins { get; set; }

  public int MatchLoses { get; set; }

  public int Kills { get; set; }

  public int Deaths { get; set; }

  public int Headshots { get; set; }

  public int Assists { get; set; }

  public int Escapes
  {
    [CompilerGenerated, SpecialName] get => ((StatisticAcemode) this).int_7;
    [CompilerGenerated, SpecialName] set => ((StatisticAcemode) this).int_7 = value;
  }

  public int Winstreaks
  {
    [CompilerGenerated, SpecialName] get => ((StatisticAcemode) this).int_8;
    [CompilerGenerated, SpecialName] set => ((StatisticAcemode) this).int_8 = value;
  }
}
