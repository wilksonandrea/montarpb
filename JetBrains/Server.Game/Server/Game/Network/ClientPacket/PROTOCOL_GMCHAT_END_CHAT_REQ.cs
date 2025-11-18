// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_GMCHAT_END_CHAT_REQ
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

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_END_CHAT_REQ : GameClientPacket
{
  private long long_0;

  public virtual void Read()
  {
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.ClanId > 0)
      {
        ClanModel clan = ClanManager.GetClan(player.ClanId);
        if (clan.Id > 0 && clan.OwnerId != player.PlayerId)
        {
          if (ComDiv.UpdateDB("accounts", "player_id", (object) player.PlayerId, new string[2]
          {
            "clan_id",
            "clan_access"
          }, new object[2]{ (object) 0, (object) 0 }))
          {
            if (ComDiv.UpdateDB("player_stat_clans", "owner_id", (object) player.PlayerId, new string[2]
            {
              "clan_matches",
              "clan_match_wins"
            }, new object[2]{ (object) 0, (object) 0 }))
            {
              using (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK Packet = (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK) new PROTOCOL_CS_MEMBER_LIST_ACK(player.PlayerId))
                ClanManager.SendPacket((GameServerPacket) Packet, player.ClanId, player.PlayerId, true, true);
              long ownerId = clan.OwnerId;
              if (DaoManagerSQL.GetMessagesCount(ownerId) < 100)
              {
                MessageModel bool_1 = ((PROTOCOL_GMCHAT_START_CHAT_REQ) this).method_0(clan, player);
                if (bool_1 != null)
                {
                  Account account = ClanManager.GetAccount(ownerId, 31 /*0x1F*/);
                  if (account != null && account.IsOnline)
                    account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
                }
              }
              player.ClanId = 0;
              player.ClanAccess = 0;
              goto label_18;
            }
          }
          ((PROTOCOL_CS_SECESSION_CLAN_REQ) this).uint_0 = 2147487851U;
        }
        else
          ((PROTOCOL_CS_SECESSION_CLAN_REQ) this).uint_0 = 2147487838U;
      }
      else
        ((PROTOCOL_CS_SECESSION_CLAN_REQ) this).uint_0 = 2147487835U;
label_18:
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_GMCHAT_START_CHAT_ACK(((PROTOCOL_CS_SECESSION_CLAN_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_SECESSION_CLAN_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
