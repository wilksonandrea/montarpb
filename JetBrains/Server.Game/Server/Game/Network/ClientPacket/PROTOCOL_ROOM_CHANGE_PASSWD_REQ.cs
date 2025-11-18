// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_CHANGE_PASSWD_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_PASSWD_REQ : GameClientPacket
{
  private string string_0;

  private MessageModel method_0(string account_0, long syncServerPacket_0, [In] long obj2)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = account_0,
      SenderId = obj2,
      Text = ((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).string_1,
      State = NoteMessageState.Unreaded
    };
    if (DaoManagerSQL.CreateMessage(syncServerPacket_0, messageModel))
      return messageModel;
    ((PROTOCOL_MESSENGER_NOTE_SEND_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
    return (MessageModel) null;
  }

  public virtual void Read()
  {
    ((PROTOCOL_ROOM_CHANGE_COSTUME_REQ) this).teamEnum_0 = (TeamEnum) this.ReadC();
  }
}
