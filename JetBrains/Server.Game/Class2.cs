// Decompiled with JetBrains decompiler
// Type: Class2
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ClientPacket;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
internal class Class2 : GameClientPacket
{
  private uint uint_0;
  private List<SlotModel> list_0;

  public virtual void Read() => ((PROTOCOL_ROOM_CHANGE_SLOT_REQ) this).int_0 = this.ReadD();

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      SlotModel slotModel;
      if (room != null && room.LeaderSlot == player.SlotId && room.GetSlot(((PROTOCOL_ROOM_CHANGE_SLOT_REQ) this).int_0 & 268435455 /*0x0FFFFFFF*/, ref slotModel))
      {
        if ((((PROTOCOL_ROOM_CHANGE_SLOT_REQ) this).int_0 & 268435456 /*0x10000000*/) == 268435456 /*0x10000000*/)
          ((PROTOCOL_ROOM_CREATE_REQ) this).method_1(player, room, slotModel);
        else
          this.method_0(player, room, slotModel);
        switch (slotModel.State)
        {
          case SlotState.UNKNOWN:
          case SlotState.SHOP:
          case SlotState.INFO:
          case SlotState.CLAN:
          case SlotState.INVENTORY:
          case SlotState.GACHA:
          case SlotState.GIFTSHOP:
          case SlotState.NORMAL:
          case SlotState.READY:
            Account playerBySlot = room.GetPlayerBySlot(slotModel);
            if (playerBySlot != null && !playerBySlot.AntiKickGM && (slotModel.State != SlotState.READY && (room.ChannelType == ChannelType.Clan && room.State != RoomState.COUNTDOWN || room.ChannelType != ChannelType.Clan) || slotModel.State == SlotState.READY && (room.ChannelType == ChannelType.Clan && room.State == RoomState.READY || room.ChannelType != ChannelType.Clan)))
            {
              playerBySlot.SendPacket((GameServerPacket) new PROTOCOL_SHOP_ENTER_ACK());
              if (!room.KickedPlayersHost.ContainsKey(player.PlayerId))
                room.KickedPlayersHost.Add(player.PlayerId, DateTimeUtil.Now());
              else
                room.KickedPlayersHost[player.PlayerId] = DateTimeUtil.Now();
              room.RemovePlayer(playerBySlot, slotModel, false, 0);
              break;
            }
            break;
          case SlotState.LOAD:
          case SlotState.RENDEZVOUS:
          case SlotState.PRESTART:
          case SlotState.BATTLE_LOAD:
          case SlotState.BATTLE_READY:
          case SlotState.BATTLE:
            ((PROTOCOL_ROOM_CHANGE_SLOT_REQ) this).uint_0 = 2147484673U /*0x80000401*/;
            break;
        }
      }
      else
        ((PROTOCOL_ROOM_CHANGE_SLOT_REQ) this).uint_0 = 2147484673U /*0x80000401*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHECK_MAIN_ACK(((PROTOCOL_ROOM_CHANGE_SLOT_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_ROOM_CHANGE_SLOT_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_0([In] Account obj0, RoomModel long_1, SlotModel long_2)
  {
    if (long_2.State != SlotState.EMPTY)
      return;
    if (long_1.Competitive && !AllUtils.CanCloseSlotCompetitive(long_1, long_2))
      obj0.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(Translation.GetLabel("Competitive"), obj0.Session.SessionId, obj0.NickColor, true, Translation.GetLabel("CompetitiveSlotMin")));
    long_1.ChangeSlotState(long_2, SlotState.CLOSE, true);
  }
}
