// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ
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

public class PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ : GameClientPacket
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
      if (player == null)
        return;
      MatchModel match = player.Match;
      if (match != null && player.MatchSlot == match.Leader)
      {
        match.Training = ((PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ) this).int_0;
        using (PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK mercenaryReceiverAck = (PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK) new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(0U, ((PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ) this).int_0))
          match.SendPacketToPlayers((GameServerPacket) mercenaryReceiverAck);
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(2147483648U /*0x80000000*/, 0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ) this).teamEnum_0 = (TeamEnum) this.ReadH();
    ((PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ) this).int_1 = (int) this.ReadH();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.ClanId == 0 || player.Match == null)
        return;
      ChannelModel C;
      if (player != null && player.Nickname.Length > 0 && player.Room == null && player.GetChannel(ref C))
      {
        RoomModel room = ((MatchModel) C).GetRoom(((PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ) this).int_0);
        if (room != null && room.GetLeader() != null)
        {
          if (room.Password.Length > 0 && !player.IsGM())
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487749U /*0x80001005*/, (Account) null));
          else if (room.Limit == (byte) 1 && room.State >= RoomState.COUNTDOWN)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487763U, (Account) null));
          else if (room.KickedPlayersVote.Contains(player.PlayerId))
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487756U /*0x8000100C*/, (Account) null));
          else if (room.AddPlayer(player, ((PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ) this).teamEnum_0) >= 0)
          {
            using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK LeaderSlot = (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) new PROTOCOL_ROOM_GET_USER_ITEM_ACK(player))
              room.SendPacketToPlayers((GameServerPacket) LeaderSlot, player.PlayerId);
            room.UpdateSlotsInfo();
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(0U, player));
          }
          else
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487747U /*0x80001003*/, (Account) null));
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487748U /*0x80001004*/, (Account) null));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487748U /*0x80001004*/, (Account) null));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
