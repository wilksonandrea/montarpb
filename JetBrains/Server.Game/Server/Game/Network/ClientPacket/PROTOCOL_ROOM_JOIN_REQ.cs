// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_JOIN_REQ
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

public class PROTOCOL_ROOM_JOIN_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private string string_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      player.Room?.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_JOIN_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ) this).int_1 = this.ReadD();
  }
}
