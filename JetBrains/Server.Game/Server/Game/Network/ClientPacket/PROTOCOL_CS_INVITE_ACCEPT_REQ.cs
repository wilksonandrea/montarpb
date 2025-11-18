// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_INVITE_ACCEPT_REQ
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

public class PROTOCOL_CS_INVITE_ACCEPT_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;

  private MessageModel method_0([In] ClanModel obj0, long long_0, long long_1)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = obj0.Name,
      SenderId = long_1,
      ClanId = obj0.Id,
      Type = NoteMessageType.Clan,
      State = NoteMessageState.Unreaded,
      ClanNote = NoteMessageClan.Deportation
    };
    return !DaoManagerSQL.CreateMessage(long_0, messageModel) ? (MessageModel) null : messageModel;
  }

  public PROTOCOL_CS_INVITE_ACCEPT_REQ()
  {
    ((PROTOCOL_CS_DEPORTATION_REQ) this).list_0 = new List<long>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_DETAIL_INFO_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_CS_DETAIL_INFO_REQ) this).int_1 = (int) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      player.FindClanId = ((PROTOCOL_CS_DETAIL_INFO_REQ) this).int_0;
      ClanModel clan = ClanManager.GetClan(player.FindClanId);
      if (clan.Id <= 0)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_INVITE_ACK(((PROTOCOL_CS_DETAIL_INFO_REQ) this).int_1, clan));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_DETAIL_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
