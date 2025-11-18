// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_USER_SOPETYPE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_USER_SOPETYPE_REQ : GameClientPacket
{
  private int int_0;

  private void method_0(Account Player, RoomModel Item, [In] SlotModel obj2, [In] bool obj3)
  {
    if (obj3)
      return;
    obj2.Latency = ((PROTOCOL_BATTLE_TIMERSYNC_REQ) this).int_2;
    obj2.Ping = ((PROTOCOL_BATTLE_TIMERSYNC_REQ) this).int_0;
    if (obj2.Latency >= ConfigLoader.MaxLatency)
      ++obj2.FailLatencyTimes;
    else
      obj2.FailLatencyTimes = 0;
    if (ConfigLoader.IsDebugPing && ComDiv.GetDuration(Player.LastPingDebug) >= (double) ConfigLoader.PingUpdateTimeSeconds)
    {
      Player.LastPingDebug = DateTimeUtil.Now();
      Player.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK("Server", 0, 5, false, $"{((PROTOCOL_BATTLE_TIMERSYNC_REQ) this).int_2}ms ({((PROTOCOL_BATTLE_TIMERSYNC_REQ) this).int_0} bar)"));
    }
    if (obj2.FailLatencyTimes >= ConfigLoader.MaxRepeatLatency)
    {
      CLogger.Print($"Player: '{Player.Nickname}' (Id: {obj2.PlayerId}) kicked due to high latency. ({obj2.Latency}/{ConfigLoader.MaxLatency}ms)", LoggerType.Warning, (Exception) null);
      this.Client.Close(500, true, false);
    }
    else
      AllUtils.RoomPingSync(Item);
  }

  private void method_1([In] RoomModel obj0, [In] bool obj1)
  {
    // ISSUE: variable of a compiler-generated type
    PROTOCOL_BATTLE_TIMERSYNC_REQ.Class4 class4 = (PROTOCOL_BATTLE_TIMERSYNC_REQ.Class4) new PROTOCOL_CHAR_CHANGE_EQUIP_REQ();
    // ISSUE: reference to a compiler-generated field
    class4.roomModel_0 = obj0;
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (class4.roomModel_0.IsDinoMode(""))
      {
        // ISSUE: reference to a compiler-generated field
        if (class4.roomModel_0.Rounds == 1)
        {
          // ISSUE: reference to a compiler-generated field
          class4.roomModel_0.Rounds = 2;
          // ISSUE: reference to a compiler-generated field
          foreach (SlotModel slot in class4.roomModel_0.Slots)
          {
            if (slot.State == SlotState.BATTLE)
            {
              slot.KillsOnLife = 0;
              slot.LastKillState = 0;
              slot.RepeatLastState = false;
            }
          }
          // ISSUE: reference to a compiler-generated field
          List<int> dinossaurs = AllUtils.GetDinossaurs(class4.roomModel_0, true, -2);
          // ISSUE: reference to a compiler-generated field
          using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK Packet = (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(class4.roomModel_0, 2, RoundEndType.TimeOut))
          {
            // ISSUE: reference to a compiler-generated field
            using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK PlayerId = (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(class4.roomModel_0, dinossaurs))
            {
              // ISSUE: reference to a compiler-generated field
              class4.roomModel_0.SendPacketToPlayers((GameServerPacket) Packet, (GameServerPacket) PlayerId, SlotState.BATTLE, 0);
            }
          }
          // ISSUE: reference to a compiler-generated field
          class4.roomModel_0.RoundTime.StartJob(5250, new TimerCallback(((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) class4).method_0));
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (class4.roomModel_0.Rounds != 2)
            return;
          // ISSUE: reference to a compiler-generated field
          AllUtils.EndBattle(class4.roomModel_0, obj1);
        }
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (class4.roomModel_0.ThisModeHaveRounds())
        {
          TeamEnum teamEnum_0;
          // ISSUE: reference to a compiler-generated field
          if (class4.roomModel_0.RoomType != RoomCondition.Destroy)
          {
            // ISSUE: reference to a compiler-generated field
            if (class4.roomModel_0.SwapRound)
            {
              teamEnum_0 = TeamEnum.FR_TEAM;
              // ISSUE: reference to a compiler-generated field
              ++class4.roomModel_0.FRRounds;
            }
            else
            {
              teamEnum_0 = TeamEnum.CT_TEAM;
              // ISSUE: reference to a compiler-generated field
              ++class4.roomModel_0.CTRounds;
            }
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (class4.roomModel_0.Bar1 > class4.roomModel_0.Bar2)
            {
              // ISSUE: reference to a compiler-generated field
              if (class4.roomModel_0.SwapRound)
              {
                teamEnum_0 = TeamEnum.CT_TEAM;
                // ISSUE: reference to a compiler-generated field
                ++class4.roomModel_0.CTRounds;
              }
              else
              {
                teamEnum_0 = TeamEnum.FR_TEAM;
                // ISSUE: reference to a compiler-generated field
                ++class4.roomModel_0.FRRounds;
              }
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              if (class4.roomModel_0.Bar1 < class4.roomModel_0.Bar2)
              {
                // ISSUE: reference to a compiler-generated field
                if (class4.roomModel_0.SwapRound)
                {
                  teamEnum_0 = TeamEnum.FR_TEAM;
                  // ISSUE: reference to a compiler-generated field
                  ++class4.roomModel_0.FRRounds;
                }
                else
                {
                  teamEnum_0 = TeamEnum.CT_TEAM;
                  // ISSUE: reference to a compiler-generated field
                  ++class4.roomModel_0.CTRounds;
                }
              }
              else
                teamEnum_0 = TeamEnum.TEAM_DRAW;
            }
          }
          // ISSUE: reference to a compiler-generated field
          AllUtils.BattleEndRound(class4.roomModel_0, teamEnum_0, RoundEndType.TimeOut);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (class4.roomModel_0.RoomType == RoomCondition.Ace)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            SlotModel[] slotModelArray = new SlotModel[2]
            {
              class4.roomModel_0.GetSlot(0),
              class4.roomModel_0.GetSlot(1)
            };
            if (slotModelArray[0] != null && slotModelArray[0].State == SlotState.BATTLE && slotModelArray[1] != null && slotModelArray[1].State == SlotState.BATTLE)
              return;
            // ISSUE: reference to a compiler-generated field
            AllUtils.EndBattleNoPoints(class4.roomModel_0);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            List<Account> allPlayers = class4.roomModel_0.GetAllPlayers(SlotState.READY, 1);
            if (allPlayers.Count > 0)
            {
              // ISSUE: reference to a compiler-generated field
              TeamEnum winnerTeam = AllUtils.GetWinnerTeam(class4.roomModel_0);
              // ISSUE: reference to a compiler-generated field
              class4.roomModel_0.CalculateResult(winnerTeam, obj1);
              // ISSUE: reference to a compiler-generated field
              using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK missionRoundEndAck = (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(class4.roomModel_0, winnerTeam, RoundEndType.TimeOut))
              {
                int Room1;
                int int_4;
                byte[] byte_1;
                // ISSUE: reference to a compiler-generated field
                AllUtils.GetBattleResult(class4.roomModel_0, ref Room1, ref int_4, ref byte_1);
                byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) missionRoundEndAck).GetCompleteBytes("PROTOCOL_BATTLE_TIMERSYNC_REQ");
                foreach (Account Room2 in allPlayers)
                {
                  // ISSUE: reference to a compiler-generated field
                  if (class4.roomModel_0.Slots[Room2.SlotId].State == SlotState.BATTLE)
                    Room2.SendCompletePacket(completeBytes, missionRoundEndAck.GetType().Name);
                  Room2.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_ENDBATTLE_ACK(Room2, winnerTeam, int_4, Room1, obj1, byte_1));
                  AllUtils.UpdateSeasonPass(Room2);
                }
              }
            }
            // ISSUE: reference to a compiler-generated field
            AllUtils.ResetBattleInfo(class4.roomModel_0);
          }
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
