// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_SHOP_ENTER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_SHOP_ENTER_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.LeaderSlot != player.SlotId || room.State != RoomState.READY || ComDiv.GetDuration(room.LastChangeTeam) < 1.5 || room.ChangingSlots)
        return;
      List<SlotChange> slotChangeList = new List<SlotChange>();
      lock (room.Slots)
      {
        room.ChangingSlots = true;
        foreach (int OldSlot in room.FR_TEAM)
        {
          int Player = OldSlot + 1;
          if (OldSlot == room.LeaderSlot)
            room.LeaderSlot = Player;
          else if (Player == room.LeaderSlot)
            room.LeaderSlot = OldSlot;
          room.SwitchSlots(slotChangeList, Player, OldSlot, true);
        }
        if (slotChangeList.Count > 0)
        {
          using (PROTOCOL_ROOM_TEAM_BALANCE_ACK roomTeamBalanceAck = (PROTOCOL_ROOM_TEAM_BALANCE_ACK) new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(slotChangeList, room.LeaderSlot, 2))
          {
            byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) roomTeamBalanceAck).GetCompleteBytes(this.GetType().Name);
            foreach (Account allPlayer in room.GetAllPlayers())
            {
              allPlayer.SlotId = AllUtils.GetNewSlotId(allPlayer.SlotId);
              allPlayer.SendCompletePacket(completeBytes, roomTeamBalanceAck.GetType().Name);
            }
          }
        }
        room.ChangingSlots = false;
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_ROOM_CHANGE_TEAM_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
