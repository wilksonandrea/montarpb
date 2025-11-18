// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_SEND_WHISPER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Network.ServerPacket;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SEND_WHISPER_REQ : GameClientPacket
{
  private long long_0;
  private string string_0;
  private string string_1;

  private MessageModel method_0(string int_1, long long_0, [In] long obj2)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = int_1,
      SenderId = obj2,
      Text = ((PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ) this).string_0,
      State = NoteMessageState.Unreaded
    };
    if (DaoManagerSQL.CreateMessage(long_0, messageModel))
      return messageModel;
    this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147483648U /*0x80000000*/));
    return (MessageModel) null;
  }

  public PROTOCOL_AUTH_SEND_WHISPER_REQ()
  {
    ((PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ) this).list_0 = new List<CartGoods>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_AUTH_RECV_WHISPER_REQ) this).string_0 = this.ReadU(66);
    ((PROTOCOL_AUTH_RECV_WHISPER_REQ) this).string_1 = this.ReadU((int) this.ReadH() * 2);
  }
}
