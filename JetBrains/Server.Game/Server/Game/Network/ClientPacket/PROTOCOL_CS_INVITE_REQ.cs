// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_INVITE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_INVITE_REQ : GameClientPacket
{
  private uint uint_0;

  public PROTOCOL_CS_INVITE_REQ()
  {
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_INVITE_ACCEPT_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_CS_INVITE_ACCEPT_REQ) this).int_1 = (int) this.ReadC();
  }

  public virtual void Run()
  {
    Account player = this.Client.Player;
    if (player == null || player.Nickname.Length == 0)
      return;
    ClanModel clan = ClanManager.GetClan(((PROTOCOL_CS_INVITE_ACCEPT_REQ) this).int_0);
    List<Account> clanPlayers = ClanManager.GetClanPlayers(((PROTOCOL_CS_INVITE_ACCEPT_REQ) this).int_0, -1L, true);
    if (clan.Id == 0)
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_JOIN_REQUEST_ACK(2147487835U));
    else if (player.ClanId > 0)
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_JOIN_REQUEST_ACK(2147487832U));
    else if (clan.MaxPlayers <= clanPlayers.Count)
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_JOIN_REQUEST_ACK(2147487830U));
    }
    else
    {
      if (((PROTOCOL_CS_INVITE_ACCEPT_REQ) this).int_1 != 0 && ((PROTOCOL_CS_INVITE_ACCEPT_REQ) this).int_1 != 1)
        return;
      try
      {
        uint list_1 = 0;
        Account account = ClanManager.GetAccount(clan.OwnerId, 31 /*0x1F*/);
        if (account != null)
        {
          if (DaoManagerSQL.GetMessagesCount(clan.OwnerId) < 100)
          {
            MessageModel bool_1 = this.method_0(clan, player.Nickname, this.Client.PlayerId);
            if (bool_1 != null && account.IsOnline)
              account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
          }
          if (((PROTOCOL_CS_INVITE_ACCEPT_REQ) this).int_1 == 1)
          {
            uint num = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
            if (ComDiv.UpdateDB("accounts", "player_id", (object) player.PlayerId, new string[3]
            {
              "clan_id",
              "clan_access",
              "clan_date"
            }, new object[3]
            {
              (object) clan.Id,
              (object) 3,
              (object) (long) num
            }))
            {
              using (PROTOCOL_CS_MEMBER_INFO_INSERT_ACK memberInfoInsertAck = (PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) new PROTOCOL_CS_NOTE_ACK(player))
                ClanManager.SendPacket((GameServerPacket) memberInfoInsertAck, clanPlayers);
              player.ClanId = clan.Id;
              player.ClanDate = num;
              player.ClanAccess = 3;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(clanPlayers));
              player.Room?.SendPacketToPlayers((GameServerPacket) new PROTOCOL_ROOM_GET_USER_ITEM_ACK(player, clan));
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK(clan, account, clanPlayers.Count + 1));
            }
            else
              list_1 = 2147483648U /*0x80000000*/;
          }
        }
        else
          list_1 = 2147483648U /*0x80000000*/;
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_PASSWD_ACK(list_1));
      }
      catch (Exception ex)
      {
        CLogger.Print(ex.Message, LoggerType.Error, ex);
      }
    }
  }

  private MessageModel method_0([In] ClanModel obj0, string long_0, long long_1)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = obj0.Name,
      SenderId = long_1,
      ClanId = obj0.Id,
      Type = NoteMessageType.Clan,
      Text = long_0,
      State = NoteMessageState.Unreaded,
      ClanNote = ((PROTOCOL_CS_INVITE_ACCEPT_REQ) this).int_1 == 0 ? NoteMessageClan.JoinDenial : NoteMessageClan.JoinAccept
    };
    return !DaoManagerSQL.CreateMessage(obj0.OwnerId, messageModel) ? (MessageModel) null : messageModel;
  }

  public PROTOCOL_CS_INVITE_REQ()
  {
  }
}
