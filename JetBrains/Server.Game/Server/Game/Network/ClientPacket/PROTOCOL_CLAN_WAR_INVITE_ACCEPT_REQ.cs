// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ
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

public class PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private int int_2;
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.ClanId == 0)
        return;
      ChannelModel channel = player.GetChannel();
      MatchModel match1 = player.Match;
      if (channel == null || match1 == null)
        return;
      MatchModel match2 = channel.GetMatch(((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_3);
      if (match2 == null)
        return;
      lock (channel.Rooms)
      {
        for (int index = 0; index < channel.MaxRooms; ++index)
        {
          if (((MatchModel) channel).GetRoom(index) == null)
          {
            RoomModel roomModel = new RoomModel(index, channel)
            {
              Name = ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).string_0,
              MapId = ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).mapIdEnum_0,
              Rule = ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).mapRules_0,
              Stage = ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).stageOptions_0,
              RoomType = ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).roomCondition_0
            };
            roomModel.SetSlotCount(((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_1, true, false);
            roomModel.Ping = ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_2;
            roomModel.WeaponsFlag = ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).roomWeaponsFlag_0;
            roomModel.Flag = ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).roomStageFlag_0;
            roomModel.Password = "";
            roomModel.KillTime = 3;
            if (roomModel.AddPlayer(player) >= 0)
            {
              channel.AddRoom(roomModel);
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(0U, roomModel));
              ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_0 = index;
              return;
            }
          }
        }
      }
      using (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK clanWarEnemyInfoAck = (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(match2))
      {
        using (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK clanWarJoinRoomAck = (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK) new PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK(match2, ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_0, 0))
        {
          byte[] completeBytes1 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) clanWarEnemyInfoAck).GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-1");
          byte[] completeBytes2 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) clanWarJoinRoomAck).GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-2");
          foreach (Account allPlayer in match1.GetAllPlayers(match1.Leader))
          {
            allPlayer.SendCompletePacket(completeBytes1, clanWarEnemyInfoAck.GetType().Name);
            allPlayer.SendCompletePacket(completeBytes2, clanWarJoinRoomAck.GetType().Name);
            if (allPlayer.Match != null)
              match1.Slots[allPlayer.MatchSlot].State = SlotMatchState.Ready;
          }
        }
      }
      using (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK clanWarEnemyInfoAck = (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(match1))
      {
        using (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK clanWarJoinRoomAck = (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK) new PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK(match1, ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_0, 1))
        {
          byte[] completeBytes3 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) clanWarEnemyInfoAck).GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-3");
          byte[] completeBytes4 = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) clanWarJoinRoomAck).GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-4");
          foreach (Account allPlayer in match2.GetAllPlayers())
          {
            allPlayer.SendCompletePacket(completeBytes3, clanWarEnemyInfoAck.GetType().Name);
            allPlayer.SendCompletePacket(completeBytes4, clanWarJoinRoomAck.GetType().Name);
            if (allPlayer.Match != null)
              match1.Slots[allPlayer.MatchSlot].State = SlotMatchState.Ready;
          }
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ()
  {
    ((PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ) this).int_0 = -1;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ) this).int_0 = (int) this.ReadC();
  }
}
