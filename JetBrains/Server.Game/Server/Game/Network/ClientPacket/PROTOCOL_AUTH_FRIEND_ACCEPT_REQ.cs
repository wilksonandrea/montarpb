// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_FRIEND_ACCEPT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FRIEND_ACCEPT_REQ : GameClientPacket
{
  private int int_0;
  private uint uint_0;

  private MessageModel method_0([In] string obj0, long int_3, [In] long obj2)
  {
    MessageModel messageModel = new MessageModel(7.0)
    {
      SenderId = obj2,
      SenderName = obj0,
      Type = NoteMessageType.Insert,
      State = NoteMessageState.Unreaded
    };
    return !DaoManagerSQL.CreateMessage(int_3, messageModel) ? (MessageModel) null : messageModel;
  }

  public virtual void Read() => ((PROTOCOL_AUTH_FIND_USER_REQ) this).string_0 = this.ReadU(34);
}
