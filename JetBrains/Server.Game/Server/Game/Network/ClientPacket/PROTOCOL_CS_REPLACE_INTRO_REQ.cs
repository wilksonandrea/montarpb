// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_REPLACE_INTRO_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_REPLACE_INTRO_REQ : GameClientPacket
{
  private string string_0;
  private EventErrorEnum eventErrorEnum_0;

  private MessageModel method_0(ClanModel int_0, long long_0, long long_1)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = int_0.Name,
      SenderId = long_1,
      ClanId = int_0.Id,
      Type = NoteMessageType.ClanInfo,
      Text = ((PROTOCOL_CS_NOTE_REQ) this).string_0,
      State = NoteMessageState.Unreaded
    };
    return !DaoManagerSQL.CreateMessage(long_0, messageModel) ? (MessageModel) null : messageModel;
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_PAGE_CHATTING_REQ) this).chattingType_0 = (ChattingType) this.ReadH();
    ((PROTOCOL_CS_PAGE_CHATTING_REQ) this).string_0 = this.ReadU((int) this.ReadH() * 2);
  }
}
