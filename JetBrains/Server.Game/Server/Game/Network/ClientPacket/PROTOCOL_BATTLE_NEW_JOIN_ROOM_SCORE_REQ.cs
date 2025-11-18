// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE || room.TRex != ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ) this).int_1)
        return;
      SlotModel slot = room.GetSlot(player.SlotId);
      if (slot == null || slot.State != SlotState.BATTLE)
        return;
      if (slot.Team == TeamEnum.FR_TEAM)
        room.FRDino += 5;
      else
        room.CTDino += 5;
      using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK touchdownCountAck = (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK) new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(room))
        room.SendPacketToPlayers((GameServerPacket) touchdownCountAck, SlotState.BATTLE, 0);
      AllUtils.CompleteMission(room, player, slot, MissionType.DEATHBLOW, ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ) this).int_0);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
