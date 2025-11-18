// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CANCEL_REQUEST_REQ
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
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CANCEL_REQUEST_REQ : GameClientPacket
{
  private uint uint_0;

  public virtual void Read()
  {
    int num = (int) this.ReadC();
    for (int index = 0; index < num; ++index)
      ((PROTOCOL_CS_ACCEPT_REQUEST_REQ) this).list_0.Add(this.ReadQ());
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ClanModel clan = ClanManager.GetClan(player.ClanId);
      if (clan.Id > 0 && (player.ClanAccess >= 1 && player.ClanAccess <= 2 || player.PlayerId == clan.OwnerId))
      {
        List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, -1L, true);
        if (clanPlayers.Count >= clan.MaxPlayers)
        {
          ((PROTOCOL_CS_ACCEPT_REQUEST_REQ) this).int_0 = -1;
          return;
        }
        for (int index = 0; index < ((PROTOCOL_CS_ACCEPT_REQUEST_REQ) this).list_0.Count; ++index)
        {
          Account account = ClanManager.GetAccount(((PROTOCOL_CS_ACCEPT_REQUEST_REQ) this).list_0[index], 31 /*0x1F*/);
          if (account != null && clanPlayers.Count < clan.MaxPlayers && account.ClanId == 0 && DaoManagerSQL.GetRequestClanId(account.PlayerId) > 0)
          {
            using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK memberInfoChangeAck = (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) new PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(account))
              ClanManager.SendPacket((GameServerPacket) memberInfoChangeAck, clanPlayers);
            account.ClanId = player.ClanId;
            account.ClanDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
            account.ClanAccess = 3;
            SendFriendInfo.Load(account, (Account) null, 3);
            ComDiv.UpdateDB("accounts", "player_id", (object) account.PlayerId, new string[3]
            {
              "clan_access",
              "clan_id",
              "clan_date"
            }, new object[3]
            {
              (object) account.ClanAccess,
              (object) account.ClanId,
              (object) (long) account.ClanDate
            });
            DaoManagerSQL.DeleteClanInviteDB(player.ClanId, account.PlayerId);
            if (account.IsOnline)
            {
              account.SendPacket((GameServerPacket) new PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(clanPlayers), false);
              account.Room?.SendPacketToPlayers((GameServerPacket) new PROTOCOL_ROOM_GET_USER_ITEM_ACK(account, clan));
              account.SendPacket((GameServerPacket) new PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK(clan, clanPlayers.Count + 1), false);
            }
            if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
            {
              MessageModel bool_1 = ((PROTOCOL_CS_CHATTING_REQ) this).method_0(clan, account.PlayerId, this.Client.PlayerId);
              if (bool_1 != null && account.IsOnline)
                account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
            }
            ((PROTOCOL_CS_ACCEPT_REQUEST_REQ) this).int_0 = ((PROTOCOL_CS_ACCEPT_REQUEST_REQ) this).int_0 + 1;
            clanPlayers.Add(account);
          }
        }
      }
      else
        ((PROTOCOL_CS_ACCEPT_REQUEST_REQ) this).int_0 = -1;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CANCEL_REQUEST_ACK((uint) ((PROTOCOL_CS_ACCEPT_REQUEST_REQ) this).int_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_ACCEPT_REQUEST_RESULT_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
