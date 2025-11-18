// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_PRESTARTBATTLE_REQ
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

public class PROTOCOL_BATTLE_PRESTARTBATTLE_REQ : GameClientPacket
{
  private StageOptions stageOptions_0;
  private MapRules mapRules_0;
  private MapIdEnum mapIdEnum_0;
  private RoomCondition roomCondition_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.State < RoomState.LOADING || room.Slots[player.SlotId].State != SlotState.NORMAL)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK(room));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ) this).byte_0 = this.ReadC();
  }
}
