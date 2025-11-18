// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_SHOP_LEAVE_REQ
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

public class PROTOCOL_SHOP_LEAVE_REQ : GameClientPacket
{
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
        room.ChangeSlotState(player.SlotId, SlotState.SHOP, false);
        room.StopCountDown(player.SlotId);
        room.UpdateSlotsInfo();
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_SHOP_LEAVE_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_SHOP_GET_SAILLIST_REQ) this).string_0 = this.ReadS(32 /*0x20*/);
  }
}
