// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SHOP_TAG_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_TAG_INFO_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 1098);
    this.WriteC((byte) 1);
    this.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
  }

  public PROTOCOL_SHOP_TAG_INFO_ACK(int eventErrorEnum_1, [In] int obj1)
  {
    ((PROTOCOL_SHOP_PLUS_TAG_ACK) this).int_0 = eventErrorEnum_1;
    ((PROTOCOL_SHOP_PLUS_TAG_ACK) this).int_1 = obj1;
  }
}
