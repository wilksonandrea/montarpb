// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_KEEP_ALIVE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_KEEP_ALIVE_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || !player.IsGM())
        return;
      SlotModel slot = room.GetSlot(player.SlotId);
      if (slot == null)
        return;
      slot.ViewType = ((PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ) this).viewerType_0;
      if (slot.ViewType == ViewerType.SpecGM)
        slot.SpecGM = true;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_MEDAL_GET_INFO_ACK(slot.Id));
    }
    catch (Exception ex)
    {
      CLogger.Print($"{this.GetType().Name}; {ex.Message}", LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
