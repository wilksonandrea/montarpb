// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_DEPORTATION_REQ
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
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_DEPORTATION_REQ : GameClientPacket
{
  private List<long> list_0;
  private uint uint_0;

  public virtual void Read()
  {
    int num = (int) this.ReadC();
    for (int index = 0; index < num; ++index)
      ((PROTOCOL_CS_DENIAL_REQUEST_REQ) this).list_0.Add(this.ReadQ());
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ClanModel clan = ClanManager.GetClan(player.ClanId);
      if (clan.Id > 0 && (player.ClanAccess >= 1 && player.ClanAccess <= 2 || clan.OwnerId == player.PlayerId))
      {
        for (int index = 0; index < ((PROTOCOL_CS_DENIAL_REQUEST_REQ) this).list_0.Count; ++index)
        {
          long num = ((PROTOCOL_CS_DENIAL_REQUEST_REQ) this).list_0[index];
          if (DaoManagerSQL.DeleteClanInviteDB(clan.Id, num))
          {
            if (DaoManagerSQL.GetMessagesCount(num) < 100)
            {
              MessageModel bool_1 = this.method_0(clan, num, player.PlayerId);
              if (bool_1 != null)
              {
                Account account = ClanManager.GetAccount(num, 31 /*0x1F*/);
                if (account != null && account.IsOnline)
                  account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
              }
            }
            ((PROTOCOL_CS_DENIAL_REQUEST_REQ) this).int_0 = ((PROTOCOL_CS_DENIAL_REQUEST_REQ) this).int_0 + 1;
          }
        }
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_DEPORTATION_RESULT_ACK(((PROTOCOL_CS_DENIAL_REQUEST_REQ) this).int_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_DENIAL_REQUEST_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  private MessageModel method_0([In] ClanModel obj0, long long_0, long long_1)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = obj0.Name,
      SenderId = long_1,
      ClanId = obj0.Id,
      Type = NoteMessageType.Clan,
      State = NoteMessageState.Unreaded,
      ClanNote = NoteMessageClan.InviteDenial
    };
    return !DaoManagerSQL.CreateMessage(long_0, messageModel) ? (MessageModel) null : messageModel;
  }
}
