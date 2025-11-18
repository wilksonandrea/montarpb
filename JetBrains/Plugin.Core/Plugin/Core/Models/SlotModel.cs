// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.SlotModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class SlotModel
{
  public int Id;
  public int Flag;
  public int AiLevel;
  public int Latency;
  public int FailLatencyTimes;
  public int Ping;
  public int PassSequence;
  public int IsPlaying;
  public int EarnedEXP;
  public int SpawnsCount;
  public int LastKillState;
  public int KillsOnLife;
  public int Exp;
  public int Cash;
  public int Gold;
  public int Score;
  public int AllKills;
  public int AllHeadshots;
  public int AllDeaths;
  public int AllAssists;
  public int Objects;
  public int BonusItemExp;
  public int BonusItemPoint;
  public int BonusEventExp;
  public int BonusEventPoint;
  public int BonusCafePoint;
  public int BonusCafeExp;
  public int UnkItem;
  public int SeasonPoint;
  public int BonusBattlePass;
  public ushort DamageBar1;
  public ushort DamageBar2;
  public long PlayerId;
  public bool Check;
  public bool SpecGM;
  public bool WithHost;
  public bool Spectator;
  public bool RepeatLastState;
  public bool MissionsCompleted;
  public bool FirstInactivityOff;
  public bool FirstRespawn;
  public TeamEnum Team;
  public TeamEnum CostumeTeam;
  public SlotState State;
  public ResultIcon BonusFlags;
  public ViewerType ViewType;
  public DeadEnum DeathState;
  public DateTime NextVoteDate;
  public DateTime StartTime;
  public DateTime PreStartDate;
  public DateTime PreLoadDate;
  public PlayerEquipment Equipment;
  public PlayerMissions Missions;
  public int[] AR;
  public int[] SMG;
  public int[] SR;
  public int[] MG;
  public int[] SG;
  public int[] SHD;
  public List<int> ItemUsages;
  public TimerState Timing;

  [CompilerGenerated]
  [SpecialName]
  public void set_Z(float value) => ((FragInfos) this).float_2 = value;

  [CompilerGenerated]
  [SpecialName]
  public List<FragModel> get_Frags() => ((FragInfos) this).list_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Frags(List<FragModel> value) => ((FragInfos) this).list_0 = value;

  public SlotModel() => this.set_Frags(new List<FragModel>());

  public KillingMessage GetAllKillFlags()
  {
    KillingMessage allKillFlags = KillingMessage.None;
    foreach (FragModel fragModel in this.get_Frags())
    {
      if (!allKillFlags.HasFlag((Enum) fragModel.KillFlag))
        allKillFlags |= fragModel.KillFlag;
    }
    return allKillFlags;
  }

  public SlotModel(int value) => ((VoteKickModel) this).SetSlotId(value);
}
