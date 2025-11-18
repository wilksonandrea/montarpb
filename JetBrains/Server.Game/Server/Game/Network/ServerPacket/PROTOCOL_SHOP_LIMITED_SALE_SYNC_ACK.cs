// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SHOP_LIMITED_SALE_SYNC_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_LIMITED_SALE_SYNC_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 1072);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_SHOP_PLUS_POINT_ACK) this).int_2);
    this.WriteD(((PROTOCOL_SHOP_PLUS_POINT_ACK) this).int_0);
    this.WriteD(((PROTOCOL_SHOP_PLUS_POINT_ACK) this).int_1);
  }
}
