// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_ATTENDANCE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_ATTENDANCE_ACK : GameServerPacket
{
  private readonly EventVisitModel eventVisitModel_0;
  private readonly PlayerEvent playerEvent_0;
  private readonly EventErrorEnum eventErrorEnum_0;
  private readonly uint uint_0;
  private readonly uint uint_1;

  public virtual void Write()
  {
    this.WriteH((short) 1070);
    this.WriteD(((PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK) this).int_0);
    this.WriteD(((PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK) this).shopData_0.ItemsCount);
    this.WriteD(((PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK) this).shopData_0.Offset);
    this.WriteB(((PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK) this).shopData_0.Buffer);
    this.WriteD(100);
  }

  public PROTOCOL_BASE_ATTENDANCE_ACK(uint shopData_1)
  {
    ((PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK) this).uint_0 = shopData_1;
  }
}
