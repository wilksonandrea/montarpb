// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK : GameServerPacket
{
  private readonly EventErrorEnum eventErrorEnum_0;

  public virtual void Write()
  {
    this.WriteH((short) 1085);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK) this).uint_0);
  }

  public PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK([In] uint obj0)
  {
    ((PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK) this).uint_0 = obj0;
  }
}
