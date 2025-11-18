// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_MEMBER_CONTEXT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_MEMBER_CONTEXT_REQ : GameClientPacket
{
  private MessageModel method_1(int clanModel_0, long string_0, long long_0)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = ClanManager.GetClan(clanModel_0).Name,
      ClanId = clanModel_0,
      SenderId = long_0,
      Type = NoteMessageType.ClanAsk,
      State = NoteMessageState.Unreaded,
      ClanNote = NoteMessageClan.Invite
    };
    return !DaoManagerSQL.CreateMessage(string_0, messageModel) ? (MessageModel) null : messageModel;
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_JOIN_REQUEST_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_CS_JOIN_REQUEST_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
  }
}
