// Decompiled with JetBrains decompiler
// Type: Class3
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ClientPacket;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
internal class Class3 : GameClientPacket
{
  private List<SlotModel> list_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      SlotModel slotModel;
      if (room != null && room.IsPreparing() && room.GetSlot(player.SlotId, ref slotModel) && slotModel.State == SlotState.LOAD)
      {
        slotModel.PreLoadDate = DateTimeUtil.Now();
        room.StartCounter(0, player, slotModel);
        room.ChangeSlotState(slotModel, SlotState.RENDEZVOUS, true);
        room.MapName = ((PROTOCOL_ROOM_LOADING_START_REQ) this).string_0;
        if (slotModel.Id == room.LeaderSlot)
        {
          room.UdpServer = SynchronizeXML.GetServer(ConfigLoader.DEFAULT_PORT[2]);
          room.State = RoomState.RENDEZVOUS;
          room.UpdateRoomInfo();
        }
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_REQUEST_MAIN_ACK(0U));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_ROOM_LOADING_START_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ) this).teamEnum_0 = (TeamEnum) this.ReadD();
    ((PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ) this).int_0 = (int) this.ReadC();
  }
}
