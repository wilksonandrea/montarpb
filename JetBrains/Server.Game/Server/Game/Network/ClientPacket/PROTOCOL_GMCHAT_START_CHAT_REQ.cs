// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_GMCHAT_START_CHAT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_START_CHAT_REQ : GameClientPacket
{
  private string string_0;
  private int int_0;
  private byte byte_0;

  private MessageModel method_0(ClanModel clanModel_0, Account long_0)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = clanModel_0.Name,
      SenderId = long_0.PlayerId,
      ClanId = clanModel_0.Id,
      Type = NoteMessageType.Clan,
      Text = long_0.Nickname,
      State = NoteMessageState.Unreaded,
      ClanNote = NoteMessageClan.Secession
    };
    return !DaoManagerSQL.CreateMessage(clanModel_0.OwnerId, messageModel) ? (MessageModel) null : messageModel;
  }

  public virtual void Read() => ((PROTOCOL_GMCHAT_END_CHAT_REQ) this).long_0 = this.ReadQ();
}
