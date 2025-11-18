// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.VoteKickModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class VoteKickModel
{
  public void SetSlotId(int value)
  {
    ((SlotModel) this).Id = value;
    ((SlotModel) this).Team = (TeamEnum) (value % 2);
    ((SlotModel) this).Flag = 1 << value;
  }

  public void StopTiming()
  {
    // ISSUE: unable to decompile the method.
  }

  public void ResetSlot()
  {
    this.StopTiming();
    ((SlotModel) this).DeathState = DeadEnum.Alive;
    ((SlotModel) this).FailLatencyTimes = 0;
    ((SlotModel) this).Latency = 0;
    ((SlotModel) this).Ping = 5;
    ((SlotModel) this).PassSequence = 0;
    ((SlotModel) this).AllKills = 0;
    ((SlotModel) this).AllHeadshots = 0;
    ((SlotModel) this).AllDeaths = 0;
    ((SlotModel) this).AllAssists = 0;
    ((SlotModel) this).BonusFlags = ResultIcon.None;
    ((SlotModel) this).KillsOnLife = 0;
    ((SlotModel) this).LastKillState = 0;
    ((SlotModel) this).Score = 0;
    ((SlotModel) this).Gold = 0;
    ((SlotModel) this).Exp = 0;
    ((SlotModel) this).Cash = 0;
    ((SlotModel) this).Objects = 0;
    ((SlotModel) this).SeasonPoint = 0;
    ((SlotModel) this).BonusItemExp = 0;
    ((SlotModel) this).BonusItemPoint = 0;
    ((SlotModel) this).BonusCafeExp = 0;
    ((SlotModel) this).BonusCafePoint = 0;
    ((SlotModel) this).BonusEventExp = 0;
    ((SlotModel) this).BonusEventPoint = 0;
    ((SlotModel) this).BonusBattlePass = 0;
    ((SlotModel) this).SpawnsCount = 0;
    ((SlotModel) this).DamageBar1 = (ushort) 0;
    ((SlotModel) this).DamageBar2 = (ushort) 0;
    ((SlotModel) this).EarnedEXP = 0;
    ((SlotModel) this).IsPlaying = 0;
    ((SlotModel) this).AiLevel = 0;
    ((SlotModel) this).NextVoteDate = new DateTime();
    ((SlotModel) this).Check = false;
    ((SlotModel) this).SpecGM = false;
    ((SlotModel) this).WithHost = false;
    ((SlotModel) this).Spectator = false;
    ((SlotModel) this).FirstRespawn = true;
    ((SlotModel) this).RepeatLastState = false;
    ((SlotModel) this).MissionsCompleted = false;
    ((SlotModel) this).FirstInactivityOff = false;
    ((SlotModel) this).Missions = (PlayerMissions) null;
    ((SlotModel) this).ItemUsages.Clear();
    Array.Clear((Array) ((SlotModel) this).AR, 0, ((SlotModel) this).AR.Length);
    Array.Clear((Array) ((SlotModel) this).SMG, 0, ((SlotModel) this).SMG.Length);
    Array.Clear((Array) ((SlotModel) this).SR, 0, ((SlotModel) this).SR.Length);
    Array.Clear((Array) ((SlotModel) this).SG, 0, ((SlotModel) this).SG.Length);
    Array.Clear((Array) ((SlotModel) this).MG, 0, ((SlotModel) this).MG.Length);
    Array.Clear((Array) ((SlotModel) this).SHD, 0, ((SlotModel) this).MG.Length);
  }

  public void SetMissionsClone(PlayerMissions value)
  {
    ((SlotModel) this).MissionsCompleted = false;
    ((SlotModel) this).Missions = (PlayerMissions) null;
    ((SlotModel) this).Missions = value.DeepCopy();
  }

  public double InBattleTime(DateTime value)
  {
    return ((SlotModel) this).StartTime == new DateTime() ? 0.0 : (value - ((SlotModel) this).StartTime).TotalSeconds;
  }

  public int CreatorIdx { get; set; }

  public int VictimIdx { get; set; }

  public int Motive { get; set; }

  public int Accept { get; set; }

  public int Denie { get; set; }

  public int Allies { get; set; }

  public int Enemies { get; set; }

  public List<int> Votes
  {
    get => this.list_0;
    [CompilerGenerated, SpecialName] set => ((VoteKickModel) this).list_0 = value;
  }

  public bool[] TotalArray
  {
    [CompilerGenerated, SpecialName] get => ((VoteKickModel) this).bool_0;
    [CompilerGenerated, SpecialName] set => ((VoteKickModel) this).bool_0 = value;
  }
}
