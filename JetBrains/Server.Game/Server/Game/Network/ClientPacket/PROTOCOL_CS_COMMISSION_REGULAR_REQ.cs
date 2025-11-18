// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_COMMISSION_REGULAR_REQ
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

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_COMMISSION_REGULAR_REQ : GameClientPacket
{
  private List<long> list_0;
  private uint uint_0;

  public virtual void Read() => ((PROTOCOL_CS_COMMISSION_MASTER_REQ) this).long_0 = this.ReadQ();

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.ClanAccess != 1)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_CS_COMMISSION_MASTER_REQ) this).long_0, 31 /*0x1F*/);
      int clanId = player.ClanId;
      if (account != null && account.ClanId == clanId)
      {
        if (account.Rank > 10)
        {
          ClanModel clan = ClanManager.GetClan(clanId);
          if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId && account.ClanAccess == 2 && ComDiv.UpdateDB("system_clan", "owner_id", (object) ((PROTOCOL_CS_COMMISSION_MASTER_REQ) this).long_0, "id", (object) clanId) && ComDiv.UpdateDB("accounts", "clan_access", (object) 1, "player_id", (object) ((PROTOCOL_CS_COMMISSION_MASTER_REQ) this).long_0) && ComDiv.UpdateDB("accounts", "clan_access", (object) 2, "player_id", (object) player.PlayerId))
          {
            account.ClanAccess = 1;
            player.ClanAccess = 2;
            clan.OwnerId = ((PROTOCOL_CS_COMMISSION_MASTER_REQ) this).long_0;
            if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
            {
              MessageModel bool_1 = this.method_0(clan, account.PlayerId, player.PlayerId);
              if (bool_1 != null && account.IsOnline)
                account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
            }
            if (account.IsOnline)
              account.SendPacket((GameServerPacket) new PROTOCOL_CS_COMMISSION_REGULAR_RESULT_ACK(), false);
          }
          else
            ((PROTOCOL_CS_COMMISSION_MASTER_REQ) this).uint_0 = 2147487744U /*0x80001000*/;
        }
        else
          ((PROTOCOL_CS_COMMISSION_MASTER_REQ) this).uint_0 = 2147487928U;
      }
      else
        ((PROTOCOL_CS_COMMISSION_MASTER_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_COMMISSION_REGULAR_ACK(((PROTOCOL_CS_COMMISSION_MASTER_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_COMMISSION_MASTER_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = clanModel_0.Name,
      SenderId = long_1,
      ClanId = clanModel_0.Id,
      Type = NoteMessageType.Clan,
      State = NoteMessageState.Unreaded,
      ClanNote = NoteMessageClan.Master
    };
    return !DaoManagerSQL.CreateMessage(long_0, messageModel) ? (MessageModel) null : messageModel;
  }
}
