// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.RoomPassPortal
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public static class RoomPassPortal
{
  public static void EndBattleByDeath(
    [In] RoomModel obj0,
    SlotModel Killer,
    [Out] bool Score,
    bool IsBotMode,
    FragInfos IsSuicide)
  {
    if (obj0.RoomType == RoomCondition.DeathMatch && !Score)
      AllUtils.BattleEndKills(obj0, Score);
    else if (obj0.RoomType == RoomCondition.FreeForAll)
    {
      AllUtils.BattleEndKillsFreeForAll(obj0);
    }
    else
    {
      if (Killer.SpecGM || obj0.RoomType != RoomCondition.Bomb && obj0.RoomType != RoomCondition.Annihilation && obj0.RoomType != RoomCondition.Destroy && obj0.RoomType != RoomCondition.Ace)
        return;
      if (obj0.RoomType != RoomCondition.Bomb && obj0.RoomType != RoomCondition.Annihilation && obj0.RoomType != RoomCondition.Destroy)
      {
        if (obj0.RoomType != RoomCondition.Ace)
          return;
        SlotModel[] slotModelArray = new SlotModel[2]
        {
          obj0.GetSlot(0),
          obj0.GetSlot(1)
        };
        if (slotModelArray[0].DeathState == DeadEnum.Dead)
        {
          ++obj0.CTRounds;
          AllUtils.BattleEndRound(obj0, TeamEnum.CT_TEAM, true, IsSuicide, Killer);
        }
        else
        {
          if (slotModelArray[1].DeathState != DeadEnum.Dead)
            return;
          ++obj0.FRRounds;
          AllUtils.BattleEndRound(obj0, TeamEnum.FR_TEAM, true, IsSuicide, Killer);
        }
      }
      else
      {
        TeamEnum isBotMode = TeamEnum.TEAM_DRAW;
        int IsBotMode1;
        int num1;
        int num2;
        int num3;
        obj0.GetPlayingPlayers(true, ref IsBotMode1, ref num1, ref num2, ref num3);
        TeamEnum teamEnum;
        RoomPassPortal.smethod_0(obj0, Killer, ref IsBotMode1, ref num1, ref num2, ref num3, ref teamEnum);
        if (((num2 != IsBotMode1 ? 0 : (teamEnum == TeamEnum.FR_TEAM ? 1 : 0)) & (IsBotMode ? 1 : 0)) != 0 && !obj0.ActiveC4)
        {
          RoomPing.smethod_1(obj0, ref isBotMode, 1);
          AllUtils.BattleEndRound(obj0, isBotMode, true, IsSuicide, Killer);
        }
        else if (num3 == num1 && teamEnum == TeamEnum.CT_TEAM)
        {
          RoomPing.smethod_1(obj0, ref isBotMode, 2);
          AllUtils.BattleEndRound(obj0, isBotMode, true, IsSuicide, Killer);
        }
        else if (num2 == IsBotMode1 && teamEnum == TeamEnum.CT_TEAM)
        {
          if (!obj0.ActiveC4)
            RoomPing.smethod_1(obj0, ref isBotMode, 1);
          else if (IsBotMode)
            RoomPing.smethod_1(obj0, ref isBotMode, 2);
          AllUtils.BattleEndRound(obj0, isBotMode, false, IsSuicide, Killer);
        }
        else
        {
          if (num3 != num1 || teamEnum != TeamEnum.FR_TEAM)
            return;
          if (IsBotMode && obj0.ActiveC4)
            RoomPing.smethod_1(obj0, ref isBotMode, 1);
          else
            RoomPing.smethod_1(obj0, ref isBotMode, 2);
          AllUtils.BattleEndRound(obj0, isBotMode, true, IsSuicide, Killer);
        }
      }
    }
  }

  private static void smethod_0(
    RoomModel Room,
    SlotModel Killer,
    ref int IsBotMode,
    ref int IsSuicide,
    ref int Kills,
    [In] ref int obj5,
    [In] ref TeamEnum obj6)
  {
    obj6 = Killer.Team;
    if (!Room.SwapRound)
      return;
    obj6 = obj6 == TeamEnum.FR_TEAM ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM;
    int num1 = IsBotMode;
    int num2 = IsSuicide;
    IsSuicide = num1;
    IsBotMode = num2;
    int num3 = Kills;
    int num4 = obj5;
    obj5 = num3;
    Kills = num4;
  }
}
