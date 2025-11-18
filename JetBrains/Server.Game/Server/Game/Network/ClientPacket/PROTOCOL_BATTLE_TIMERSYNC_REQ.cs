// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_TIMERSYNC_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_TIMERSYNC_REQ : GameClientPacket
{
  private float float_0;
  private uint uint_0;
  private int int_0;
  private int int_1;
  private int int_2;
  private int int_3;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.State != RoomState.BATTLE || player.SlotId == ((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).int_1)
        return;
      SlotModel slot = room.GetSlot(player.SlotId);
      if (slot == null || slot.State != SlotState.BATTLE || room.GetSlot(((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).int_1).State != SlotState.BATTLE)
        return;
      int num;
      int Type;
      room.GetPlayingPlayers(true, ref num, ref Type);
      if (player.Rank < ConfigLoader.MinRankVote && !player.IsGM())
        ((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).uint_0 = 2147487972U;
      else if (room.VoteTime.IsTimer())
        ((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).uint_0 = 2147487968U /*0x800010E0*/;
      else if (slot.NextVoteDate > DateTimeUtil.Now())
        ((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).uint_0 = 2147487969U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_USER_SOPETYPE_ACK(((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).uint_0));
      if (((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).uint_0 > 0U)
        return;
      slot.NextVoteDate = DateTimeUtil.Now().AddMinutes(1.0);
      VoteKickModel voteKickModel = new VoteKickModel(slot.Id, ((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).int_1)
      {
        Motive = ((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).int_0
      };
      room.VoteKick = voteKickModel;
      for (int index = 0; index < 18; ++index)
        room.VoteKick.TotalArray[index] = room.Slots[index].State == SlotState.BATTLE;
      using (PROTOCOL_BATTLE_START_KICKVOTE_ACK startKickvoteAck = (PROTOCOL_BATTLE_START_KICKVOTE_ACK) new PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK(room.VoteKick))
        room.SendPacketToPlayers((GameServerPacket) startKickvoteAck, SlotState.BATTLE, 0, player.SlotId, ((PROTOCOL_BATTLE_START_KICKVOTE_REQ) this).int_1);
      room.StartVote();
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_START_KICKVOTE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_BATTLE_TIMERSYNC_REQ()
  {
  }

  public virtual void Read() => ((PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ) this).int_0 = this.ReadD();

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      SlotModel slotModel;
      if (room == null || !room.GetSlot(((PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ) this).int_0, ref slotModel) || player.SlotId != slotModel.Id)
        return;
      player.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_VOTE_KICKVOTE_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_BATTLE_TIMERSYNC_REQ()
  {
  }
}
