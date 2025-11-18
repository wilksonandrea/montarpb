// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.RoomSadeSync
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class RoomSadeSync
{
  public static void Load([In] SyncClientPacket obj0)
  {
    int match = (int) obj0.ReadH();
    int num1 = (int) obj0.ReadH();
    int roomModel_0 = (int) obj0.ReadH();
    byte num2 = obj0.ReadC();
    byte num3 = obj0.ReadC();
    byte num4 = obj0.ReadC();
    int num5 = obj0.ReadD();
    if (obj0.ToArray().Length > 15)
      CLogger.Print($"Invalid Hit (Length > 15): {obj0.ToArray().Length}", LoggerType.Warning, (Exception) null);
    int list_0 = num1;
    ChannelModel channel = AllUtils.GetChannel(roomModel_0, list_0);
    if (channel == null)
      return;
    RoomModel room = ((MatchModel) channel).GetRoom(match);
    if (room == null || room.State != RoomState.BATTLE)
      return;
    Account playerBySlot = room.GetPlayerBySlot((int) num2);
    if (playerBySlot == null)
      return;
    string str = "";
    if (num3 == (byte) 10)
      str = Translation.GetLabel("LifeRestored", new object[1]
      {
        (object) num5
      });
    switch (num4)
    {
      case 0:
        str = Translation.GetLabel("HitMarker1", new object[1]
        {
          (object) num5
        });
        break;
      case 1:
        str = Translation.GetLabel("HitMarker2", new object[1]
        {
          (object) num5
        });
        break;
      case 2:
        str = Translation.GetLabel("HitMarker3");
        break;
      case 3:
        str = Translation.GetLabel("HitMarker4");
        break;
    }
    playerBySlot.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(Translation.GetLabel("HitMarkerName"), playerBySlot.GetSessionId(), 0, true, str));
  }

  public static void Load([In] SyncClientPacket obj0)
  {
    int match = (int) obj0.ReadH();
    int num1 = (int) obj0.ReadH();
    int roomModel_0 = (int) obj0.ReadH();
    int int_0 = (int) obj0.ReadC();
    int num2 = (int) obj0.ReadC();
    if (obj0.ToArray().Length > 10)
      CLogger.Print($"Invalid Portal (Length > 10): {obj0.ToArray().Length}", LoggerType.Warning, (Exception) null);
    int list_0 = num1;
    ChannelModel channel = AllUtils.GetChannel(roomModel_0, list_0);
    if (channel == null)
      return;
    RoomModel room = ((MatchModel) channel).GetRoom(match);
    if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE || !room.IsDinoMode("DE"))
      return;
    SlotModel slot = room.GetSlot(int_0);
    if (slot == null || slot.State != SlotState.BATTLE)
      return;
    ++slot.PassSequence;
    if (slot.Team == TeamEnum.FR_TEAM)
      room.FRDino += 5;
    else
      room.CTDino += 5;
    ServerCache.smethod_0(room, slot);
    using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK Packet = (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK) new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(room, slot))
    {
      using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK PlayerId = (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK) new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(room))
        room.SendPacketToPlayers((GameServerPacket) Packet, (GameServerPacket) PlayerId, SlotState.BATTLE, 0);
    }
  }
}
