// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_MESSENGER_NOTE_SEND_REQ
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

public class PROTOCOL_MESSENGER_NOTE_SEND_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private string string_0;
  private string string_1;
  private uint uint_0;

  public PROTOCOL_MESSENGER_NOTE_SEND_REQ()
  {
    ((PROTOCOL_MESSENGER_NOTE_DELETE_REQ) this).list_0 = new List<object>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ) this).long_0 = this.ReadQ();
    ((PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || this.Client.PlayerId == ((PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ) this).long_0)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ) this).long_0, 31 /*0x1F*/);
      if (account != null)
      {
        if (DaoManagerSQL.GetMessagesCount(account.PlayerId) >= 100)
        {
          ((PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ) this).uint_0 = 2147487871U;
        }
        else
        {
          MessageModel bool_1 = this.method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
          if (bool_1 != null)
            account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
        }
      }
      else
        ((PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ) this).uint_0 = 2147487870U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_PASSWD_ACK(((PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  private MessageModel method_0(string int_0, long int_1, long list_0)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = int_0,
      SenderId = list_0,
      Text = ((PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ) this).string_0,
      State = NoteMessageState.Unreaded
    };
    if (DaoManagerSQL.CreateMessage(int_1, messageModel))
      return messageModel;
    ((PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
    return (MessageModel) null;
  }
}
