// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ : GameClientPacket
{
  private TeamEnum teamEnum_0;
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ChannelModel C;
      if (player.Nickname.Length > 0 && player.Room == null && player.Match == null && player.GetChannel(ref C))
      {
        RoomModel room = ((MatchModel) C).GetRoom(((PROTOCOL_ROOM_JOIN_REQ) this).int_0);
        if (room != null && room.GetLeader() != null)
        {
          if (room.RoomType == RoomCondition.Tutorial)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487868U, (Account) null));
          else if (room.Password.Length > 0 && ((PROTOCOL_ROOM_JOIN_REQ) this).string_0 != room.Password && player.Rank != 53 && !player.IsGM() && ((PROTOCOL_ROOM_JOIN_REQ) this).int_1 != 1)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487749U /*0x80001005*/, (Account) null));
          else if (room.Limit == (byte) 1 && room.State >= RoomState.COUNTDOWN && !player.IsGM())
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487763U, (Account) null));
          else if (room.KickedPlayersVote.Contains(player.PlayerId) && !player.IsGM())
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_LOADING_START_ACK(2147487756U /*0x8000100C*/, (Account) null));
          else if (room.KickedPlayersHost.ContainsKey(player.PlayerId) && ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId]) < (double) ConfigLoader.IntervalEnterRoomAfterKickSeconds)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(Translation.GetLabel("KickByHostMessage", new object[2]
            {
              (object) ConfigLoader.IntervalEnterRoomAfterKickSeconds,
              (object) (int) ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId])
            })));
          else if (room.AddPlayer(player) >= 0)
          {
            player.ResetPages();
            using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK LeaderSlot = (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK) new PROTOCOL_ROOM_GET_USER_ITEM_ACK(player))
              room.SendPacketToPlayers((GameServerPacket) LeaderSlot, player.PlayerId);
            if (room.Competitive)
              AllUtils.SendCompetitiveInfo(player);
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
      CLogger.Print("PROTOCOL_LOBBY_JOIN_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_ROOM_LOADING_START_REQ) this).string_0 = this.ReadS((int) this.ReadC());
  }
}
