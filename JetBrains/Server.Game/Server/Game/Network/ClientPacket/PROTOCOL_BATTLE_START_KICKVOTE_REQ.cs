// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_START_KICKVOTE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_START_KICKVOTE_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      SlotModel slotModel;
      if (room == null || !room.GetSlot(player.SlotId, ref slotModel))
        return;
      int num = 0;
      if (slotModel == null || slotModel.State < SlotState.BATTLE_READY)
        return;
      if (room.State == RoomState.BATTLE)
        room.Ping = (int) ((PROTOCOL_BATTLE_SENDPING_REQ) this).byte_0[room.LeaderSlot];
      using (PROTOCOL_BATTLE_SENDPING_ACK battleSendpingAck = (PROTOCOL_BATTLE_SENDPING_ACK) new PROTOCOL_BATTLE_STARTBATTLE_ACK(((PROTOCOL_BATTLE_SENDPING_REQ) this).byte_0))
      {
        List<Account> allPlayers = room.GetAllPlayers(SlotState.READY, 1);
        if (allPlayers.Count == 0)
          return;
        byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) battleSendpingAck).GetCompleteBytes(this.GetType().Name);
        foreach (Account account in allPlayers)
        {
          SlotModel slot = room.GetSlot(account.SlotId);
          if (slot != null && slot.State >= SlotState.BATTLE_READY)
            account.SendCompletePacket(completeBytes, battleSendpingAck.GetType().Name);
          else
            ++num;
        }
      }
      if (num != 0)
        return;
      room.SpawnReadyPlayers();
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_SENDPING_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
