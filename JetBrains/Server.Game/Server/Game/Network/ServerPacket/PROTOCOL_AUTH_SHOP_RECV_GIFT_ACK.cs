// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK : GameServerPacket
{
  private readonly MessageModel messageModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 1088);
    this.WriteD(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_ACK) this).uint_0);
  }

  public PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK([In] string obj0, int int_1, [In] int obj2)
  {
    ((PROTOCOL_AUTH_SHOP_JACKPOT_ACK) this).string_0 = obj0;
    ((PROTOCOL_AUTH_SHOP_JACKPOT_ACK) this).int_0 = int_1;
    ((PROTOCOL_AUTH_SHOP_JACKPOT_ACK) this).int_1 = obj2;
  }
}
