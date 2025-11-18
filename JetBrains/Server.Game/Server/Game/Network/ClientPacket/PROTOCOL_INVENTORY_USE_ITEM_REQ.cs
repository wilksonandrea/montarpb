// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_INVENTORY_USE_ITEM_REQ
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

public class PROTOCOL_INVENTORY_USE_ITEM_REQ : GameClientPacket
{
  private long long_0;
  private uint uint_0;
  private uint uint_1;
  private byte[] byte_0;
  private string string_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room != null)
      {
        room.ChangeSlotState(player.SlotId, SlotState.INVENTORY, false);
        room.StopCountDown(player.SlotId);
        room.UpdateSlotsInfo();
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_INVENTORY_GET_INFO_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      player.Room?.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_ENTER_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_INVENTORY_LEAVE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
