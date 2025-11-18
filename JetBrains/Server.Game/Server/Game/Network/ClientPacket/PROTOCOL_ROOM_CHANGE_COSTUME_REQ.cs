// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_CHANGE_COSTUME_REQ
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

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_COSTUME_REQ : GameClientPacket
{
  private TeamEnum teamEnum_0;

  public virtual void Read()
  {
    ((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).int_0 = (int) this.ReadC();
    ((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).int_1 = (int) this.ReadC();
    ((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).string_0 = this.ReadU(((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).int_0 * 2);
    ((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).string_1 = this.ReadU(((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).int_1 * 2);
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.Nickname.Length == 0 || player.Nickname == ((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).string_0)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).string_0, 1, 0);
      if (account != null)
      {
        if (DaoManagerSQL.GetMessagesCount(account.PlayerId) >= 100)
        {
          ((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).uint_0 = 2147487871U;
        }
        else
        {
          MessageModel bool_1 = ((PROTOCOL_ROOM_CHANGE_PASSWD_REQ) this).method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
          if (bool_1 != null)
            account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
        }
      }
      else
        ((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).uint_0 = 2147487870U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_PASSWD_ACK(((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_MESSENGER_NOTE_SEND_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
