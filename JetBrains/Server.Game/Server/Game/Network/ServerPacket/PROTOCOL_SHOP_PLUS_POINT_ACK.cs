// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SHOP_PLUS_POINT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_PLUS_POINT_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;
  private readonly int int_2;

  public virtual void Write()
  {
    this.WriteH((short) 1030);
    this.WriteC((byte) ((PROTOCOL_SHOP_GET_SAILLIST_ACK) this).bool_0);
    this.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
  }
}
