// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_PAGE_CHATTING_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_PAGE_CHATTING_REQ : GameClientPacket
{
  private ChattingType chattingType_0;
  private string string_0;

  public virtual void Read()
  {
    ((PROTOCOL_CS_NOTE_REQ) this).int_0 = (int) this.ReadC();
    ((PROTOCOL_CS_NOTE_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (((PROTOCOL_CS_NOTE_REQ) this).string_0.Length > 120 || player == null)
        return;
      ClanModel clan = ClanManager.GetClan(player.ClanId);
      int account_1 = 0;
      if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId)
      {
        List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, this.Client.PlayerId, true);
        for (int index = 0; index < clanPlayers.Count; ++index)
        {
          Account account = clanPlayers[index];
          if ((((PROTOCOL_CS_NOTE_REQ) this).int_0 == 0 || account.ClanAccess == 2 && ((PROTOCOL_CS_NOTE_REQ) this).int_0 == 1 || account.ClanAccess == 3 && ((PROTOCOL_CS_NOTE_REQ) this).int_0 == 2) && DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
          {
            ++account_1;
            MessageModel bool_1 = ((PROTOCOL_CS_REPLACE_INTRO_REQ) this).method_0(clan, account.PlayerId, this.Client.PlayerId);
            if (bool_1 != null && account.IsOnline)
              account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
          }
        }
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_POINT_RESET_RESULT_ACK(account_1));
      if (account_1 <= 0)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_PASSWD_ACK(0U));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_NOTE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
