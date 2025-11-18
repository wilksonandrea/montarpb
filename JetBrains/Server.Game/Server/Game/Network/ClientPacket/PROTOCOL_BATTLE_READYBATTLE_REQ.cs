// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_READYBATTLE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_READYBATTLE_REQ : GameClientPacket
{
  private ViewerType viewerType_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      SlotModel slotModel;
      if (room == null || room.State != RoomState.BATTLE || !room.VoteTime.IsTimer() || room.VoteKick == null || !room.GetSlot(player.SlotId, ref slotModel) || slotModel.State != SlotState.BATTLE)
        return;
      VoteKickModel voteKick = room.VoteKick;
      if (voteKick.Votes.Contains(player.SlotId))
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CHAR_CHANGE_STATE_ACK(2147487985U));
      }
      else
      {
        lock (voteKick.Votes)
          voteKick.Votes.Add(slotModel.Id);
        if (((PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ) this).byte_0 == (byte) 0)
        {
          ++voteKick.Accept;
          if (slotModel.Team == (TeamEnum) (voteKick.VictimIdx % 2))
            ++voteKick.Allies;
          else
            ++voteKick.Enemies;
        }
        else
          ++voteKick.Denie;
        if (voteKick.Votes.Count >= voteKick.GetInGamePlayers())
        {
          room.VoteTime.StopJob();
          AllUtils.VotekickResult(room);
        }
        else
        {
          using (PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK currentKickvoteAck = (PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK) new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK(voteKick))
            room.SendPacketToPlayers((GameServerPacket) currentKickvoteAck, SlotState.BATTLE, 0);
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_REQ) this).mapIdEnum_0 = (MapIdEnum) this.ReadC();
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_REQ) this).mapRules_0 = (MapRules) this.ReadC();
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_REQ) this).stageOptions_0 = (StageOptions) this.ReadC();
    ((PROTOCOL_BATTLE_PRESTARTBATTLE_REQ) this).roomCondition_0 = (RoomCondition) this.ReadC();
  }
}
