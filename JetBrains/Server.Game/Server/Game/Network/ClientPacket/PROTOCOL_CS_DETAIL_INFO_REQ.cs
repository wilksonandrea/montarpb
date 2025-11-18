// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_DETAIL_INFO_REQ
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

public class PROTOCOL_CS_DETAIL_INFO_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;

  public PROTOCOL_CS_DETAIL_INFO_REQ()
  {
    ((PROTOCOL_CS_DENIAL_REQUEST_REQ) this).list_0 = new List<long>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    int num = (int) this.ReadC();
    for (int index = 0; index < num; ++index)
      ((PROTOCOL_CS_DEPORTATION_REQ) this).list_0.Add(this.ReadQ());
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ClanModel clan = ClanManager.GetClan(player.ClanId);
      if (clan.Id != 0 && (player.ClanAccess >= 1 && player.ClanAccess <= 2 || clan.OwnerId == this.Client.PlayerId))
      {
        List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, -1L, true);
        for (int index = 0; index < ((PROTOCOL_CS_DEPORTATION_REQ) this).list_0.Count; ++index)
        {
          Account account = ClanManager.GetAccount(((PROTOCOL_CS_DEPORTATION_REQ) this).list_0[index], 31 /*0x1F*/);
          if (account != null && account.ClanId == clan.Id && account.Match == null)
          {
            if (ComDiv.UpdateDB("accounts", "player_id", (object) account.PlayerId, new string[2]
            {
              "clan_id",
              "clan_access"
            }, new object[2]{ (object) 0, (object) 0 }))
            {
              if (ComDiv.UpdateDB("player_stat_clans", "owner_id", (object) account.PlayerId, new string[2]
              {
                "clan_matches",
                "clan_match_wins"
              }, new object[2]{ (object) 0, (object) 0 }))
              {
                using (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK memberInfoDeleteAck = (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK) new PROTOCOL_CS_MEMBER_LIST_ACK(account.PlayerId))
                  ClanManager.SendPacket((GameServerPacket) memberInfoDeleteAck, clanPlayers, account.PlayerId);
                account.ClanId = 0;
                account.ClanAccess = 0;
                SendFriendInfo.Load(account, (Account) null, 0);
                if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
                {
                  MessageModel bool_1 = ((PROTOCOL_CS_INVITE_ACCEPT_REQ) this).method_0(clan, account.PlayerId, this.Client.PlayerId);
                  if (bool_1 != null && account.IsOnline)
                    account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
                }
                if (account.IsOnline)
                  account.SendPacket((GameServerPacket) new PROTOCOL_CS_INVITE_ACCEPT_ACK(), false);
                ((PROTOCOL_CS_DEPORTATION_REQ) this).uint_0 = ((PROTOCOL_CS_DEPORTATION_REQ) this).uint_0 + 1U;
                clanPlayers.Remove(account);
                continue;
              }
            }
          }
          ((PROTOCOL_CS_DEPORTATION_REQ) this).uint_0 = 2147487833U;
          break;
        }
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_DETAIL_INFO_ACK(((PROTOCOL_CS_DEPORTATION_REQ) this).uint_0));
      }
      else
        ((PROTOCOL_CS_DEPORTATION_REQ) this).uint_0 = 2147487833U;
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_DEPORTATION_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
