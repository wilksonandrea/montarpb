// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 1111);
    this.WriteC((byte) 1);
    this.WriteD(1);
    this.WriteC((byte) 1);
  }

  public PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK(int account_1, int roomModel_1, [In] int obj2)
  {
    ((PROTOCOL_SHOP_PLUS_POINT_ACK) this).int_1 = account_1;
    ((PROTOCOL_SHOP_PLUS_POINT_ACK) this).int_0 = roomModel_1;
    ((PROTOCOL_SHOP_PLUS_POINT_ACK) this).int_2 = obj2;
  }
}
