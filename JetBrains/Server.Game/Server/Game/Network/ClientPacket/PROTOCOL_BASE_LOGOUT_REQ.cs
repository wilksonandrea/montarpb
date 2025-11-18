// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_LOGOUT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_LOGOUT_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      Account State;
      if (room == null || !room.GetPlayerBySlot(((PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ) this).int_0, ref State))
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(State));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
