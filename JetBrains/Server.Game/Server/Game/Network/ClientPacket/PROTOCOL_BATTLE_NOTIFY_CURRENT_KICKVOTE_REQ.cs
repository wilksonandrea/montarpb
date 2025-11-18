// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ : GameClientPacket
{
  private byte byte_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null)
        return;
      room.State = RoomState.BATTLE_END;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(room));
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(room, 0, RoundEndType.Tutorial));
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_SLOT_ACK(room));
      if (room.State == RoomState.BATTLE_END)
      {
        room.State = RoomState.READY;
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_ENDBATTLE_ACK(player));
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_SLOT_ACK(room));
      }
      AllUtils.ResetBattleInfo(room);
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(room));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
