// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CREATE_CLAN_REQ
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

public class PROTOCOL_CS_CREATE_CLAN_REQ : GameClientPacket
{
  private uint uint_0;
  private string string_0;
  private string string_1;

  private MessageModel method_0([In] ClanModel obj0, long long_1, long long_2)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = obj0.Name,
      SenderId = long_2,
      ClanId = obj0.Id,
      Type = NoteMessageType.Clan,
      State = NoteMessageState.Unreaded,
      ClanNote = NoteMessageClan.Staff
    };
    return !DaoManagerSQL.CreateMessage(long_1, messageModel) ? (MessageModel) null : messageModel;
  }

  public PROTOCOL_CS_CREATE_CLAN_REQ()
  {
    ((PROTOCOL_CS_COMMISSION_STAFF_REQ) this).list_0 = new List<long>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
  }
}
