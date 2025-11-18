// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.ServerCache
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class ServerCache
{
  private static void smethod_0(RoomModel roomModel_0, SlotModel teamEnum_0)
  {
    MissionType Kills = MissionType.NA;
    if (teamEnum_0.PassSequence == 1)
      Kills = MissionType.TOUCHDOWN;
    else if (teamEnum_0.PassSequence == 2)
      Kills = MissionType.TOUCHDOWN_ACE_ATTACKER;
    else if (teamEnum_0.PassSequence == 3)
      Kills = MissionType.TOUCHDOWN_HATTRICK;
    else if (teamEnum_0.PassSequence >= 4)
      Kills = MissionType.TOUCHDOWN_GAME_MAKER;
    if (Kills == MissionType.NA)
      return;
    AllUtils.CompleteMission(roomModel_0, teamEnum_0, Kills, 0);
  }

  public static void Load([In] SyncClientPacket obj0)
  {
    int match = (int) obj0.ReadH();
    int num1 = (int) obj0.ReadH();
    int roomModel_0 = (int) obj0.ReadH();
    int int_0 = (int) obj0.ReadC();
    int num2 = (int) obj0.ReadC();
    int num3 = (int) obj0.ReadUH();
    if (obj0.ToArray().Length > 12)
      CLogger.Print($"Invalid Ping (Length > 12): {obj0.ToArray().Length}", LoggerType.Warning, (Exception) null);
    int list_0 = num1;
    ChannelModel channel = AllUtils.GetChannel(roomModel_0, list_0);
    if (channel == null)
      return;
    RoomModel room = ((MatchModel) channel).GetRoom(match);
    if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE)
      return;
    SlotModel slot = room.GetSlot(int_0);
    if (slot == null || slot.State != SlotState.BATTLE)
      return;
    Account playerBySlot = room.GetPlayerBySlot(slot);
    if (room.IsBotMode() || playerBySlot == null)
      return;
    slot.Latency = num3;
    slot.Ping = num2;
    if (slot.Latency >= ConfigLoader.MaxLatency)
      ++slot.FailLatencyTimes;
    else
      slot.FailLatencyTimes = 0;
    if (ConfigLoader.IsDebugPing && ComDiv.GetDuration(playerBySlot.LastPingDebug) >= (double) ConfigLoader.PingUpdateTimeSeconds)
    {
      playerBySlot.LastPingDebug = DateTimeUtil.Now();
      playerBySlot.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK("Server", 0, 5, false, $"{num3}ms ({num2} bar)"));
    }
    if (slot.FailLatencyTimes >= ConfigLoader.MaxRepeatLatency)
    {
      CLogger.Print($"Player: '{playerBySlot.Nickname}' (Id: {slot.PlayerId}) kicked due to high latency. ({slot.Latency}/{ConfigLoader.MaxLatency}ms)", LoggerType.Warning, (Exception) null);
      playerBySlot.Connection.Close(500, true, false);
    }
    else
      AllUtils.RoomPingSync(room);
  }
}
