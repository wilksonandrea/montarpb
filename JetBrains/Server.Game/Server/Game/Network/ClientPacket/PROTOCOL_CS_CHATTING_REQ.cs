// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CHATTING_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CHATTING_REQ : GameClientPacket
{
  private ChattingType chattingType_0;
  private string string_0;

  private MessageModel method_0(ClanModel object_0, long list_1, [In] long obj2)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = object_0.Name,
      SenderId = obj2,
      ClanId = object_0.Id,
      Type = NoteMessageType.Clan,
      State = NoteMessageState.Unreaded,
      ClanNote = NoteMessageClan.InviteAccept
    };
    return !DaoManagerSQL.CreateMessage(list_1, messageModel) ? (MessageModel) null : messageModel;
  }

  public PROTOCOL_CS_CHATTING_REQ()
  {
    ((PROTOCOL_CS_ACCEPT_REQUEST_REQ) this).list_0 = new List<long>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
  }
}
