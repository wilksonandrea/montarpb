// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1079);
    this.WriteD((uint) ((PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK) this).messageModel_0.ObjectId);
    this.WriteD((uint) ((PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK) this).messageModel_0.SenderId);
    this.WriteD((int) ((PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK) this).messageModel_0.State);
    this.WriteD((uint) ((PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK) this).messageModel_0.ExpireDate);
    this.WriteC((byte) (((PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK) this).messageModel_0.SenderName.Length + 1));
    this.WriteS(((PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK) this).messageModel_0.SenderName, ((PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK) this).messageModel_0.SenderName.Length + 1);
    this.WriteC((byte) 6);
    this.WriteS("EVENT", 6);
  }

  public PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK([In] ShopData obj0, int int_2)
  {
    ((PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK) this).shopData_0 = obj0;
    ((PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK) this).int_0 = int_2;
  }
}
